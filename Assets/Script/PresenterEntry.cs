using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//日本語対応
public class PresenterEntry : MonoBehaviour
{
    [SerializeField]
    private BasePresenter[] presenters;
    void Awake()
    {
        // 1. 初期化処理
        foreach (var presenter in presenters)
        {
            presenter.Initialize();
        }

        // 2. 購読処理
        foreach (var presenter in presenters)
        {
            presenter.Subscribe();
        }
    }
}
