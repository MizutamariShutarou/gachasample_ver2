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

    public enum AssetName
    {
        Armor,
        Arrow,
        Boots,
        Bow,
        Glove,
        Helmet,
        Weapon,
        WeaponObj,
    }

    private Dictionary<AssetName, string> _assetBundlesDic = new Dictionary<AssetName, string>();

    public AssetBundleStore()
    {
        if (_assetBundlesDic.Count >= Enum.GetNames(typeof(AssetName)).Length) return;
        _assetBundlesDic.Add(AssetName.Armor, ARMOR);
        _assetBundlesDic.Add(AssetName.Arrow, ARROW);
        _assetBundlesDic.Add(AssetName.Boots, BOOTS);
        _assetBundlesDic.Add(AssetName.Bow, BOW);
        _assetBundlesDic.Add(AssetName.Glove, GLOVES);
        _assetBundlesDic.Add(AssetName.Helmet, HELMET);
        _assetBundlesDic.Add(AssetName.Weapon, WEAPON);
        _assetBundlesDic.Add(AssetName.WeaponObj, WEAPONOBJ);
    }

    // private Dictionary<string, AssetBundle> _bundleStore = new Dictionary<string, AssetBundle>();

    private Dictionary<AssetName, AssetBundle> _bundleStore = new Dictionary<AssetName, AssetBundle>();

    public AssetBundle this[AssetName name] => _bundleStore[name];

    public async UniTask LoadAssetBundle(AssetName key)
    {
        //ここでダウンロードも考慮しておけると良い

        var assetBundleRequest
            = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, _assetBundlesDic[key]));

        await UniTask.WaitUntil(() => assetBundleRequest.isDone);
        _bundleStore[key] = assetBundleRequest.assetBundle;
    }
}
