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

    private List<bool> _boolList = default;

    public List<Sprite> SpritesList => _spritesList;

    public List<GameObject> GameObjectsList => _gameObjectsList;

    public List<bool> BoolList => _boolList;
    
    public async UniTask DataPreparation()
    {
        try
        {
            _assetsBundles = new AsstsBundles();
        }
        catch(System.NullReferenceException e)
        {
            Debug.LogException(e);
            _alertpanel.SetActive(true);
        }
        finally
        {
            Debug.Log("何か処理があれば書きたい");
            _spritesList = new List<Sprite>(_num);
            _gameObjectsList = new List<GameObject>(_num);
            _boolList = new List<bool>(_num);
            await UniTask.CompletedTask;
        }
    }

    public async UniTask LoadAssets(CancellationToken cts)
    {
        for(int i = 0; i < _num; i++)
        {
            var randomNom = Random.Range(0, _weaponDataList.Count);

            bool isForFirstTime = _weaponDataList[randomNom]._isFirst;

            _boolList.Add(isForFirstTime);

            var sprite = _assetsBundles.Weapon.LoadAsset<Sprite>(_weaponDataList[randomNom]._iconName);

            _spritesList.Add(sprite);

            GameObject gameObject = _assetsBundles.WeaponObj.LoadAsset<GameObject>(_weaponDataList[randomNom]._itemObjName);
            Debug.Log(gameObject.name);
            _gameObjectsList.Add(gameObject);
        }
        await UniTask.CompletedTask;
    }

    public void UnLoadAsset()
    {
        _assetsBundles.Weapon.Unload(true);
        _assetsBundles.WeaponObj.Unload(true);
    }
}

