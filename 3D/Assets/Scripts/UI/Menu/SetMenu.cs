using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using PureMVCDemo;
/// <summary>
/// 设置菜单
/// </summary>
public class SetMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject close;
    [SerializeField]
    private GameObject open;
    [SerializeField]
    private Transform menu;
    /// <summary>
    /// 汇聚点
    /// </summary>
    public Slider point;
    /// <summary>
    /// 瞳间距
    /// </summary>
    public Slider eyeDistance;
    public Button set2D3D;
    [SerializeField]
    private GameObject closeProg;
    [SerializeField]
    private GameObject backGround;

    /// <summary>
    /// 关闭
    /// </summary>
    public void Close(GameObject o)
    {
        
        //Tweener t = menu.DOLocalMove(new Vector3(-7, -7, 0), 0.3f);
        Tweener t = menu.DOScale(Vector3.zero, Global.animationLength);
        t.OnComplete(Complete);
        SetMenuControl.Singleton.isOpen = false;

    }
    /// <summary>
    /// 开启
    /// </summary>
    public void Open(GameObject o)
    {
       
        //menu.DOLocalMove(Vector3.zero, 0.3f);
        Tweener t = menu.DOScale(new Vector3(0.1f, 0.1f, 1f), Global.animationLength);
        SetMenuControl.Singleton.isOpen = true;
        close.SetActive(true);
        //open.SetActive(false);
        backGround.SetActive(true);

    }
    /// <summary>
    /// 退出按钮
    /// </summary>
    /// <param name="o"></param>
    public void CloseProg(GameObject o)
    {
        QuitProcedure.Quit();
       // Close(null);
    }
    /// <summary>
    /// 关闭回调
    /// </summary>
    private void Complete()
    {
        close.SetActive(false);
        //open.SetActive(true);
        backGround.SetActive(false);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void OnInit()
    {
        EventTriggerListener.Get(close).onClick = Close;
        EventTriggerListener.Get(open).onClick = Open;
        EventTriggerListener.Get(closeProg).onClick = CloseProg;
        EventTriggerListener.Get(close).onEnter = OnEnter;
        EventTriggerListener.Get(close).onExit = OnExit;
        EventTriggerListener.Get(open).onEnter = OnEnter;
        EventTriggerListener.Get(open).onExit = OnExit;
        EventTriggerListener.Get(closeProg).onEnter = OnEnter;
        EventTriggerListener.Get(closeProg).onExit = OnExit;
        //eDiatance.maxValue = 0.1f;
        // eDiatance.minValue = 0.01f;
        // point.maxValue = 5f;
        // point.minValue = 0.2f;
        // mDistance.minValue = 0f;
        //  mDistance.maxValue = 5f;
        menu.localScale = Vector3.zero;
        close.transform.localPosition = new Vector3(close.transform.localPosition.x, close.transform.localPosition.y,- 0.002f);
        closeProg.transform.localPosition = new Vector3(closeProg.transform.localPosition.x, closeProg.transform.localPosition.y, -0.002f);
        // menu.localPosition = new Vector3(-7, -7, 0);
        close.SetActive(false);
        open.SetActive(true);
    }
    private void OnEnter(GameObject o)
    {
        switch (o.name)
        {
            case "Open":
                HintInfoControl.Singleton.Open("打开设置",o.transform.position.x, o.transform.position.y);
                break;
            case "Close":
                HintInfoControl.Singleton.Open("关闭设置", o.transform.position.x, o.transform.position.y);
                break;
            case "CloseProg":
                HintInfoControl.Singleton.Open("退出程序", o.transform.position.x, o.transform.position.y);
                break;
            default:
                break;
        }
    }
    private void OnExit(GameObject o)
    {
        HintInfoControl.Singleton.Close();
    }
    void OnDisable()
    {
        SetMenuControl.Singleton = null;
    }
}
