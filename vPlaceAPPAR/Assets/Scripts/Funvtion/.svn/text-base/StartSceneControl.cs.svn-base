using PlaceAR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vPlace_zpc;
/// <summary>
/// 控制开始场景
/// </summary>
public class StartSceneControl
{
    public GameObject startObj;
    public StartScene startScene;
    private StartSceneControl()
    {
        GameObject obj = Resources.Load<GameObject>(Global.startScene);
        startObj = GameObject.Instantiate(obj);
        startScene = startObj.GetComponent<StartScene>();
        //DontDestroyOnLoad(startObj);
        
    }
    private static StartSceneControl singleton;
    public static StartSceneControl Singleton
    {
        get
        {
            if (singleton == null)
                singleton = new StartSceneControl();
            return singleton;
        }
    }
    public void Open()
    {
        startScene.Open();
        ScrollMenuControl.Singleton.Close(null);
        BrowserTypeViewContol.Instance.Close();
        
        //startObj.transform.localScale=Vector3.one;
    }
    public void Close(bool isClose = false)
    {
        startScene.Close(isClose);
        //startObj.transform.localScale = Vector3.zero;
        //startObj.SetActive(false);
    }
}

