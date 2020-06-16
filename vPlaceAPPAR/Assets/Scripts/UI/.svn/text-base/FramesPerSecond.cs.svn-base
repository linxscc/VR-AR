using UI_XYRF;
using UnityEngine;


public class FramesPerSecond : MonoBehaviour
{
    private float updateInterval = 1;
    private float seconds = 0;
    private float frames = 0;
    private string text = string.Empty;
    private GUIStyle guiStyle;
    private int frame;
    public void OnInit()
    {
        guiStyle = new GUIStyle();
        Application.targetFrameRate = 30;
    }
    public void SetValue(int frame)
    {
        DontDestroyOnLoad(gameObject);
        this.frame = frame;
        Application.targetFrameRate = frame;
    }
    private void OnGUI()
    {
        if (Application.targetFrameRate > frame)
        {
            Application.targetFrameRate = frame;
        }
        GUI.Label(new Rect(Screen.width - 55, Screen.height - 20, 20, 10), text, guiStyle);
        seconds += Time.deltaTime;
        frames++;

        if (seconds >= updateInterval)
        {
            float fps = frames / seconds;
            text = System.String.Format("{0:F2} fps", fps);

            if (fps < 20)
                guiStyle.normal.textColor = Color.yellow;
            else if (fps < 10)
                guiStyle.normal.textColor = Color.red;
            else
                guiStyle.normal.textColor = Color.white;
            seconds = 0;
            frames = 0;
        }

    }
}
