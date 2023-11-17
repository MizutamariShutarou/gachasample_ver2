using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GachaResultView : ViewBase, ISubscribe
{
    private ScreenController _screenController = default;

    [SerializeField, Header("State")]
    private Navigation.State _state = Navigation.State.GachaTop;

    [SerializeField, Header("Screen")]
    private ScreenCollection.Screens _screen = ScreenCollection.Screens.GachaTop;

    [SerializeField]
    private GameObject _gachaIconBG = default;

    [SerializeField]
    private Image _defaultImage = default;

    [SerializeField]
    private Button _skipButton = default;

    [SerializeField]
    private Button _goHome = default;

    [SerializeField]
    private float _firstAwaitTime = 0.5f;

    [SerializeField]
    private float _skipAwaitTime = default;

    [SerializeField]
    private Canvas _firstCanvas = default;

    [SerializeField]
    GameObject _firstWeaponObj = default;

    private float _interval = 0f;

    private void Awake()
    {
        _screenController = GetComponent<ScreenController>();
    }

    private void Start()
    {
        Initialize(Navigation.State.GachaResult, _screenController.NavigationEntryPoint);
        _screenController = GetComponent<ScreenController>();
        _interval = _firstAwaitTime;
    }
    public void Subscribe()
    {
        _goHome.onClick.AddListener(
            async () => await _screenController.NavigationEntryPoint.Navigation.ExecuteTrigger(Navigation.Trigger.PageBack));

        _skipButton.onClick.AddListener(Skip);
    }

    public void Release()
    {
        _goHome.onClick.RemoveAllListeners();
        _skipButton.onClick.RemoveAllListeners();
    }

    protected override async UniTask EnterRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log(state + " : ページをめくるアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.CompletedTask;
    }

    protected override async UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log(state + " : ページがはけるアニメーションなど" + (popped ? " (pop)" : ""));
        var images = _gachaIconBG.GetComponentsInChildren<Image>();
        for(int i = 0; i < images.Length; i++)
        {
            images[i].sprite = _defaultImage.sprite;
        }
        _interval = _firstAwaitTime;
        await UniTask.CompletedTask;
    }

    protected override async UniTask OnEnter(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log("OnEnter : " + state + (popped ? " (pop)" : ""));
        OnActive(true);
        Subscribe();
        _goHome.gameObject.SetActive(false);
        await ShowResult();
        _goHome.gameObject.SetActive(true);
    }

    protected override async UniTask OnExit(Navigation.State state, bool popped, CancellationToken ct)
    {
        Debug.Log("OnExit : " + state + (popped ? " (pop)" : ""));
        Release();
        OnActive(false);
        await UniTask.CompletedTask;
    }
    protected override void OnActive(bool flag)
    {
        _screenController.ScreenCollection.ScreenList[_screen].gameObject.SetActive(flag);

    }
    private async UniTask ShowResult()
    {
        var imageAlly = _gachaIconBG.GetComponentsInChildren<Image>();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        for (int i = 0; i < LoadAssetData.Instance.Num; i++)
        {
            imageAlly[i].sprite = LoadAssetData.Instance.SpritesList[i];
            await UniTask.Delay(TimeSpan.FromSeconds(_interval));
        }
    }
    public void Skip()
    {
        if (_interval == _skipAwaitTime) return;
        _interval = _skipAwaitTime;
    }
}
