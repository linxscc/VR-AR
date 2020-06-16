using UnityEngine;
using System.Collections;
using EasyAR;

public class ARStartup : MonoBehaviour
{
    //AR虚拟相机
    public GameObject ArCamera = null;

    /// <summary>
    /// 初始化
    /// </summary>
    public void OnInit()
    {
        DontDestroyOnLoad(this);
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.eulerAngles = Vector3.zero;
        LoadStartSceneSetCameraPos();
    }

    /// <summary>
    /// 跳Main场景时给相机重新赋值
    /// </summary>
    public void LoadMainSceneSetCameraPos()
    {
        ArCamera.transform.localEulerAngles = new Vector3(311.3233f, 87.8862f, 270);
        // ArCamera.transform.localPosition = new Vector3(-14.08f, -9.59f, 19.7f);
        ArCamera.transform.localPosition = new Vector3(-3.579999f, -5.990505f, 19.92139f);
        ArCamera.transform.localScale = Vector3.one;
    }

    /// <summary>
    /// 跳Start场景时给相机重新赋值
    /// </summary>
    public void LoadStartSceneSetCameraPos()
    {
        ArCamera.transform.localEulerAngles = new Vector3(313.4411f, 88.36289f, 270);
       // ArCamera.transform.localPosition = new Vector3(-14.08f, -9.59f, 19.7f);
        ArCamera.transform.localPosition = new Vector3(-3.819985f, -5.940525f, 19.8212f);
        ArCamera.transform.localScale = Vector3.one;
    }
}
