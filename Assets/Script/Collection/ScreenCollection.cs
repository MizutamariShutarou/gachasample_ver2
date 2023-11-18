using System.Collections.Generic;
using UnityEngine;

//日本語対応

public class GachaScreenCollection
{
    public enum Screens
    {
        None,
        GachaTop,
        GachaStaging,
        GachaResult,
    }

    private Dictionary<Screens, GameObject> _screenList = new Dictionary<Screens, GameObject>();

    public Dictionary<Screens, GameObject> ScreenList => _screenList;

    public GachaScreenCollection(GameObject top, GameObject staging, GameObject result)
    {
        _screenList.Add(Screens.None, null);
        _screenList.Add(Screens.GachaTop, top);
        _screenList.Add(Screens.GachaStaging, staging);
        _screenList.Add(Screens.GachaResult, result);
    }
}
