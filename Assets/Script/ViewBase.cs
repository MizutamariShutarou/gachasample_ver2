using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//日本語対応
public abstract class ViewBase : MonoBehaviour
{
    [SerializeField]
    private Navigation _navigation;

    protected void Initialize(Navigation.State state)
    {
        // 初期化
        _navigation.Initialize();

        // 各状態毎のふるまいを定義
        SetupState(state);
    }

    private void SetupState(Navigation.State state)
    {
        // 画面遷移時の処理
        _navigation.SetupState
        (
            state,
            popped => Debug.Log("OnEnter : " + state + (popped ? " (pop)" : "")),
            popped => EnterRoutine(state, popped),
            popped => Debug.Log("OnExit : " + state + (popped ? " (pop)" : "")),
            popped => ExitRoutine(state, popped)
        );
    }
    protected abstract UniTask EnterRoutine(Navigation.State state, bool popped);

    protected abstract UniTask ExitRoutine(Navigation.State state, bool popped);
}
