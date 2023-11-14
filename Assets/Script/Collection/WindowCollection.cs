using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HomeWindowView), typeof(GachaWindowView))]
//日本語対応
public class WindowCollection : MonoBehaviour
{
    public enum Windows
    {
        None,
        Home,
        Gacha,
    }
    [SerializeField, Header("HomeWindow")]
    private GameObject _homeWindow = default;

    [SerializeField, Header("GachaWindow")]
    private GameObject _GachaWindow = default;

    private Dictionary<Windows, GameObject> _windowList = new Dictionary<Windows, GameObject>(); 

    public Dictionary<Windows, GameObject> WindowList => _windowList;

    void Start()
    {
        _windowList.Add(Windows.None, null);
        _windowList.Add(Windows.Home, _homeWindow);
        _windowList.Add(Windows.Gacha, _GachaWindow);
    }
}
