//日本語対応

using System;
using System.Collections.Generic;

public static class AssetBundleName
{
    private const string ARMOR = "gacha/armor";
    private const string ARROW = "gacha/arrow";
    private const string BOOTS = "gacha/boots";
    private const string BOW = "gacha/bow";
    private const string GLOVES = "gacha/gloves";
    private const string HELMET = "gacha/helmet";
    private const string WEAPON = "gacha/weapon";
    private const string WEAPONOBJ = "gacha/weaponobj";

    private static Dictionary<AssetName, string> _assetBundlesDic = new Dictionary<AssetName, string>();
    public static Dictionary<AssetName, string> AssetBundlesDic => _assetBundlesDic;

    static AssetBundleName()
    {
        _assetBundlesDic.Add(AssetName.Armor, ARMOR);
        _assetBundlesDic.Add(AssetName.Arrow, ARROW);
        _assetBundlesDic.Add(AssetName.Boots, BOOTS);
        _assetBundlesDic.Add(AssetName.Bow, BOW);
        _assetBundlesDic.Add(AssetName.Glove, GLOVES);
        _assetBundlesDic.Add(AssetName.Helmet, HELMET);
        _assetBundlesDic.Add(AssetName.Weapon, WEAPON);
        _assetBundlesDic.Add(AssetName.WeaponObj, WEAPONOBJ);
    }

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
}

