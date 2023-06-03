using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//日本語対応
public class WindowController : MonoBehaviour
{
    public Button HomeButton => _homeButton;

    public Button QuestButton => _questButton;

    public Button BoxButton => _boxButton;

    public Button GachaButton => _gachaButton;

    public Button MenuButton => _menuButton;

    public GameObject HomeWindow => _homeWindow;

    public GameObject GachaWindow => _gachaWindow;
    public GameObject NowWindow => _nowWindow;

    [SerializeField, Tooltip("HomeIcon")]
    private Button _homeButton = default;

    [SerializeField, Tooltip("QuestIcon")]
    private Button _questButton = default;

    [SerializeField, Tooltip("BoxIcon")]
    private Button _boxButton = default;

    [SerializeField, Tooltip("GachaIcon")]
    private Button _gachaButton = default;

    [SerializeField, Tooltip("MenuIcon")]
    private Button _menuButton = default;

    [SerializeField, Tooltip("HomeWindow")]
    private GameObject _homeWindow = default;

    [SerializeField, Tooltip("GachaWindow")]
    private GameObject _gachaWindow = default;

    private GameObject _nowWindow = default;
    void Start()
    {
        Initialize(true);
        Subscribe();
    }

    public void Initialize(bool flag)
    {
        _nowWindow = _homeWindow;
        _gachaWindow.gameObject.SetActive(!flag);
    }

    public void Subscribe()
    {
        _homeButton.onClick.AddListener(OnHomeButtonClicked);
        // _mainUIView.QuestButton.onClick.AddListener(OnQuestButtonClicked);
        _gachaButton.onClick.AddListener(OnGachaButtonClicked);
    }

    public void Release()
    {
        _homeButton.onClick.RemoveAllListeners();
        // _mainUIView.QuestButton.onClick.RemoveAllListeners();
        _gachaButton.onClick.RemoveAllListeners();
    }
    public void OnChangeWindow(GameObject old, GameObject next)
    {
        _nowWindow = old;
        old.SetActive(false);
        next.SetActive(true);
        _nowWindow = next;
    }
    private void OnHomeButtonClicked()
    {
        if (_nowWindow == _homeWindow)
        {
            return;
        }
        OnChangeWindow(_nowWindow, _homeWindow);
    }

    //private void OnQuestButtonClicked()
    //{
    //    if (_mainUIView.NowWindow == _mainUIView.HomeWindow)
    //    {
    //        return;
    //    }
    //    _mainUIView.OnChangeWindow(_mainUIView.NowWindow, _mainUIView.HomeWindow);
    //}

    private void OnGachaButtonClicked()
    {
        if (_nowWindow == _gachaWindow)
        {
            return;
        }
        OnChangeWindow(_nowWindow, _gachaWindow);
    }
}
