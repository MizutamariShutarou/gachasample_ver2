using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

//日本語対応
public abstract class ViewBase : MonoBehaviour
{
    NavigationEntryPoint _navigationEntryPoint;
    protected void Initialize(Navigation.State state, NavigationEntryPoint navigationEntryPoint)
    {
        _navigationEntryPoint = navigationEntryPoint;
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
            popped => OnEnter(state, popped, ct),
            popped => EnterRoutine(state, popped, ct),
            popped => OnExit(state, popped, ct),
            popped => ExitRoutine(state, popped, ct)
        );
    }
    protected abstract UniTask OnEnter(Navigation.State state, bool popped, CancellationToken ct);

    protected abstract UniTask EnterRoutine(Navigation.State state, bool popped, CancellationToken ct);

    protected abstract UniTask ExitRoutine(Navigation.State state, bool popped, CancellationToken ct);

    protected abstract UniTask OnExit(Navigation.State state, bool popped, CancellationToken ct);

    protected abstract void OnActive(bool flag);
}
