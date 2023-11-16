using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEditorInternal;

//日本語対応
public class LoadAssetData : MonoBehaviour
{
    private static LoadAssetData _instance = default;

    public static LoadAssetData Instance => _instance;

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

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            _assetsBundles = new AsstsBundles();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        _alertpanel.gameObject.SetActive(false);
    }

    public async UniTask Load()
    {
        await _assetsBundles.LoadWeaponIcon();
    }
    public async UniTask DataPreparation()
    {
        if (_assetsBundles.WeaponIcon == null)// || _assetsBundles.WeaponObj == null)
        {
            Debug.Log("データがないためダウンロードします");
            await Load();
            Debug.Log("ダウンロード完了");
        }
        _spritesList = new List<Sprite>(_num);
    }

    public async UniTask LoadAssets()
    {
        for(int i = 0; i < _num; i++)
        {
            var randomNom = Random.Range(0, _weaponDataList.Count);

            if(_assetsBundles.WeaponIcon == null)
            {
                LoadingManager.Instance.ActiveLoadingWindow(false);
                _alertpanel.gameObject.SetActive(true);
            }
            var sprite = _assetsBundles.WeaponIcon.LoadAsset<Sprite>(_weaponDataList[randomNom]._iconName);

            _spritesList.Add(sprite);

            // GameObject gameObject = _assetsBundles.WeaponObj.LoadAsset<GameObject>(_weaponDataList[randomNom]._itemObjName);
            // _gameObjectsList.Add(gameObject);
        }
        await UniTask.CompletedTask;
    }

    public void UnLoadAsset()
    {
        _assetsBundles.WeaponIcon.Unload(true);
        // _assetsBundles.WeaponObj.Unload(true);
    }
}

