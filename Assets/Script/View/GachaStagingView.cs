using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GachaStagingView : ViewBase, ISubscribe
{
    private ScreenController _screenController = default;

    [SerializeField, Header("State")]
    private Navigation.State _state = Navigation.State.GachaStaging;

    [SerializeField, Header("Screen")]
    private ScreenCollection.Screens _screen = ScreenCollection.Screens.GachaStaging;

    [SerializeField]
    private Button _doGachaButton = default;

    [SerializeField]
    private Animation _gachaAnim = default;

    [SerializeField]
    private LoadAssetData _loadAssetData = default;

    [SerializeField]
    private Canvas _loadingCanvas = default;

    private void Awake()
    {
        _screenController = GetComponent<ScreenController>();
    }
    private void Start()
    {
        Initialize(_state, _screenController.NavigationEntryPoint);
        Subscribe();
        _screenController = GetComponent<ScreenController>();
    }
    public void Subscribe()
    {

    }
    public void Release()
    {

    }
    protected override async UniTask OnEnter(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log("OnEnter : " + state + (popped ? " (pop)" : ""));
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnExit(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log("OnExit : " + state + (popped ? " (pop)" : ""));
        await UniTask.CompletedTask;
    }

    protected override async UniTask EnterRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        //Debug.Log("sta");
        //_loadingCanvas.gameObject.SetActive(true);
        //await _loadAssetData.DataPreparation();
        //await _loadAssetData.LoadAssets();
        //_loadingCanvas.gameObject.SetActive(false);
        OnActive(true);
        await UniTask.CompletedTask;
    }

    protected override async UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log(state + " : ページがはけるアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
        OnActive(false);
    }

    protected override void OnActive(bool flag)
    {
        _screenController.ScreenCollection.ScreenList[_screen].gameObject.SetActive(flag);
    }
}
