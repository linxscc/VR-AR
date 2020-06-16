using UnityEngine;
using System.Collections;

using ZERO.Utilities;
using UnityEngine.SceneManagement;
using PureMVCDemo;

public class MainController : MonoBehaviour {
    

    private void _InputManager_OnEscape ( object sender, System.EventArgs e )
    {
        //ConfirmMenuControl.Singleton.Open("确定返回?", Back);
        Back();
        //  Debug.Log ( "Escape" );
#if UNITY_EDITOR
        //  Debug.Log ( "Escape" );        
#elif UNITY_STANDALONE
       // Application.Quit ( );
#endif
    }
    public void Back()
    {
        SceneManager.LoadScene("StartScene");
        //ARStartupControl.ARInstance.ARStartup.LoadStartSceneSetCameraPos();
    }


    void OnDisable ( )
    {
        _InputManager.OnEscape -= _InputManager_OnEscape;
    }


    void OnEnable ( )
    {
        _InputManager.OnEscape += _InputManager_OnEscape;
    }
}
