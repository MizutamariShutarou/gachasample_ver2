using System.IO;
using UnityEngine;
using UnityEngine.UI;
using StoringPlace;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

//日本語対応
public class LoadAssets : MonoBehaviour
{
    [SerializeField]
    private List<Image> _imageList = default;

    AsstsBundles _assetsBundles = default;

    [SerializeField]
    List<ItemDataBase> _armorData = default;
    private void Start()
    {
        _assetsBundles = new AsstsBundles();

        if (_assetsBundles == null)
        {
            Debug.Log("AssetBundleのロードに失敗しました");

            return;
        }
    }

    private async void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            for (int i = 0; i < 10; i++)
            {
                var randomNom = Random.Range(0, _armorData.Count);

                var sprite = _assetsBundles.Armor.LoadAsset<Sprite>(_armorData[randomNom]._iconName);

                _imageList[i].sprite = sprite;
                await UniTask.DelayFrame(200);
            }
            _assetsBundles.Armor.Unload(false);
        }
    }
}
