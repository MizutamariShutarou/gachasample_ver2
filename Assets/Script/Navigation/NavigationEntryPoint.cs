using UnityEngine;

public class NavigationEntryPoint : MonoBehaviour
{

    [SerializeField]
    private Navigation _navigation;

    public Navigation Navigation => _navigation;

    private void Awake()
    {
        // 初期化
        _navigation.Initialize();
    }
}