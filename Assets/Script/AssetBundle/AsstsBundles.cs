using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using StoringPlace;

//日本語対応
public class AsstsBundles
{
    private AssetBundle _armor = default;
    private AssetBundle _arrow = default;
    private AssetBundle _boots = default;
    private AssetBundle _bow = default;
    private AssetBundle _gloves = default;
    private AssetBundle _helmet = default;
    private AssetBundle _weapon = default;
    private AssetBundle _weaponObj = default;
    public AssetBundle Armor => _armor;
    public AssetBundle Arrow => _arrow;
    public AssetBundle Boots => _boots;
    public AssetBundle Bow => _bow;
    public AssetBundle Gloves => _gloves;
    public AssetBundle Helmet => _helmet;
    public AssetBundle Weapon => _weapon;
    public AssetBundle WeaponObj => _weaponObj;

    //TODO:重くて固まるから改良をする
    public AsstsBundles()
    {
        //_armor = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, AssetBundleName.ARMOR));
        //_arrow = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, AssetBundleName.ARROW));
        //_boots = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, AssetBundleName.BOOTS));
        //_bow = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, AssetBundleName.BOW));
        //_gloves = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, AssetBundleName.GLOVES));
        //_helmet = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, AssetBundleName.HELMET));
        var weaponIconAssetBundleRequest
            = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, AssetBundleName.WEAPON));

        _weapon = weaponIconAssetBundleRequest.assetBundle;

        // _weapon = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, AssetBundleName.WEAPON));
        Debug.Log("weapon読み込み完了");

         var weaponObjAssetBundleRequest
            = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, AssetBundleName.WEAPONOBJ));

        _weaponObj = weaponObjAssetBundleRequest.assetBundle;
        Debug.Log("weaponObj読み込み完了");
    }
}
