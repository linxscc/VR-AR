using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour {

    public Transform backGrround;
    public Transform mainCamera;
    public Transform startCanvas;
    public GameObject dirLight;
    public GameObject modelBrowserBG;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        // startCanvas = transform.Find("StartCanvas");
    }
    public void Close(bool isClose = false)
    {
        //print("123");
        // backGrround.parent = null;
        backGrround.transform.localScale =new Vector3(0.00001f,0.00001f,0.0001f);
        //backGrround.transform.localScale = Vector3.zero;
        dirLight.SetActive(isClose);
       modelBrowserBG.SetActive(isClose);
        mainCamera.GetComponent<Camera>().enabled = isClose;
    }
    public void Open()
    {
        backGrround.transform.localScale = Vector3.one;
        // backGrround.parent = startCanvas;
        backGrround.localPosition = Vector3.zero;
        //backGrround.localScale = Vector3.one;
        backGrround.SetSiblingIndex(0);
        //canvas.GetComponent<Canvas>().enabled = true;
        mainCamera.GetComponent<Camera>().enabled = true;
        dirLight.SetActive(true);
        modelBrowserBG.SetActive(true);
    }
}
