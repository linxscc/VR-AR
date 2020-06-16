using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 模式
/// </summary>
public enum Dimensional
{
    View2D,
    View3D
}
/// <summary>
/// 单选
/// </summary>
public class UIToggle : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle2D;
    [SerializeField]
    private Toggle toggle3D;
    public CallBack<bool> OnToggle;
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="dimensional"></param>
    public void OnInIt(Dimensional dimensional)
    {
        if (dimensional == Dimensional.View2D)
        {
            toggle3D.isOn = true;
            toggle2D.isOn = false;
        }
        else
        {
            toggle3D.isOn = false;
            toggle2D.isOn = true;
        }
    }
    public void OnButtonClick()
    {
       
        if (OnToggle != null)
        {
            OnToggle(toggle2D.isOn);
            Global.is2D = toggle2D.isOn;
            //print(Global.is3D);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnButtonClick();
           // toggle2D.isOn = !toggle2D.isOn;
            // toggleButton2D3D.isOneEnable = !toggleButton2D3D.isOneEnable;
        }
    }
}
