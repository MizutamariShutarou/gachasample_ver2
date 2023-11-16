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

    public async UniTask LoadWeaponIcon()
    {
        var weaponIconAssetBundleRequest
            = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, AssetBundleName.WEAPON));
        await weaponIconAssetBundleRequest;
        _weaponIcon = weaponIconAssetBundleRequest.assetBundle;
        Debug.Log(weaponIconAssetBundleRequest.progress);
    }

    public async UniTask LoadWeaponObj()
    {
        var weaponObjAssetBundleRequest
           = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, AssetBundleName.WEAPONOBJ));

        _weaponObj = weaponObjAssetBundleRequest.assetBundle;

        await UniTask.CompletedTask;
    }
}
