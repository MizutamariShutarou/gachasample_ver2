using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using System.Threading.Tasks;
using System.Threading;

// 各State毎のdelagateを登録しておくクラス
public class StateMapping
{
    public Action onEnter = default;
    public Func<UniTask> EnterRoutine = default;
    public Action onExit = default;
    public Func<UniTask> ExitRoutine = default;
    public Action<float> onUpdate = default;
}

public class Transition<TState, TTrigger>
{
    public TState To { get; set; }
    public TTrigger Trigger { get; set; }
}

public class StateMachine<TState, TTrigger>
    where TState : struct, IConvertible, IComparable
    where TTrigger : struct, IConvertible, IComparable
{
    private MonoBehaviour _monoBehaviour = default;
    private TState _stateType = default;
    private StateMapping _stateMapping = default;

    /// <summary>
    /// 遷移中である場合の遷移先
    /// </summary>
    private TState? _destinationState = default;
    
    /// <summary>
    /// Exit遷移中か
    /// </summary>
    private bool _inExitTransition = default;

    /// <summary>
    /// Enter遷移中か
    /// </summary>
    private bool _inEnterTransition = default;    

    private Dictionary<object, StateMapping> _stateMappings = new Dictionary<object, StateMapping>();
    private Dictionary<TState, List<Transition<TState, TTrigger>>> _transitionLists = new Dictionary<TState, List<Transition<TState, TTrigger>>>();

    public StateMachine(MonoBehaviour monoBehaviour, TState initialState)
    {
        _monoBehaviour = monoBehaviour;

        // StateからStateMappingを作成
        var enumValues = Enum.GetValues(typeof(TState));
        for (int i = 0; i < enumValues.Length; i++)
        {
            var mapping = new StateMapping();
            _stateMappings.Add(enumValues.GetValue(i), mapping);
        }

        // 最初のStateに遷移
        ChangeStateImmediately(initialState);
    }

    /// <summary>
    /// トリガーを実行する
    /// </summary>
    public async UniTask<bool> ExecuteTriggerAsync(TTrigger trigger)
    {
        var transitions = _transitionLists[_stateType];
        foreach (var transition in transitions)
        {
            if (transition.Trigger.Equals(trigger))
            {
                await ChangeState(transition.To);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 遷移情報を登録する
    /// </summary>
    public void AddTransition(TState from, TState to, TTrigger trigger, TTrigger? otherTrigger = null)
    {
        if (!_transitionLists.ContainsKey(from))
        {
            _transitionLists.Add(from, new List<Transition<TState, TTrigger>>());
        }
        var transitions = _transitionLists[from];
        var transition = transitions.FirstOrDefault(x => x.To.Equals(to));
        if (transition == null)
        {
            // 新規登録
            transitions.Add(new Transition<TState, TTrigger> { To = to, Trigger = trigger});

            if (otherTrigger == null) return;
            transitions.Add(new Transition<TState, TTrigger> { To = to, Trigger = (TTrigger)otherTrigger });
        }
        else
        {
            // 更新
            transition.To = to;
            transition.Trigger = trigger;
        }
    }

    /// <summary>
    /// Stateを初期化する
    /// </summary>
    public void SetupState(TState state, Action onEnter = null, Func<UniTask> enterRoutine = null, Action onExit = null, Func<UniTask> exitRoutine = null, Action<float> onUpdate = null)
    {
        var stateMapping = _stateMappings[state];
        stateMapping.onEnter = onEnter;
        stateMapping.EnterRoutine = enterRoutine;
        stateMapping.onExit = onExit;
        stateMapping.ExitRoutine = exitRoutine;
        stateMapping.onUpdate = onUpdate;
    }

    /// <summary>
    /// 更新する
    /// </summary>
    public void Update(float deltaTime)
    {
        if (_inExitTransition || _inEnterTransition)
        {
            // 遷移中は更新しない
            return;
        }
        if (_stateMapping != null && _stateMapping.onUpdate != null)
        {
            _stateMapping.onUpdate(deltaTime);
        }
    }

    /// <summary>
    /// Stateをただちに変更する
    /// </summary>
    private void ChangeStateImmediately(TState to)
    {
        // Exit処理
        if (_stateMapping != null)
        {
            if (_stateMapping.onExit != null)
            {
                _stateMapping.onExit();
            }
        }

        // Enter処理
        _stateType = to;
        _stateMapping = _stateMappings[_stateType];
        if (_stateMapping.onEnter != null)
        {
            _stateMapping.onEnter();
        }
    }

    /// <summary>
    /// Stateを変更する
    /// </summary>
    private async UniTask ChangeState(TState to)
    {
        if (_inEnterTransition)
        {
            // Enter遷移中だったら何もせずbreak（状態遷移失敗）
            Debug.Log($"{to}へのEnter遷移中です。完了するまでお待ちください。");
            return;
        }

        _destinationState = to;
        if (_inExitTransition)
        {
            // Exit遷移中だったら遷移先を上書きしてbreak
            Debug.Log($"{to}のExit遷移中です。完了するまでお待ちください。");
            return;
        }

        // Exit
        _inExitTransition = true;
        if (_stateMapping != null)
        {
            if (_stateMapping.ExitRoutine != null)
            {
                await _stateMapping.ExitRoutine();
            }
            if (_stateMapping.onExit != null)
            {
                _stateMapping.onExit();
            }
        }
        _inExitTransition = false;

        // Enter
        _inEnterTransition = true;
        var stateMapping = _stateMappings[_destinationState.Value];
        if (stateMapping.EnterRoutine != null)
        {
            await stateMapping.EnterRoutine();
        }
        if (stateMapping.onEnter != null)
        {
            stateMapping.onEnter();
        }
        _inEnterTransition = false;
        // Stateを書き換え
        _stateType = _destinationState.Value;
        _stateMapping = _stateMappings[_stateType];

        _destinationState = null;
    }
}