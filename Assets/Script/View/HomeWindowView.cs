using System.Collections.Generic;
using UnityEngine;

//日本語対応
public class HomeWindowView : WindowBase
{
    private void OnEnable()
    {
        ActiveFirstObject();
    }
    protected override void ActiveFirstObject()
    {
        foreach (var obj in _firstActiveObjectList)
        {
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetActive(true);
            }
            obj.SetActive(true);
        }
    }
}
