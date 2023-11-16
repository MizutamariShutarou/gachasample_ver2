using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//日本語対応
public class WindowController : MonoBehaviour
{
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

    [SerializeField]
    NavigationEntryPoint _navigationEntryPoint = default;

    private WindowCollection _windowCollection;
    private void Awake()
    {
        _windowCollection = new WindowCollection(_homeWindow, _gachaWindow);
    }
    void Start()
    {
        Initialize();
        Subscribe();
    }

    public void Initialize()
    {
        foreach (GameObject window in _windowCollection.WindowList.Values)
        {
            if(window == null)
            {
                continue;
            }
            else if(window == _windowCollection.WindowList[WindowCollection.Windows.Home])
            {
                _nowWindow = window;
            }
            else
            {
                window.gameObject.SetActive(false);
            }
        }
        _nowWindow.SetActive(true);
    }

    public void Subscribe()
    {
        _homeButton.onClick.AddListener(() => OnHomeButtonClicked(_windowCollection.WindowList[WindowCollection.Windows.Home]));
        _gachaButton.onClick.AddListener(() => OnGachaButtonClicked(_windowCollection.WindowList[WindowCollection.Windows.Gacha]));
    }

    public void Release()
    {
        _homeButton.onClick.RemoveAllListeners();
        _gachaButton.onClick.RemoveAllListeners();
    }

    private void OnChangeWindow(GameObject old, GameObject next)
    {
        old = _nowWindow;
        old.SetActive(false);
        next.SetActive(true);
        _nowWindow = next;
    }

    private async void OnHomeButtonClicked(GameObject next)
    {
        if (_nowWindow == next)
        {
            return;
        }
        await _navigationEntryPoint.Navigation.ExecuteTrigger(Navigation.Trigger.TapHomePage);
        OnChangeWindow(_nowWindow, next);
    }

    private async void OnGachaButtonClicked(GameObject next)
    {
        if (_nowWindow == next)
        {
            return;
        }
        await _navigationEntryPoint.Navigation.ExecuteTrigger(Navigation.Trigger.TapEnterGachaPage);
        OnChangeWindow(_nowWindow, next);
    }
}
