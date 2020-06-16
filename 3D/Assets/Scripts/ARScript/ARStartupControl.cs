using UnityEngine;
using System.Collections;

/// <summary>
/// AR启动预设
/// </summary>
public class ARStartupControl
{
    public ARStartup ARStartup = null;

    private GameObject easyAR = null;

    private ARStartupControl()
    {
        easyAR = GameObject.Instantiate(Resources.Load<GameObject>(Global.EASYARURL));
        ARStartup = easyAR.GetComponent<ARStartup>();
        ARStartup.OnInit();
    }
    private static ARStartupControl _ARInstance;
    public static ARStartupControl ARInstance
    {
        get
        {
            if (_ARInstance == null)
                _ARInstance = new ARStartupControl();
            return _ARInstance;
        }
        set
        {
            _ARInstance = value;
        }
    }

    public void OpenAR()
    {
        
    }

    public void CloseAR()
    {
       
    }
}
