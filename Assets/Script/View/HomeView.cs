using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

//日本語対応
public class HomeWindowView : ViewBase, ISubscribe
{
    WindowCollection _windowCollection;

    [SerializeField, Header("State")]
    private Navigation.State _state;

    [SerializeField, Header("Window")]
    private WindowCollection.Windows _window;

    [SerializeField]
    private Button _goGachaPageButton = default;

    private void Start()
    {
        Initialize(_state);
        Subscribe();
        _windowCollection = GetComponent<WindowCollection>();
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
        OnActive(true);
    }

    protected override async UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        OnActive(false);
        Debug.Log(state + " : ページがはけるアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
    }

    protected override void OnActive(bool flag)
    {
        _windowCollection.WindowList[_window].SetActive(flag);
    }
}
