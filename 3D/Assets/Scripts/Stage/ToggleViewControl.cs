using UnityEngine;
using System.Collections;
using ModelViewerProject.Stage;
/// <summary>
/// UGUI单选框
/// </summary>
public class ToggleViewControl : StageController
{
   // public UIToggle uiToggle;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        {
            //Cursor.visible = false;
#if UNITY_EDITOR
            Cursor.visible = true;
#elif UNITY_STANDALONE
                        Cursor.visible = false;
#endif

           // uiToggle.OnToggle += Render2D;
        }
    }

    private void Update()
    {
        var cam = StereoControl.Singleton.stereoCam.CamL;
        cursor.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.9F));

        cursor.transform.SetSiblingIndex(cursor.transform.parent.childCount - 1);
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    stereoCamera.parallaxDistance += 0.05f;
        //    Debug.LogError(stereoCamera.parallaxDistance);
        //}
        //if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    stereoCamera.parallaxDistance -= 0.05f;
        //    Debug.LogError(stereoCamera.parallaxDistance);
        //}
        //if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    stereoCamera.eyeDistance += 0.1f;
        //}
        //if (Input.GetKeyDown(KeyCode.F4))
        //{
        //    stereoCamera.eyeDistance -= 0.1f;
        //}
    }
}
