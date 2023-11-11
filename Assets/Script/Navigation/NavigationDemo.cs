using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class NavigationDemo : MonoBehaviour {

    [SerializeField]
    private Navigation _navigation;
    
    private void Start()
    {
        CancellationToken ct = this.GetCancellationTokenOnDestroy();
        // 初期化
        _navigation.Initialize();

        // 各状態毎のふるまいを定義
        SetupState(Navigation.State.Home, ct);
        SetupState(Navigation.State.GachaTop, ct);
        SetupState(Navigation.State.GachaStaging, ct);
        SetupState(Navigation.State.GachaResult, ct);
    }

    private void SetupState(Navigation.State state, CancellationToken ct)
    {
        // 画面遷移時の処理
        // モデルであるNavigationから通知を受けて
        // Viewを更新する処理を書く
        _navigation.SetupState
        (
            state,
            popped => Debug.Log("OnEnter : " + state + (popped ? " (pop)" : "")),
            popped => EnterRoutine(state, popped, ct),
            popped => Debug.Log("OnExit : " + state + (popped ? " (pop)" : "")),
            popped => ExitRoutine(state, popped, ct)
        );
    }

    protected async UniTask EnterRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log(state + " : ロード処理など" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
        Debug.Log(state + " : ページに入るアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
    }

    protected async UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log(state + " : ページがはけるアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
    }

    private async void Update()
    {
        // デバッグ用にトリガーを呼ぶ
        // 本来は各画面からNavigation.ExecuteTrigger()を直接呼ぶ
        if (Input.GetKeyDown(KeyCode.G))
            await _navigation.ExecuteTrigger(Navigation.Trigger.TapEnterGachaPage);
        if (Input.GetKeyDown(KeyCode.B))
            await _navigation.ExecuteTrigger(Navigation.Trigger.PageBack);
        if (Input.GetKeyDown(KeyCode.H))
            await _navigation.ExecuteTrigger(Navigation.Trigger.TapHomePage);
    }
}