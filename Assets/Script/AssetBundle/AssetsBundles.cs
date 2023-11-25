using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

//日本語対応
public class AssetBundleStore
{
    private const string ARMOR = "gacha/armor";
    private const string ARROW = "gacha/arrow";
    private const string BOOTS = "gacha/boots";
    private const string BOW = "gacha/bow";
    private const string GLOVES = "gacha/gloves";
    private const string HELMET = "gacha/helmet";
    private const string WEAPON = "gacha/weapon";
    private const string WEAPONOBJ = "gacha/weaponobj";

    private Dictionary<int, AssetBundle> _bundleStore = new Dictionary<int, AssetBundle>();

    public AssetBundle this[AssetsName name] => _bundleStore[(int)name];

    public async UniTask LoadAssetBundle(AssetsName key)
    {
        var assetBundleRequest
            = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, key.ToString()));

        await UniTask.WaitUntil(() => assetBundleRequest.isDone);
        _bundleStore[(int)key] = assetBundleRequest.assetBundle;
    }

    //private Dictionary<int, AssetBundle> _bundleStore = new Dictionary<int, AssetBundle>();

    //public AssetBundle this[AssetName name] => _bundleStore[(int)name];

    //public async UniTask LoadAssetBundle(AssetName key)
    //{
    //    //ここでダウンロードも考慮しておけると良い

    //    var assetBundleRequest
    //        = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, key.ToString()));

    //    await UniTask.WaitUntil(() => assetBundleRequest.isDone);
    //    _bundleStore[(int)key] = assetBundleRequest.assetBundle;
    //}
}
