using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//日本語対応
public class WindowCollection
{
    public enum Windows
    {
        None,
        Home,
        Gacha,
        Loading,
    }
    private Dictionary<Windows, GameObject> _windowList = new Dictionary<Windows, GameObject>(); 

    public Dictionary<Windows, GameObject> WindowList => _windowList;

    public WindowCollection(GameObject Home, GameObject Gacha)
    {
        _windowList.Add(Windows.None, null);
        _windowList.Add(Windows.Home, Home);
        _windowList.Add(Windows.Gacha, Gacha);
    }
}
