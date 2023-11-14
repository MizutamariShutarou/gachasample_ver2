using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GachaStagingView : ViewBase, ISubscribe
{
    private ScreenCollection _screenCollection;

    [SerializeField, Header("State")]
    private Navigation.State _state = Navigation.State.GachaStaging;

    [SerializeField, Header("Screen")]
    private ScreenCollection.Screens _screen = ScreenCollection.Screens.GachaStaging;

    private void Awake()
    {
        _screenCollection = GetComponent<ScreenCollection>();
    }
    private void Start()
    {
        Initialize(_state, _screenCollection.NavigationEntryPoint);
        Subscribe();
        _screenCollection = GetComponent<ScreenCollection>();
    }
    public void Subscribe()
    {

    }
    public void Release()
    {

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

    protected override void OnActive(bool flag)
    {
        _screenCollection.ScreenList[_screen].gameObject.SetActive(flag);
    }
}
