using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ARCaptureAlert : MonoBehaviour
{
    public static ARCaptureAlert showARAlert;
    private Text text;
    public float timer = 2f;
    void Start()
    {
        showARAlert = this;
        text = GetComponentInChildren<Text>();
        if (Global.isCapturing)
        {
            text.text = "AR视频录制已开启...\n请通过“Space”空格键控制开关";
            StartCoroutine(HideAlertText());
        }
        else
        {
            text.text = "AR视频录制尚未开启...\n请通过“Space”空格键控制开关";
            StartCoroutine(HideAlertText());
        }
    }
    public void SetValue(string txt)
    {
        gameObject.SetActive(true);
        text.text = txt;
        StartCoroutine(HideAlertText());
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator HideAlertText()
    {
        float hideTime = 0;
        while (true)
        {
            hideTime += Time.deltaTime;
            if (hideTime > timer)
            {
                Close();
            }
            yield return 0;
        }
    }
}
