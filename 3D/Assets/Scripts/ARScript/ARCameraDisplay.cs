using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCameraDisplay : MonoBehaviour
{

    private static bool isShow = false;
   // public int displayIndex;          //显示器编号

    private void Awake()
    {
        //if (Display.displays.Length > 1)
        //    Display.displays[1].Activate();
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        // Screen.SetResolution(Screen.width, Screen.height, true);

    }
}
