using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

//日本語対応
public class TitleManager : MonoBehaviour, ISubscribe
{
    [SerializeField]
    private Canvas _titleCanvas = default;

    [SerializeField]
    private Button _titleButton = default;

    private void Awake()
    {
        Subscribe();
    }
    private void Start()
    {
        _titleCanvas.gameObject.SetActive(true);
    }
    public void Subscribe()
    {
        _titleButton.onClick.AddListener(() => TransitionHomeScene());
    }
    public void Release()
    {
        _titleButton.onClick.RemoveAllListeners();
    }

    private async void TransitionHomeScene()
    {
        _titleCanvas.gameObject.SetActive(false);
        LoadingManager.Instance.ActiveLoadingWindow(true);

        var async = SceneChanger.Instance.ReturnAsyncOperation("DemoHomeScene");

        async.allowSceneActivation = false;

        //AssetBundleよみこみ

        await Load(async, this.GetCancellationTokenOnDestroy());

        await UniTask.Delay(TimeSpan.FromSeconds(2f), false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());

        LoadingManager.Instance.ActiveLoadingWindow(false);
        Debug.Log("読み込みを完了しました");
        async.allowSceneActivation = true;
        Debug.Log("遷移完了");
    }

    private async UniTask Load(AsyncOperation async, CancellationToken ct)
    {
        while (async.progress < 0.9f)
        {
            await LoadingManager.Instance.ChangeSliderValue(async.progress, ct);
        }
        await UniTask.Delay(TimeSpan.FromSeconds(1f), false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
        await LoadingManager.Instance.ChangeSliderValue(1f, ct);
    }
}
