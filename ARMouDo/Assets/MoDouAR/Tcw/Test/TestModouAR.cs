using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoDouAR;
using UnityEngine.UI;
using Tools_XYRF;


public class TestModouAR : MonoBehaviour
{
    public List<Texture2D> tex = new List<Texture2D>();
    public Image s;

    private void OnEnable()
    {
        ComeBack.OnSaveLoadTextureSuccess += ComeBack_OnSaveLoadTextureSuccess;
        ComeBack.OnSaveLoadTextureLost += ComeBack_OnSaveLoadTextureLost;
    }

    private void ComeBack_OnSaveLoadTextureLost(FileTextureResult obj)
    {
        Debug.Log(obj.GetState);
        Debug.Log(obj.GetMessage);
    }

    private void ComeBack_OnSaveLoadTextureSuccess(FileTextureResult obj)
    {
        switch (obj.GetState)
        {
            case FileTextureState.None:
                break;
            case FileTextureState.SaveSuccess:
                break;
            case FileTextureState.SaveLost:
                break;
            case FileTextureState.LoadSucessSingle:
                //    Debug.Log(obj.GetState + ":  " + obj.GetLoadTime);
                break;
            case FileTextureState.LoadLostSingle:
                break;
            case FileTextureState.LoadSucessAll:
                break;
            case FileTextureState.LoadLostAll:
                break;
            case FileTextureState.ReadTextureSucess:
                //      Debug.Log(obj.GetState + ":  " + obj.GetLoadTime);
                break;
            case FileTextureState.ReadTextureLost:
                break;
        }
    }

    private void OnDisable()
    {
        ComeBack.OnSaveLoadTextureSuccess -= ComeBack_OnSaveLoadTextureSuccess;
        ComeBack.OnSaveLoadTextureLost -= ComeBack_OnSaveLoadTextureLost;
    }
    private void OnGUI()
    {
        //number = GUI.TextField(new Rect(50, 50, 150, 100), number);
        //code = GUI.TextField(new Rect(50, 150, 150, 100), code);
        //password = GUI.TextField(new Rect(50, 250, 150, 100), password);
        if (GUI.Button(new Rect(500, 150, 200, 80), "发送验证码"))
        {

        }
        if (GUI.Button(new Rect(500, 230, 200, 80), "验证码登录"))
        {

        }
        if (GUI.Button(new Rect(500, 310, 200, 80), "账号密码登录"))
        {

        }
        if (GUI.Button(new Rect(500, 390, 200, 80), "重置密码"))
        {

        }

        //if (GUI.Button(new Rect(100, 50, 200, 120), "截图图片"))
        //{
        //    Texture2D te = FileTexture.ReadTexture(Application.persistentDataPath, tex[1].name, TextureSuffixs.JPG, Screen.width, Screen.height);
        //    int a = 0;
        //    if (te.height >= te.width)
        //    {
        //        a = (Screen.height - Screen.width) / 2;
        //    }
        //    else
        //        a = (Screen.width - Screen.height) / 2;
        //    uis[0].GetComponent<Image>().sprite = FileTexture.SpriteFromTex2D(FileTexture.SetS(te, 0, a, 1024));
        //}

        //if (GUI.Button(new Rect(100, 50, 200, 120), "截图"))
        //{
        //    Singleton<ScreenShotFun>.Instance.ScreenShotCapture(new Rect(0, 0, Screen.width, Screen.height), s, delegate { Debug.Log("截图成功!"); }, delegate { });
        //}
        //if (GUI.Button(new Rect(100, 170, 200, 120), "保存+写入数据"))
        //{
        //    Singleton<ScreenShotFun>.Instance.ScreenShotSave(delegate { Debug.Log("保存成功!"); }, delegate { });
        //}

    }
}

