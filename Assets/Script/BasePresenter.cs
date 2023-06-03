using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//日本語対応
public abstract class BasePresenter : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void Subscribe();
    public abstract void Release();
}
