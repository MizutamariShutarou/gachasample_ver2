using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GachaStagingView : ViewBase, ISubscribe
{
    private GachaScreenController _screenController = default;

    [SerializeField, Header("State")]
    private Navigation.State _state = Navigation.State.GachaStaging;

    [SerializeField, Header("Screen")]
    private GachaScreenCollection.Screens _screen = GachaScreenCollection.Screens.GachaStaging;

    [SerializeField]
    private Button _doGachaButton = default;

    [SerializeField]
    private Animation _gachaAnim = default;

    private void Awake()
    {
        _screenController = GetComponent<GachaScreenController>();
        _gachaAnim.gameObject.SetActive(false);
    }
    private void Start()
    {
        Initialize(_state, _screenController.NavigationEntryPoint);
        _screenController = GetComponent<GachaScreenController>();
    }
    public void Subscribe()
    {
        _doGachaButton.onClick.AddListener(async() =>
        {
            await StartGachaStaging();
        });
    }
    public void Release()
    {

    }
    protected override async UniTask OnEnter(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log("OnEnter : " + state + (popped ? " (pop)" : ""));
        OnActive(true);
        Subscribe();
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnExit(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log("OnExit : " + state + (popped ? " (pop)" : ""));
        Release();
        OnActive(false);
        await UniTask.CompletedTask;
    }

    protected override async UniTask EnterRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        var amount = 0f;

        OnActive(true);
        LoadingManager.Instance.ActiveLoadingWindow(true); 
        _gachaAnim.gameObject.SetActive(false);

        await _screenController.GachaController.DataPreparation(AssetBundleStore.AssetName.Weapon);
        await _screenController.GachaController.LoadGachaData(AssetBundleStore.AssetName.Weapon);

        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken:ct);

        while(amount < 0.9f)
        {
            amount += 0.1f;
            await LoadingManager.Instance.ChangeSliderValue(amount, ct);
        }
        await LoadingManager.Instance.ChangeSliderValue(1f, ct);

        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken:ct);

        LoadingManager.Instance.ActiveLoadingWindow(false);

        await UniTask.CompletedTask;
    }

    protected override async UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log(state + " : ページがはけるアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, ct);
    }

    private async UniTask StartGachaAnim()
    {
        _gachaAnim.gameObject.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: this.GetCancellationTokenOnDestroy());
    }

    private async UniTask StartGachaStaging()
    {
        _doGachaButton.gameObject.SetActive(false);
        await StartGachaAnim();
        await _screenController.NavigationEntryPoint.Navigation.ExecuteTrigger(Navigation.Trigger.FinishStaging);
    }

    protected override void OnActive(bool flag)
    {
        _screenController.ScreenCollection.ScreenList[_screen].gameObject.SetActive(flag);
        _doGachaButton.gameObject.SetActive(flag);
    }
}
