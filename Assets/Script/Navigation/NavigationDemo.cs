using System.Collections;
using UnityEngine;

public class NavigationDemo : MonoBehaviour {

    [SerializeField]
    private Navigation _navigation;
    
    private void Start()
    {
        // 初期化
        _navigation.Initialize();

        // 各状態毎のふるまいを定義
        SetupState(Navigation.State.Home);
        SetupState(Navigation.State.GachaTop);
        SetupState(Navigation.State.GachaStaging);
        SetupState(Navigation.State.GachaResult);
    }

    private void SetupState(Navigation.State state)
    {
        // 画面遷移時の処理
        // モデルであるNavigationから通知を受けて
        // Viewを更新する処理を書く
        _navigation.SetupState
        (
            state,
            popped => Debug.Log("OnEnter : " + state + (popped ? " (pop)" : "")),
            popped => EnterRoutine(state, popped),
            popped => Debug.Log("OnExit : " + state + (popped ? " (pop)" : "")),
            popped => ExitRoutine(state, popped)
        );
    }
    
    private IEnumerator EnterRoutine(Navigation.State state, bool popped)
    {
        Debug.Log(state + " : ロード処理など" + (popped ? " (pop)" : ""));
        yield return new WaitForSeconds(1.0f);
        Debug.Log(state + " : ページに入るアニメーションなど" + (popped ? " (pop)" : ""));
        yield return new WaitForSeconds(1.0f);
    }

    private IEnumerator ExitRoutine(Navigation.State state, bool popped)
    {
        Debug.Log(state + " : ページがはけるアニメーションなど" + (popped ? " (pop)" : ""));
        yield return new WaitForSeconds(1.0f);
    }

    private void Update()
    {
        // デバッグ用にトリガーを呼ぶ
        // 本来は各画面からNavigation.ExecuteTrigger()を直接呼ぶ
        if (Input.GetKeyDown(KeyCode.G))
            _navigation.ExecuteTrigger(Navigation.Trigger.TapEnterGachaPage);
        if (Input.GetKeyDown(KeyCode.B))
            _navigation.ExecuteTrigger(Navigation.Trigger.PageBack);
        if (Input.GetKeyDown(KeyCode.H))
            _navigation.ExecuteTrigger(Navigation.Trigger.TapHomePage);
    }
}