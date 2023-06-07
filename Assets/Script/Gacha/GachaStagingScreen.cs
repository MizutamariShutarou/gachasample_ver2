using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;
using System;

//日本語対応
public class GachaStagingScreen : MonoBehaviour, IScreenController
{
    [SerializeField]
    private Button _tapButton = default;

    [SerializeField]
    private Animation _gachaAnim = default;

    private CancellationTokenSource _cts = default;

    [SerializeField]
    private LoadAssetData _loadAssetData = default;

    [SerializeField]
    private GameObject _gachaResultScreen = default;

    [SerializeField]
    private GachaTopScreen _gachaTopScreen = default;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Release();
        
    }
    public void Initialize()
    {
        _cts = new CancellationTokenSource();
        _gachaAnim.gameObject.SetActive(false); 
        _tapButton.gameObject.SetActive(true);
        Subscribe();
    }
    public void BackPrevious()
    {
        throw new System.NotImplementedException();
    }
    public async void GoNext()
    {
        // await _loadAssetData.DataPreparation();

        _gachaAnim.gameObject.SetActive(true);

        _tapButton.gameObject.SetActive(false);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        await ActiveGachaAnim(_cts.Token);
        await _gachaTopScreen.GoNext(_cts.Token);
    }
    public void Subscribe()
    {
        _tapButton.onClick.AddListener(GoNext);
    }

    public void Release()
    {
        _tapButton.onClick.RemoveAllListeners();
        _cts?.Cancel();
    }

    private async UniTask ActiveGachaAnim(CancellationToken cancellationToken)
    {
        // _tapButton.gameObject.SetActive(false);
        _gachaAnim.gameObject.SetActive(true);
        await _loadAssetData.LoadAssets(cancellationToken);
        gameObject.SetActive(false);
        _gachaResultScreen.SetActive(true);
    }
}