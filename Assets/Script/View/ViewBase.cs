using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//日本語対応
public abstract class ViewBase : MonoBehaviour
{
    [SerializeField]
    protected NavigationEntryPoint _navigationEntryPoint;

    protected void Initialize(Navigation.State state)
    {
        SetupState(state, this.GetCancellationTokenOnDestroy());
    }
    private void SetupState(Navigation.State state, CancellationToken ct)
    {
        // 画面遷移時の処理
        // モデルであるNavigationから通知を受けて
        // Viewを更新する処理を書く
        _navigationEntryPoint.Navigation.SetupState
        (
            state,
            popped => Debug.Log("OnEnter : " + state + (popped ? " (pop)" : "")),
            popped => EnterRoutine(state, popped, ct),
            popped => Debug.Log("OnExit : " + state + (popped ? " (pop)" : "")),
            popped => ExitRoutine(state, popped, ct)
        );
    }

    protected abstract UniTask EnterRoutine(Navigation.State state, bool popped, CancellationToken ct);

    protected abstract UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct);
}
