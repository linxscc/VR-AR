using UnityEngine;
using System.Collections;

public class KeyBordControlARCapture : MonoBehaviour
{
    private AVProMovieCaptureBase movieCapture = null;

    private void Start()
    {
        movieCapture = GetComponent<AVProMovieCaptureBase>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Global.isCapturing)
            {
                ARCaptureAlert.showARAlert.SetValue("AR视频录制已开启...\n请通过“Space”空格键控制开关");
                movieCapture.StartCapture();
            }
            else
            {
                ARCaptureAlert.showARAlert.SetValue("AR视频录制已关闭...\n请通过“Space”空格键控制开关");
                movieCapture.StopCapture();
            }
            Global.isCapturing = !Global.isCapturing;
        }
    }
}
