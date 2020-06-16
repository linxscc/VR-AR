using UnityEngine;
using System.Collections;
/// <summary>
/// 设置菜单
/// </summary>
public class SetMenuControl
{
    private GameObject menu;
    private GameObject canvas;
    public float eyeDistance;
    public float mDistance;
    public float point;
    public CallBack open;
    public CallBack close;
    public SetMenu setMenu;
    private SetMenuControl()
    {
        canvas = GameObject.FindGameObjectWithTag(Tag.mainUI);
        menu = Resources.Load<GameObject>(Global.seMenuUrl);
        menu = GameObject.Instantiate(menu);
        setMenu=menu.GetComponent<SetMenu>() ;
        setMenu.OnInit();
        close += setMenu.Close;
        open += setMenu.Open;
        eyeDistance = setMenu.EyeDistance;
        mDistance = setMenu.MDistance;
        point = setMenu.Point;
        menu.transform.parent = canvas.transform;
        menu.transform.localPosition = Vector3.zero;
        menu.transform.localScale = new Vector3(0.1f,0.1f,1);
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

    }
    public void Close()
    {

    }
}
