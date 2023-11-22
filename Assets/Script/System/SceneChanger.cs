using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//日本語対応
public class SceneChanger : MonoBehaviour
{
    static SceneChanger _instance;
    public static SceneChanger Instance => _instance;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public AsyncOperation ReturnAsyncOperation(string name)
    {
        var async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);

        return async;
    }
}
