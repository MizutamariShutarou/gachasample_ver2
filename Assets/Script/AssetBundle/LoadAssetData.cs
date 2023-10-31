using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

//日本語対応
public class LoadAssetData : MonoBehaviour
{
    [SerializeField]
    private GameObject _alertpanel = default;

    private AsstsBundles _assetsBundles = default;

    public AsstsBundles AsstsBundles => _assetsBundles;

    [SerializeField]
    private List<ItemDataBase> _weaponDataList = default;

    public List<ItemDataBase> WeaponDataList => _weaponDataList;

    [SerializeField]
    private int _num = 10;

    public int Num => _num;

    private List<Sprite> _spritesList = default;

    private List<GameObject> _gameObjectsList = default;

    public List<Sprite> SpritesList => _spritesList;

    public List<GameObject> GameObjectsList => _gameObjectsList;
    
    public async UniTask DataPreparation(CancellationToken ct)
    {
        _assetsBundles = new AsstsBundles();

        _assetsBundles.LoadWeaponIcon(ct).Forget();
        Debug.Log("weaponIcon取得");
        _assetsBundles.LoadWeaponObj(ct).Forget();
        Debug.Log("weaponObj取得");

        if (_assetsBundles.WeaponIcon == null || _assetsBundles.WeaponObj == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            _alertpanel.gameObject.SetActive(true);
            return;
        }
        else
        {
            _spritesList = new List<Sprite>(_num);
            _gameObjectsList = new List<GameObject>(_num);
        }

        await UniTask.CompletedTask;
    }

    public async UniTask LoadAssets()
    {
        for(int i = 0; i < _num; i++)
        {
            var randomNom = Random.Range(0, _weaponDataList.Count);

            var sprite = _assetsBundles.WeaponIcon.LoadAsset<Sprite>(_weaponDataList[randomNom]._iconName);

            _spritesList.Add(sprite);

            GameObject gameObject = _assetsBundles.WeaponObj.LoadAsset<GameObject>(_weaponDataList[randomNom]._itemObjName);
            Debug.Log(gameObject.name);
            _gameObjectsList.Add(gameObject);
        }
        await UniTask.CompletedTask;
    }

    public void UnLoadAsset()
    {
        _assetsBundles.WeaponIcon.Unload(true);
        _assetsBundles.WeaponObj.Unload(true);
    }
}

