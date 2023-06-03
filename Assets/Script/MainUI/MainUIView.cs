using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//日本語対応
public class MainUIView : MonoBehaviour
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

    public void Initialize()
    {
        _nowWindow = _homeWindow;
        _gachaWindow.gameObject.SetActive(false);
    }

    public void OnChangeWindow(GameObject old, GameObject next)
    {
        _nowWindow = old;
        old.SetActive(false);
        next.SetActive(true);
        _nowWindow = next;
    }
}
