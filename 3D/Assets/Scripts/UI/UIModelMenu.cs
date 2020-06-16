using UnityEngine;
using System.Collections;
using ModelViewerProject.Model;
using UnityEngine.UI;

public class UIModelMenu : MonoBehaviour
{

    [SerializeField]
    private ModelController modelController;
    /// <summary>
    /// 预设拆解
    /// </summary>
    private GameObject assemble;
    /// <summary>
    /// 复位
    /// </summary>
    private GameObject restore;
    private Transform gridTile;
    /// <summary>
    /// 2d3d按钮
    /// </summary>
    [SerializeField]
    private GameObject set2D3D;
    //private GameObject assemble;
    //private bool isAssemble = false;
    private void Start()
    {
        gridTile = transform.FindChild("GridTitle");
        assemble = transform.FindChild("AssembleDis").gameObject;
        restore = transform.FindChild("Restore").gameObject;
        EventTriggerListener.Get(restore).onClick = BackHome;
        EventTriggerListener.Get(assemble).onClick = OnAssembleDis;
        EventTriggerListener.Get(set2D3D).onClick = Set2D3D;
        if (Global.labelDataList.controlType == 0)
        {
            assemble.GetComponentInChildren<Text>().text = "预设拆解(合)";
        }
        else if (Global.labelDataList.controlType == 2)
        {
            assemble.GetComponentInChildren<Text>().text = "逐层浏览";
        }
        gridTile.GetComponentInChildren<Text>().text = Global.labelDataList.transform.Name;
        if (Global.is2D)
        {
            set2D3D.transform.FindChild("Button3D").gameObject.SetActive(false);
            
        }
        else
        {
            set2D3D.transform.FindChild("Button3D").gameObject.SetActive(true);
           
        }
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
    /// 复位
    /// </summary>
    /// <param name="obj"></param>
    public void BackHome(GameObject obj)
    {
        ShowInformation.showInformation.Close();
        modelController.BackHome();
        modelController.OnAssemble();
        Global.isAssemble = false;
    }
    public void BackStartScene()
    {

    }
    /// <summary>
    /// 预设拆解/逐层/剖面按钮
    /// </summary>
    /// <param name="obj"></param>
    public void OnAssembleDis(GameObject obj)
    {
        if (Global.labelDataList.controlType == 0)
        {
            if (Global.isAssemble)
            {
                modelController.OnAssemble();
                assemble.GetComponentInChildren<Text>().text = "预设拆解(合)";
            }
            else
            {
                modelController.OnDisassemble();
                assemble.GetComponentInChildren<Text>().text = "预设拆解(分)";
            }
        }
        else if (Global.labelDataList.controlType == 2)
        {
            modelController.MenuButtonDefault();
        }
        else if (Global.labelDataList.controlType == 1)
        {
            modelController.Profile();
        }
        Global.isAssemble = !Global.isAssemble;
    }
}
