using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GachaTopView : ViewBase, ISubscribe
{
    private ScreenCollection _screenCollection = default;

    [SerializeField, Header("State")]
    private Navigation.State _state = Navigation.State.GachaTop;

    [SerializeField, Header("Screen")]
    private ScreenCollection.Screens _screen = ScreenCollection.Screens.GachaTop;

    [SerializeField]
    private Canvas _confirmationPanel;

    [SerializeField]
    private Button _activeGachaConfirmationButton; 
    
    [SerializeField]
    private Button _goGachaStagingButton;
    private void Awake()
    {
        _screenCollection = GetComponent<ScreenCollection>();
    }
    void Start()
    {
        Initialize(Navigation.State.GachaTop, _screenCollection.NavigationEntryPoint);
        Subscribe();
    }
    public void Subscribe()
    {
        _activeGachaConfirmationButton.onClick.AddListener(() => _confirmationPanel.gameObject.SetActive(true));
        _goGachaStagingButton.onClick.AddListener(
            async () => await _screenCollection.NavigationEntryPoint.Navigation.ExecuteTrigger(Navigation.Trigger.TapGachaButton));
    }
    public void Release()
    {
        _activeGachaConfirmationButton.onClick.RemoveAllListeners();
        _goGachaStagingButton.onClick.RemoveAllListeners();
    }
    protected override async UniTask EnterRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        OnActive(true);
        await UniTask.CompletedTask;
    }

    protected override async UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        OnActive(false);
        await UniTask.CompletedTask;
    }

    protected override void OnActive(bool flag)
    {
        _screenCollection.ScreenList[_screen].gameObject.SetActive(flag);
    }
}