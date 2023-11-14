using System.Collections.Generic;
using UnityEngine;

//日本語対応

[RequireComponent(typeof(GachaTopView), typeof(GachaStagingView))]
public class ScreenCollection : MonoBehaviour
{
    public enum Screens
    {
        None,
        GachaTop,
        GachaStaging,
        GachaResult,
    }
    [SerializeField]
    NavigationEntryPoint _navigationEntryPoint;

    [SerializeField, Header("GachaTop")]
    private GameObject _gachaTop = default;

    [SerializeField, Header("GachaStaging")]
    private GameObject _gachaStaging = default;

    [SerializeField, Header("GachaResult")]
    private GameObject _gachaResult = default;

    public NavigationEntryPoint NavigationEntryPoint => _navigationEntryPoint;

    private Dictionary<Screens, GameObject> _screenList = new Dictionary<Screens, GameObject>();

    public Dictionary<Screens, GameObject> ScreenList => _screenList;

    void Start()
    {
        _screenList.Add(Screens.None, null);
        _screenList.Add(Screens.GachaTop, _gachaTop);
        _screenList.Add(Screens.GachaStaging, _gachaStaging);
        _screenList.Add(Screens.GachaResult, _gachaResult);
    }
}
