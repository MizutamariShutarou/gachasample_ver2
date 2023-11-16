using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//日本語対応
public class ScreenController : MonoBehaviour
{
    [SerializeField]
    NavigationEntryPoint _navigationEntryPoint;

    [SerializeField, Header("GachaTop")]
    private GameObject _gachaTop = default;

    [SerializeField, Header("GachaStaging")]
    private GameObject _gachaStaging = default;

    [SerializeField, Header("GachaResult")]
    private GameObject _gachaResult = default;

    private ScreenCollection _screenCollection = default;

    public NavigationEntryPoint NavigationEntryPoint => _navigationEntryPoint;

    public ScreenCollection ScreenCollection => _screenCollection;

    private void Awake()
    {
        _screenCollection = new ScreenCollection(_gachaTop, _gachaStaging, _gachaResult);
        _screenCollection.ScreenList[ScreenCollection.Screens.GachaTop].SetActive(true);
        _screenCollection.ScreenList[ScreenCollection.Screens.GachaStaging].SetActive(false);
        _screenCollection.ScreenList[ScreenCollection.Screens.GachaResult].SetActive(false);
    }
}
