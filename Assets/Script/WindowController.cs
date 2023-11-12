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
        _gachaButton.onClick.AddListener(OnGachaButtonClicked);
    }

    public void Release()
    {
        _homeButton.onClick.RemoveAllListeners();
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

    private void OnGachaButtonClicked()
    {
        if (_nowWindow == _gachaWindow)
        {
            return;
        }
        OnChangeWindow(_nowWindow, _gachaWindow);
    }
}
