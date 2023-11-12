using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GachaTopView : ViewBase, ISubscribe
{
    [SerializeField]
    Navigation.State _state;

    [SerializeField]
    private Button _goHomePageButton;
    void Start()
    {
        Initialize(Navigation.State.GachaTop);
        Subscribe();
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
        Debug.Log(state + " : ���[�h�����Ȃ�" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
        Debug.Log(state + " : �y�[�W�ɓ���A�j���[�V�����Ȃ�" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
    }

    protected override async UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log(state + " : �y�[�W���͂���A�j���[�V�����Ȃ�" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
    }
}
