using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

//日本語対応
public class GachaTopScreen : MonoBehaviour, IScreenController
{
    [SerializeField]
    private Button _showCheckGachaButton = default;

    [SerializeField]
    private Button _doGachaButton = default;

    [SerializeField]
    private GameObject _gachaStagingScreen = default;

    [SerializeField]
    private GameObject _gachaResultScreen = default;

    [SerializeField]
    private Image _checkDoGachaPanel = default;

    [SerializeField]
    private LoadAssetData _loadAssetData = default;

    private CancellationTokenSource _cts = default;


    private void OnEnable()
    {
        Initialize();
    }
    private void OnDisable()
    {
        Release();
        _checkDoGachaPanel.gameObject.SetActive(false);
    }
    public void Initialize()
    {
        _cts = new CancellationTokenSource();
        _checkDoGachaPanel.gameObject.SetActive(false);
        _gachaStagingScreen.gameObject.SetActive(false);
        gameObject.SetActive(true);

        Subscribe();
    }
    public void Subscribe()
    {
        _showCheckGachaButton.onClick.AddListener(ShowCheckGachaPanel);
        _doGachaButton.onClick.AddListener(() => GoNext(_cts.Token));
    }

    public void Release()
    {
        _showCheckGachaButton.onClick.RemoveAllListeners();
        _doGachaButton.onClick.RemoveAllListeners();
        _cts?.Cancel();
    }

    public void BackPrevious()
    {

    }
    private void ShowCheckGachaPanel()
    {
        _checkDoGachaPanel.gameObject.SetActive(true);
    }

    public async void GoNext(CancellationToken cancellationToken)
    {
        gameObject.SetActive(false);
        _gachaStagingScreen.SetActive(true);
        await _loadAssetData.DataPreparation(cancellationToken);
        // await UniTask.Delay(TimeSpan.FromSeconds(1));
        await UniTask.CompletedTask;
    }

    void IScreenController.GoNext()
    {
        throw new NotImplementedException();
    }
}
