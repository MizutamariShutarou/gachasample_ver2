using UnityEngine;

//日本語対応
public abstract class ItemDataBase : ScriptableObject
{
    public bool _isFirst = default;

    public string _iconName = default;

    public string _itemObjName = default;

    public int _rareNum = default;

    public Sprite _iconImage;

    public GameObject _itemObject;

    public ItemType _itemType = default;
}
