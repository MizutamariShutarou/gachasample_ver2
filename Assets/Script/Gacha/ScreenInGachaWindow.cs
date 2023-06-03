using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//日本語対応
public class ScreenInGachaWindow
{
    private GachaTopScreen _gachaTopScreen = default;

    private GachaStagingScreen _gachaStagingScreen = default;

    private GachaResultScreen _gachaResultScreen = default;

    public GachaTopScreen TopScreen => _gachaTopScreen;

    public GachaStagingScreen StagingScreen => _gachaStagingScreen;

    public GachaResultScreen ResultScreen => _gachaResultScreen;

}
