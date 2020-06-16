using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 设置菜单
/// </summary>
public class SetMenuControl
{
    public bool isOpen = false;
    private GameObject menu;
    private GameObject canvas;
    /// <summary>
    /// 汇聚点
    /// </summary>
    public Slider point;
    /// <summary>
    /// 瞳间距
    /// </summary>
    public Slider eyeDistance;
    public Button set2D3D;
    public CallBack<GameObject> open;
    public CallBack<GameObject> close;
    public SetMenu setMenu;
    private SetMenuControl()
    {
        canvas = GameObject.FindGameObjectWithTag(Tag.mainUI);
        menu = Resources.Load<GameObject>(Global.seMenuUrl);
        menu = GameObject.Instantiate(menu);
        setMenu = menu.GetComponent<SetMenu>();
        point = setMenu.point;
        point.onValueChanged.AddListener(Point);
        set2D3D = setMenu.set2D3D;
        EventTriggerListener.Get(set2D3D.gameObject).onClick = Set2D3D;
       // set2D3D.onClick.AddListener(Set2D3D);
        eyeDistance = setMenu.eyeDistance;
        eyeDistance.onValueChanged.AddListener(EyeDistance);
   
        setMenu.OnInit();
        close += setMenu.Close;
        open += setMenu.Open;
        menu.transform.parent = canvas.transform;
        menu.transform.localPosition = Vector3.zero;
        menu.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        SetPointMaxMin();
        if (Global.is2D)
        {
            set2D3D.transform.FindChild("Button3D").gameObject.SetActive(false);
        }
        else
        {
            set2D3D.transform.FindChild("Button3D").gameObject.SetActive(true);
        }
    }
    public void PointControl(float value)
    {
        if (!isOpen) return;
        point.value += value*5;

    }
    public void EyeDistanceControl(float value)
    {
        if (!isOpen) return;
        eyeDistance.value += value*0.1f;
    }
    /// <summary>
    /// 2d3d切换
    /// </summary>
    public void Set2D3D(GameObject obj)
    {
        if (Global.is2D)
        {
            set2D3D.transform.FindChild("Button3D").gameObject.SetActive(false);
            StereoControl.Singleton.stereoCam.stereo = StereoModes.SideBySide;
        }
        else
        {
            set2D3D.transform.FindChild("Button3D").gameObject.SetActive(true);
            StereoControl.Singleton.stereoCam.stereo = StereoModes.Disabled;
        }   
        Global.is2D = !Global.is2D;
    }
    /// <summary>
    /// 调整汇聚点
    /// </summary>
    /// <param name="p"></param>
    public void Point(float p)
    {
        StereoControl.Singleton.stereoCam.parallaxDistance = p;
    }
    /// <summary>
    /// 调整瞳间距
    /// </summary>
    /// <param name="p"></param>
    public void EyeDistance(float p)
    {
        //Debug.Log(p);
        if (StereoControl.Singleton.stereoCam != null)
            StereoControl.Singleton.stereoCam.eyeDistance = p;
    }
    public void SetPointMaxMin()
    {
        setMenu.point.maxValue = 5f;
        setMenu.point.minValue = 0.2f;
        setMenu.eyeDistance.maxValue = 0.1f;
        setMenu.eyeDistance.minValue = 0.01f;
    }
    private static SetMenuControl singleton;
    public static SetMenuControl Singleton
    {
        get
        {
            if (singleton == null)
                singleton = new SetMenuControl();
            return singleton;
        }
        set
        {
            singleton = value;
        }
    }
    public void Open()
    {
        if (isOpen) return;
        if (open != null)
            open(null);
        isOpen = true;
    }
    /// <summary>
    /// 手柄控制
    /// </summary>
    public void TriggerControl()
    {

        if (isOpen)
            Close();
        else
            Open();
        //isOpen = !isOpen;
    }
    public void Close()
    {
        if (!isOpen) return;
        if (close != null)
            close(null);
        isOpen = false;
    }
}
