using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    void Start()
    {
        var uiPrefab = Resources.Load<GameObject>("Prefabs/UI");
        var uiInstance = Object.Instantiate(uiPrefab);
        var uiView = uiInstance.GetComponent<UIView>();
        var game = new Game(uiView);
    }
}
