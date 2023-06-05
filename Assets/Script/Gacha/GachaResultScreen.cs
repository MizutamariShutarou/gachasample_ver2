using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

//日本語対応
public class GachaResultScreen : MonoBehaviour, IScreenController
{
    [SerializeField]
    private List<Image> _imagesList = default;

    [SerializeField]
    private Button _skipButton = default;

    [SerializeField]
    private Button _goHome = default;

    [SerializeField]
    private float _firstAwaitTime = 0.5f;

    [SerializeField]
    private float _awaitTime = default;

    [SerializeField]
    private LoadAssetData _loadAssetData = default;

    [SerializeField]
    private Canvas _firstCanvas = default;

    [SerializeField]
    GameObject _firstWeaponObj = default;

    [SerializeField]
    GameObject _gachaTopScreen = default;

    private CancellationTokenSource _cts = default;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Release();
    }
    public async void Initialize()
    {
        _cts = new CancellationTokenSource();
        _awaitTime = _firstAwaitTime;
        _skipButton.gameObject.SetActive(true);
        _firstCanvas.gameObject.SetActive(false);
        _goHome.gameObject.SetActive(false);
        Subscribe();
        await ShowResult(_cts.Token);
    }
    public void Subscribe()
    {
        _skipButton.onClick.AddListener(Skip);
        _goHome.onClick.AddListener(GoNext);
    }

    public void Release()
    {
        _skipButton.onClick.RemoveAllListeners();
        _goHome.onClick.RemoveAllListeners();
        _cts?.Cancel();
    }

    public void BackPrevious()
    {
        Destroy(gameObject);
    }

    public void GoNext()
    {
        gameObject.SetActive(false);
        _gachaTopScreen.SetActive(true);
        _loadAssetData.UnLoadAsset();
    }

    private async UniTask ShowResult(CancellationToken cancellationToken)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        for (int i = 0; i < _loadAssetData.Num; i++)
        {
            if (!_loadAssetData.WeaponDataList[i]._isFirst)
            {
                Debug.Log(_loadAssetData.WeaponDataList[i]._isFirst);
                _loadAssetData.WeaponDataList[i]._isFirst = true;
             
                _firstWeaponObj = _loadAssetData.GameObjectsList[i].gameObject;

                await ShowFirstStage(cancellationToken);
            }
            _imagesList[i].sprite = _loadAssetData.SpritesList[i];
            Debug.Log(_loadAssetData.SpritesList[i]);
            await UniTask.Delay(TimeSpan.FromSeconds(_awaitTime));
        }
        _goHome.gameObject.SetActive(true);
    }

    private async UniTask ShowFirstStage(CancellationToken ct)
    {
        _firstCanvas.gameObject.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        var obj = Instantiate(_firstWeaponObj, new Vector3(0, 0, 10), Quaternion.identity);
        obj.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        Destroy(obj);
        _firstCanvas.gameObject.SetActive(false);
    }

    public void Skip()
    {
        _awaitTime = 0.2f;
        _skipButton.gameObject.SetActive(false);
    }

}
