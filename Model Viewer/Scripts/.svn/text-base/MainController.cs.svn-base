using UnityEngine;
using System.Collections;

using ZERO.Utilities;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {
    

    private void _InputManager_OnEscape ( object sender, System.EventArgs e )
    {
        //SceneManager.LoadScene("StartScene");
        Debug.Log ( "Escape" );
#if UNITY_EDITOR
        Debug.Log ( "Escape" );        
#elif UNITY_STANDALONE
        Application.Quit ( );
#endif
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
