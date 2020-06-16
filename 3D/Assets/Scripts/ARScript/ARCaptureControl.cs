using UnityEngine;
using System.Collections;

public class ARCaptureControl
{
    public GameObject captureButton;
    public ARCaptureButton ARCaptureButton;

    private GameObject canvas = null;
    private ARCaptureControl()
    {
        canvas = GameObject.FindGameObjectWithTag(Tag.mainUI);
        captureButton = Resources.Load<GameObject>(Global.ARCAPTUREBUTTONURL);
        if (captureButton != null)
            captureButton = GameObject.Instantiate(captureButton);
        else
            Debug.Log("AR录屏预设不存在");
        captureButton.transform.SetParent(canvas.transform);
        ARCaptureButton = captureButton.GetComponentInChildren<ARCaptureButton>();
        captureButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0f);
        captureButton.transform.localScale = Vector3.one;
        ARCaptureButton.OnInit();
    }
    private static ARCaptureControl instance;
    public static ARCaptureControl Instance
    {
        get
        {
            if (instance == null)
                instance = new ARCaptureControl();
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    public void Open()
    {

    }
    public void Close()
    {

    }
}
