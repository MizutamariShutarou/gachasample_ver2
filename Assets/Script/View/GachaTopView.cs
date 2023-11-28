using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GachaTopView : ViewBase, ISubscribe
{
    private GachaScreenController _screenController = default;

    [SerializeField]
    private Canvas _confirmationPanel;

    [SerializeField]
    private Button _activeGachaConfirmationButton;

    [SerializeField]
    private Button _goGachaStagingButton;

    private void Awake()
    {
        _screenController = GetComponent<GachaScreenController>();
    }
    void Start()
    {
        Initialize(Navigation.State.GachaTop, _screenController.NavigationEntryPoint);
        _screenController = GetComponent<GachaScreenController>();
    }
    public void Subscribe()
    {
        _activeGachaConfirmationButton.onClick.AddListener(() => _confirmationPanel.gameObject.SetActive(true));
        _goGachaStagingButton.onClick.AddListener(
            async () => await _screenController.NavigationEntryPoint.Navigation.ExecuteTrigger(Navigation.Trigger.TapGachaButton));
    }
    public void Release()
    {
        _activeGachaConfirmationButton.onClick.RemoveAllListeners();
        _goGachaStagingButton.onClick.RemoveAllListeners();
    }
    protected override async UniTask OnEnter(Navigation.State state, bool popped, CancellationToken ct)
    {
        OnActive(true);
        Subscribe();
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnExit(Navigation.State state, bool popped, CancellationToken ct)
    {
        Release();
        OnActive(false);
        await UniTask.CompletedTask;
    }

    protected override async UniTask EnterRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        await UniTask.CompletedTask;
    }

    protected override async UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        _confirmationPanel.gameObject.SetActive(false);
        await UniTask.CompletedTask;
    }


    protected override void OnActive(bool flag)
    {
        _screenController.ScreenCollection.ScreenList[GachaScreenCollection.Screens.GachaTop].gameObject.SetActive(flag);
    }
}