using UnityEngine;
using System.Collections;
using PureMVCDemo;
using UnityEngine.UI;

public class ARCaptureButton : MonoBehaviour
{
    private AVProMovieCaptureBase moiveCapture = null;
    private Sprite StartCaptureSprite = null;
    private Sprite StopCaptureSprite = null;
    private Sprite CaptureButtonSprite = null;
    /// <summary>
    /// 初始化
    /// </summary>
    public void OnInit()
    {
        moiveCapture = GameObject.FindObjectOfType<AVProMovieCaptureBase>();
        StartCaptureSprite = GetComponent<Button>().spriteState.disabledSprite;
        StopCaptureSprite = GetComponent<Button>().spriteState.pressedSprite;
        EventTriggerListener.Get(gameObject).onClick = OnClick;
        EventTriggerListener.Get(gameObject).onEnter = OnEnter;
        EventTriggerListener.Get(gameObject).onExit = OnExit;
        ChangeCaptureSprite();
    }

    /// <summary>
    /// 判断是否录制
    /// </summary>
    private void ChangeCaptureSprite()
    {
        if (!Global.isCapturing)
        {
            GetComponent<Image>().sprite = StartCaptureSprite;
        }
        else
        {
            GetComponent<Image>().sprite = StopCaptureSprite;
        }
    }
    private void OnClick(GameObject o)
    {
        if (!Global.isCapturing)
        {
            GetComponent<Image>().sprite = StopCaptureSprite;
            moiveCapture.StartCapture();
        }
        else
        {
            GetComponent<Image>().sprite = StartCaptureSprite;
            moiveCapture.StopCapture();
        }
        Global.isCapturing = !Global.isCapturing;
    }

    private void OnEnter(GameObject o)
    {
        if (!Global.isCapturing)
            HintInfoControl.Singleton.Open("开始录制", transform.position.x, transform.position.y, 0);
        else
            HintInfoControl.Singleton.Open("停止录制", transform.position.x, transform.position.y, 0);
    }

    private void OnExit(GameObject o)
    {
        HintInfoControl.Singleton.Close();
    }

    private void OnDisable()
    {
        ARCaptureControl.Instance = null;
    }
}
