using System.Collections.Generic;
using UnityEngine;

public abstract class WindowBase : MonoBehaviour
{
    [SerializeField]
    NavigationEntryPoint _navigationEntryPoint;
    [SerializeField]
    protected List<GameObject> _firstActiveObjectList = default;
    protected virtual void ActiveFirstObject()
    {
        foreach (var obj in _firstActiveObjectList)
        {
            obj.SetActive(true);
        }
    }
}
