using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using ModelViewerProject.Label3D;
using UnityEngine.SceneManagement;

public class UIFirstScnenButton : MonoBehaviour, IPointerClickHandler
{
    private string modelName;
    public LabelDataList labelDataList;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnInIt(LabelDataList labelDataList,string modelName)
    {
        this.labelDataList = labelDataList;
        this.modelName = modelName;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Global.modelName = modelName;
        Global.labelDataList = labelDataList;
        //Application.LoadLevel(1);
        SceneManager.LoadScene("MainScene");
    }
}
