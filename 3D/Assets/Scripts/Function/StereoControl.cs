using UnityEngine;
using System.Collections;

public class StereoControl
{

    public GameObject camera3D;
    public StereoCam stereoCam;
    private StereoControl()
    {
        camera3D = Resources.Load<GameObject>(Global.stereo);
        camera3D = GameObject.Instantiate(camera3D);
        camera3D.transform.position = new Vector3(0,0,-4);
        camera3D.transform.localScale = Vector3.one;
        stereoCam = camera3D.GetComponent<StereoCam>();
       
    }
    private static StereoControl singleton;
    public static StereoControl Singleton
    {
        get
        {
            if (singleton == null)
                singleton = new StereoControl();
            return singleton;
        }
    }
    public void Open()
    {

    }
}
