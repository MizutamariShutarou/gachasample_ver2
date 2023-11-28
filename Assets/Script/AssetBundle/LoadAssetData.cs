using Cysharp.Threading.Tasks;
using UnityEngine;

//日本語対応
public class LoadAssetData
{
    private static LoadAssetData _instance = default;

    public static LoadAssetData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LoadAssetData();
            }
            return _instance;
        }
    }
    private AssetBundleStore _store = new AssetBundleStore();

    public AssetBundleStore Store => _store;

    // 読み込む必要があるAssetBundleを読み込む
    public async UniTask LoadAssetBundles(AssetsName assetName)
    {
        await _store.LoadAssetBundle(assetName);
    }

    public void UnLoadAsset(AssetsName assetName, bool flag)
    {
        _store[assetName].Unload(flag);
        Debug.Log($"{assetName}をUnLoadしました");
    }
}

