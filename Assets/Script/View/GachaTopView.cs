using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GachaTopView : ViewBase, ISubscribe
{
    private ScreenController _screenController = default;

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
        _screenController = GetComponent<ScreenController>();
    }
    void Start()
    {
        Initialize(Navigation.State.GachaTop, _screenController.NavigationEntryPoint);
        _screenController = GetComponent<ScreenController>();
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
        Debug.Log(state + " : ページをめくるアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.CompletedTask;
    }

    protected override async UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        _confirmationPanel.gameObject.SetActive(false);
        Debug.Log(state + " : ページがはけるアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.CompletedTask;
    }

    
    protected override void OnActive(bool flag)
    {
        _screenController.ScreenCollection.ScreenList[_screen].gameObject.SetActive(flag);
    }
}