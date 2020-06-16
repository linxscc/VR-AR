using UnityEngine;
using System.Collections;
using PureMVCDemo;
/// <summary>
/// 退出程序
/// </summary>
public class QuitProcedure
{

    private static bool isReadQuit = false;
    public static void Quit()
    {
        if (isReadQuit)
            Close(true);
        isReadQuit = true;
        ConfirmMenuControl.Singleton.Open("是否退出?\n再次点击退出!", Close);
    }
    /// <summary>
    /// 退出操作
    /// </summary>
    private static void Close(bool isClose)
    {
        if (isClose)
        {
            Debug.Log("quit");
            Application.Quit();
        }
        else
            isReadQuit = false;

    }

}
