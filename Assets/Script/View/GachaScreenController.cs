using UnityEngine;

//日本語対応
public class GachaScreenController : MonoBehaviour
{
    [SerializeField]
    NavigationEntryPoint _navigationEntryPoint = default;

    [SerializeField]
    GachaController _gcachaController = default;

    [SerializeField, Header("GachaTop")]
    private GameObject _gachaTop = default;

    [SerializeField, Header("GachaStaging")]
    private GameObject _gachaStaging = default;

    [SerializeField, Header("GachaResult")]
    private GameObject _gachaResult = default;

    private GachaScreenCollection _screenCollection = default;

    public NavigationEntryPoint NavigationEntryPoint => _navigationEntryPoint;

    public GachaController GachaController => _gcachaController;

    public GachaScreenCollection ScreenCollection => _screenCollection;

    private void Awake()
    {
        _screenCollection = new GachaScreenCollection(_gachaTop, _gachaStaging, _gachaResult);
        _screenCollection.ScreenList[GachaScreenCollection.Screens.GachaTop].SetActive(true);
        _screenCollection.ScreenList[GachaScreenCollection.Screens.GachaStaging].SetActive(false);
        _screenCollection.ScreenList[GachaScreenCollection.Screens.GachaResult].SetActive(false);
    }
}
