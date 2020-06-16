using UnityEngine;
using System.Collections;
using ModelViewerProject.UI;
using ModelViewerProject.Label3D;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class LabelUIEventArgs : System.EventArgs
{
    public bool Used { set; get; }
    public Vector3 Direction { set; get; }
    public Label3DHandler label3D { set; get; }

}
public class GridItem : MonoBehaviour
{
    public Label3DHandler label3D;
    public EventHandler onClick;

    /// <summary>
    ///所属子模型
    /// </summary>
    public List<GridItem> labelChild = new List<GridItem>();
    public void OnInit(Label3DHandler label3D)
    {
        this.label3D = label3D;
        switch (Global.labelDataList.controlType)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
               // Button.ButtonClickedEvent be = new Button.ButtonClickedEvent();
                transform.FindChild("Title/ItemButton").GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                EventTriggerListener.Get(transform.FindChild("Title/ItemButton").gameObject).onClick = OnClick;

                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
        // transform.FindChild("Title/ItemButton").GetComponent<Button>().onClick.RemoveAllListeners();
        ///this.uiCon = uiCon;
        //transform.FindChild("Title/ItemButton").GetComponent<Button>().onClick.AddListener(OnClick);
        //onClick += uiCon.Label2DUI_OnClick;


    }
    public void OnClick(GameObject obj)
    {
      //  print("click");
        ShowInformation.showInformation.SetValue(label3D.Title+"\n"+label3D.Description);
        LabelUIEventArgs e = new LabelUIEventArgs() { Direction = label3D.LocalPosition, label3D = this.label3D };
        if(onClick!=null)
        onClick(this, e);
    }
    public void OnClickUp()
    {

    }
    private void OnDisable()
    {
       // onClick -= uiCon.Label2DUI_OnClick;
    }
}
