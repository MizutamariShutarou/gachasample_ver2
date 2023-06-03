using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

//日本語対応
public class MainUIModel
{
    private ReactiveProperty<GameObject> _nowWindow = default;

    public IReactiveProperty<GameObject> NowWindow => _nowWindow;
    
    public MainUIModel(GameObject nowWindow)
    {
        _nowWindow = new ReactiveProperty<GameObject>(nowWindow);
    }
}
