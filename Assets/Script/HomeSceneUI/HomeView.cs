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
    protected override IEnumerator EnterRoutine(Navigation.State state, bool popped)
    {
        Debug.Log(state + " : ロード処理など" + (popped ? " (pop)" : ""));
        yield return new WaitForSeconds(1.0f);
        Debug.Log(state + " : ページに入るアニメーションなど" + (popped ? " (pop)" : ""));
        yield return new WaitForSeconds(1.0f);
    }

    protected override IEnumerator ExitRoutine(Navigation.State state, bool popped)
    {
        Debug.Log(state + " : ページがはけるアニメーションなど" + (popped ? " (pop)" : ""));
        yield return new WaitForSeconds(1.0f);
    }
}
