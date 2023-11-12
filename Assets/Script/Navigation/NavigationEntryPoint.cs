using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class NavigationEntryPoint : MonoBehaviour {

    [SerializeField]
    private Navigation _navigation;

    public Navigation Navigation => _navigation;
    
    private void Awake()
    {
        // 初期化
        _navigation.Initialize();
    }
    private async void Update()
    {
        // デバッグ用にトリガーを呼ぶ
        // 本来は各画面からNavigation.ExecuteTrigger()を直接呼ぶ
        if (Input.GetKeyDown(KeyCode.G))
            await _navigation.ExecuteTrigger(Navigation.Trigger.TapEnterGachaPage);
        if (Input.GetKeyDown(KeyCode.B))
            await _navigation.ExecuteTrigger(Navigation.Trigger.PageBack);
        if (Input.GetKeyDown(KeyCode.H))
            await _navigation.ExecuteTrigger(Navigation.Trigger.TapHomePage);
    }
}