using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

//日本語対応
public class MainUIPresenter : BasePresenter
{
    [SerializeField]
    private MainUIView _mainUIView;

    public override void Initialize()
    {
        _mainUIView.Initialize();
    }

    public override void Subscribe()
    {
        _mainUIView.HomeButton.onClick.AddListener(OnHomeButtonClicked);
        // _mainUIView.QuestButton.onClick.AddListener(OnQuestButtonClicked);
        _mainUIView.GachaButton.onClick.AddListener(OnGachaButtonClicked);
    }

    public override void Release()
    {
        _mainUIView.HomeButton.onClick.RemoveAllListeners();
        // _mainUIView.QuestButton.onClick.RemoveAllListeners();
        _mainUIView.GachaButton.onClick.RemoveAllListeners();
    }

    private void OnHomeButtonClicked()
    {
        if(_mainUIView.NowWindow == _mainUIView.HomeWindow )
        {
            return;
        }
        _mainUIView.OnChangeWindow(_mainUIView.NowWindow, _mainUIView.HomeWindow);
    }

    //private void OnQuestButtonClicked()
    //{
    //    if (_mainUIView.NowWindow == _mainUIView.HomeWindow)
    //    {
    //        return;
    //    }
    //    _mainUIView.OnChangeWindow(_mainUIView.NowWindow, _mainUIView.HomeWindow);
    //}

    private void OnGachaButtonClicked()
    {
        if(_mainUIView.NowWindow == _mainUIView.GachaWindow )
        {
            return;
        }
        _mainUIView.OnChangeWindow(_mainUIView.NowWindow, _mainUIView.GachaWindow);
    }
}
