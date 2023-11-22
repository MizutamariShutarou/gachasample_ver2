using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//日本語対応
public class LoadAssetData : MonoBehaviour
{
    private static LoadAssetData _instance = default;

    public static LoadAssetData Instance => _instance;

    [SerializeField]
    private GameObject _alertpanel = default;

    private AssetBundleStore _store = new AssetBundleStore();

    public AssetBundleStore Store => _store;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        _alertpanel.gameObject.SetActive(false);
    }

    public async UniTask LoadAllAssetBundles()
    {
        foreach (AssetBundleStore.AssetName assetName in Enum.GetValues(typeof(AssetBundleStore.AssetName)))
        {
            await _store.LoadAssetBundle(assetName);
            Debug.Log(assetName);
        }
        Debug.Log("すべて読み込みました");
    }

    public async UniTask LoadNotLoadedData(AssetBundleStore.AssetName assetName)
    {
        await _store.LoadAssetBundle(assetName);
    }

    public void UnLoadAsset(AssetBundleStore.AssetName assetName)
    {
        _store[assetName].Unload(true);
    }
}

