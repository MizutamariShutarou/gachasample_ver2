using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;

//日本語対応
//TODO:Subscribe処理等をどこに持たせるか
public class HomeView : ViewBase
{
    private void Start()
    {
        Initialize(Navigation.State.Home);
    }
    protected override async UniTask EnterRoutine(Navigation.State state, bool popped)
    {
        Debug.Log(state + " : ロード処理など" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        Debug.Log(state + " : ページに入るアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
    }

    protected override async UniTask ExitRoutine(Navigation.State state, bool popped)
    {
        Debug.Log(state + " : ページがはけるアニメーションなど" + (popped ? " (pop)" : ""));
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
    }
}
