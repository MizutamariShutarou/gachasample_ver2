using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Cysharp.Threading.Tasks;
using System.Threading;

//日本語対応
public class AsstsBundles
{
    private Dictionary<string, AssetBundle> _bundleStore = new Dictionary<string, AssetBundle>();
    public AssetBundle this[string name] => _bundleStore[name];

    public async UniTask LoadAssetBundle(string key)
    {
        //ここでダウンロードも考慮しておけると良い

        var assetBundleRequest
            = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, key));

        await UniTask.WaitUntil(() => assetBundleRequest.isDone);
        _bundleStore[key] = assetBundleRequest.assetBundle;
    }

    //public async UniTask LoadWeaponIcon()
    //{
    //    var weaponIconAssetBundleRequest
    //        = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, AssetBundleName.AssetBundlesDic[AssetBundleName.AssetName.Weapon]));
    //    await weaponIconAssetBundleRequest;
    //    _weaponIcon = weaponIconAssetBundleRequest.assetBundle;
    //    Debug.Log(weaponIconAssetBundleRequest.progress);
    //}

    //public async UniTask LoadWeaponObj()
    //{
    //    var weaponObjAssetBundleRequest
    //       = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, AssetBundleName.WEAPONOBJ));

    //    _weaponObj = weaponObjAssetBundleRequest.assetBundle;

    //    await UniTask.CompletedTask;
    //}
}
