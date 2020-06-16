using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Button button;
    private void Start()
    {
        button.onClick.AddListener(Click);
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), ""))
        {

        }
    }
    private void Click()
    {
        print("ss");
    }
}
