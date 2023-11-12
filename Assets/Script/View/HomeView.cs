using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

//日本語対応
//TODO:Subscribe処理等をどこに持たせるか
public class HomeView : ViewBase, ISubscribe
{
    [SerializeField]
    Navigation.State _state;

    [SerializeField]
    private Button _goGachaPageButton = default;

    private void Start()
    {
        Initialize(_state);
        Subscribe();
    }
    public void Subscribe()
    {
        _goGachaPageButton.onClick.AddListener(async () => await _navigationEntryPoint.Navigation.ExecuteTrigger(Navigation.Trigger.TapEnterGachaPage));
    }
    public void Release()
    {
        _goGachaPageButton.onClick.RemoveAllListeners();
    }
    protected override async UniTask EnterRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log(state + " : ロード処理など" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
        Debug.Log(state + " : ページに入るアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
    }

    protected override async UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log(state + " : ページがはけるアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
    }
}
