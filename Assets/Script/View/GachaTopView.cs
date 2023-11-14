using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GachaWindowView : ViewBase, ISubscribe
{
    WindowCollection _windowCollection;

    [SerializeField, Header("State")]
    private Navigation.State _state;

    [SerializeField, Header("Window")]
    private WindowCollection.Windows _window;

    [SerializeField]
    private Button _goHomePageButton;
    void Start()
    {
        Initialize(Navigation.State.GachaTop);
        Subscribe();
        _windowCollection = GetComponent<WindowCollection>();
    }
    public void Subscribe()
    {
        _goHomePageButton.onClick.AddListener(async () => await _navigationEntryPoint.Navigation.ExecuteTrigger(Navigation.Trigger.TapHomePage));
    }
    public void Release()
    {
        _goHomePageButton.onClick.RemoveAllListeners();
    }
    protected override async UniTask EnterRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log(state + " : ロード処理など" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
        Debug.Log(state + " : ページに入るアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
        OnActive(true);
    }

    protected override async UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log(state + " : ページがはけるアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
        OnActive(false);
    }

    protected override void OnActive(bool flag)
    {
        _windowCollection.WindowList[WindowCollection.Windows.Gacha].gameObject.SetActive(flag);
    }
}
