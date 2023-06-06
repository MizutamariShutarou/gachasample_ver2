using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using StoringPlace;
using Cysharp.Threading.Tasks;
using System.Threading;

//日本語対応
public class AsstsBundles
{
    private AssetBundle _armor = default;
    private AssetBundle _arrow = default;
    private AssetBundle _boots = default;
    private AssetBundle _bow = default;
    private AssetBundle _gloves = default;
    private AssetBundle _helmet = default;
    private AssetBundle _weaponIcon = default;
    private AssetBundle _weaponObj = default;
    public AssetBundle Armor => _armor;
    public AssetBundle Arrow => _arrow;
    public AssetBundle Boots => _boots;
    public AssetBundle Bow => _bow;
    public AssetBundle Gloves => _gloves;
    public AssetBundle Helmet => _helmet;
    public AssetBundle WeaponIcon => _weaponIcon;
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
    }

    public async UniTask LoadWeaponIcon(CancellationToken ct)
    {
        var weaponIconAssetBundleRequest
            = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, AssetBundleName.WEAPON));

        _weaponIcon = weaponIconAssetBundleRequest.assetBundle;

        // _weapon = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, AssetBundleName.WEAPON));
        await UniTask.CompletedTask;
    }

    public async UniTask LoadWeaponObj(CancellationToken ct)
    {
        var weaponObjAssetBundleRequest
           = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, AssetBundleName.WEAPONOBJ));

        _weaponObj = weaponObjAssetBundleRequest.assetBundle;

        await UniTask.CompletedTask;
    }
}
