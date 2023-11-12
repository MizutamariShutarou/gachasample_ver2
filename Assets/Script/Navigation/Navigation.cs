using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//日本語対応
public class Navigation : MonoBehaviour
{
    public enum State
    {
        None,
        Home, // ホームページ
        GachaTop, // ガチャトップページ
        GachaStaging, // ガチャ演出
        GachaResult, // ガチャ結果
        End, // 終了
    }

    public enum Trigger
    {
        None,
        PageBack, // 戻る
        TapHomePage, // ホームページ遷移ボタン
        TapEnterGachaPage, // ガチャページ遷移ボタン
        TapGachaButton, // 回すボタンタップ
        FinishStaging, // ガチャ演出終了
        End, // 終了
    }

    private StateMachine<State, Trigger> _stateMachine = default;

    private List<State> _history = new List<State>();

    private bool _popped = false;

    private bool _isInitialized = false;


    public void Initialize()
    {
        if (_isInitialized) return;

        // StateMachineを生成
        _stateMachine = new StateMachine<State, Trigger>(this, State.Home);

        // 遷移情報を登録
        _stateMachine.AddTransition(State.Home, State.GachaTop, Trigger.TapEnterGachaPage);
        _stateMachine.AddTransition(State.GachaTop, State.Home, Trigger.PageBack, Trigger.TapHomePage);
        _stateMachine.AddTransition(State.GachaTop, State.GachaStaging, Trigger.TapGachaButton);
        _stateMachine.AddTransition(State.GachaStaging, State.GachaResult, Trigger.FinishStaging);
        _stateMachine.AddTransition(State.GachaResult, State.GachaTop, Trigger.PageBack);
        _stateMachine.AddTransition(State.GachaTop, State.End, Trigger.End);

        // 振る舞いを初期化
        foreach (State state in Enum.GetValues(typeof(State)))
        {
            SetupState(state);
        }

        _isInitialized = true;
    }
    /// <summary>
    /// Stateのふるまいを設定する
    /// </summary>
    public void SetupState(State state, Action<bool> onEnter = null, Func<bool, UniTask> enterRoutine = null
        , Action<bool> onExit = null, Func<bool, UniTask> exitRoutine = null, Action<bool, float> onUpdate = null)
    {
        // On Enter
        Action onEnterArg = () =>
        {
            var popped = _popped;
            OnEnter(state);
            if (onEnter != null)
                onEnter(popped);
        };

        // Enter Routine
        Func<UniTask> enterRoutineArg = null;
        if (enterRoutine != null)
            enterRoutineArg = () => enterRoutine(_popped);

        // On Exit
        Action onExitArg = null;
        if (onExit != null)
            onExitArg = () => onExit(_popped);

        // Exit Routine
        Func<UniTask> exitRoutineArg = null;
        if (exitRoutine != null)
            exitRoutineArg = () => exitRoutine(_popped);

        // Update
        Action<float> onUpdateArg = null;
        if (onUpdate != null)
            onUpdateArg = deltaTime => onUpdate(_popped, deltaTime);

        _stateMachine.SetupState(state, onEnterArg, enterRoutineArg, onExitArg, exitRoutineArg, onUpdateArg);
    }

    /// <summary>
    /// トリガーを実行する
    /// 各Viewから呼ぶ（ボタンが押されたときなど）
    /// </summary>
    public async UniTask<bool> ExecuteTrigger(Trigger trigger)
    {
        Debug.Log(trigger);
        if (trigger == Trigger.PageBack)
        {
            // 戻るトリガーだった場合は戻る処理をする
            return await Pop();
        }
        return await _stateMachine.ExecuteTriggerAsync(trigger);
    }

    /// <summary>
    /// ページを戻る
    /// </summary>
    private async UniTask<bool> Pop()
    {
        if (await _stateMachine.ExecuteTriggerAsync(Trigger.PageBack) && _history.Count >= 1)
        {
            // 戻ったフラグを立てておく
            _popped = true;
            return true;
        }
        return false;
    }

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Stateに入った時の処理
    /// </summary>
    private void OnEnter(State state)
    {
        if (_popped)
        {
            // 戻ってきた場合は履歴から削除する
            _history.RemoveAt(_history.Count - 1);
            _popped = false;
        }
        else if (IsBackable(state))
        {
            // 履歴に積んで戻れるようにする
            _history.Add(state);
        }
        if (ClearHistoryWhenEnter(state))
        {
            // 履歴をクリアする
            _history.Clear();
        }
    }

    /// <summary>
    /// 履歴に積んで戻れるようにするか
    /// </summary>
    private bool IsBackable(State state)
    {
        switch (state)
        {
            case State.Home:
            case State.GachaTop:
            case State.End:
                return false;
            case State.GachaStaging:
            case State.GachaResult:
                return true;
            default:
                throw new Exception("予期しないStateが検知されました");
        }
    }

    /// <summary>
    /// このStateに入った時に履歴をクリアするか
    /// </summary>
    private bool ClearHistoryWhenEnter(State state)
    {
        switch (state)
        {
            case State.GachaTop:
                return false;
            case State.Home:
            case State.GachaStaging:
            case State.GachaResult:
            case State.End:
                return true;
            default:
                throw new Exception("予期しないStateが検知されました");
        }
    }

    private void Update()
    {
        // ステートマシンを更新
        _stateMachine.Update(Time.deltaTime);
    }

}
