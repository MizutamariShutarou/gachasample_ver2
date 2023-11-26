using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//日本語対応
public class AssetBundleStore
{
    private Dictionary<int, AssetBundle> _bundleStore = new Dictionary<int, AssetBundle>();

    public AssetBundle this[AssetsName name] => _bundleStore[(int)name];

    public async UniTask LoadAssetBundle(AssetsName key)
    {
        var assetBundleRequest
            = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, key.ToString()));

        await UniTask.WaitUntil(() => assetBundleRequest.isDone);
        _bundleStore[(int)key] = assetBundleRequest.assetBundle;
    }
}
