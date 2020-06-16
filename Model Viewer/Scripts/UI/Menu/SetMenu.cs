using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
/// <summary>
/// 设置菜单
/// </summary>
public class SetMenu : MonoBehaviour
{

    private bool isOpen = false;
    [SerializeField]
    private GameObject close;
    [SerializeField]
    private GameObject open;
    [SerializeField]
    private Transform menu;
    [SerializeField]
    private Slider eDiatance;
    [SerializeField]
    private Slider point;
    [SerializeField]
    private Slider mDistance;
    public float EyeDistance
    {
        get { return eDiatance.value; }
        set
        {
            eDiatance.value = value;
        }
    }
    public float MDistance
    {
        get { return mDistance.value; }
        set
        {
            mDistance.value = value;
        }
    }
    public float Point
    {
        get { return point.value; }
        set
        {
            point.value = value;
        }
    }
    /// <summary>
    /// 关闭
    /// </summary>
    public void Close()
    {
        if (!isOpen) return;
        //Tweener t = menu.DOLocalMove(new Vector3(-7, -7, 0), 0.3f);
        Tweener t = menu.DOScale(Vector3.zero, Global.animationLength);
        t.OnComplete(Complete);
        isOpen = false;

    }
    /// <summary>
    /// 开启
    /// </summary>
    public void Open()
    {
        if (isOpen) return;
        //menu.DOLocalMove(Vector3.zero, 0.3f);
        Tweener t = menu.DOScale(new Vector3(0.1f, 0.1f, 1f), Global.animationLength);
        isOpen = true;
        close.SetActive(true);
        open.SetActive(false);
    }
    /// <summary>
    /// 关闭回调
    /// </summary>
    private void Complete()
    {
        close.SetActive(false);
        open.SetActive(true);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void OnInit()
    {
        eDiatance.maxValue = 0.1f;
        eDiatance.minValue = 0.01f;
        point.maxValue = 5f;
        point.minValue = 0.2f;
        mDistance.minValue = 0f;
        mDistance.maxValue = 5f;
        menu.localScale = Vector3.zero;
        // menu.localPosition = new Vector3(-7, -7, 0);
        close.SetActive(false);
        open.SetActive(true);
    }
    void OnDisable()
    {
        SetMenuControl.Singleton = null;
    }
}
