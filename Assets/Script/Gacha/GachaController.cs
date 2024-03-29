using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

//日本語対応
public class GachaController : MonoBehaviour
{
    [SerializeField]
    private List<ItemDataBase> _weaponDataList = default;

    [SerializeField]
    private int _maxEmissionNum = 10;

    private List<Sprite> _spritesList = new List<Sprite>();

    public int MaxEmissionNum => _maxEmissionNum;

    public List<Sprite> SpritesList => _spritesList;

    public async UniTask LoadGachaData(AssetsName assetName)
    {
        for (int i = 0; i < _maxEmissionNum; i++)
        {
            var randomNom = Random.Range(0, _weaponDataList.Count);

            var sprite = LoadAssetData.Instance.Store[assetName].LoadAsset<Sprite>(_weaponDataList[randomNom]._iconName);

            _spritesList.Add(sprite);
        }
        await UniTask.CompletedTask;
    }
    public async UniTask DataPreparation(AssetsName assetName)
    {
        if (LoadAssetData.Instance.Store[assetName] == null)
        {
            Debug.Log("データがないためダウンロードします");
            await LoadAssetData.Instance.LoadAssetBundles(assetName);
            Debug.Log("ダウンロード完了");
        }
    }
    
    public void ClearList()
    {
        _spritesList.Clear();
    }
}
