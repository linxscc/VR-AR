using UnityEngine;
using System.Collections;


namespace Input_XYRF
{
    /// <summary>
    /// 输入控制
    /// </summary>
    public class AR_InputControl
    {
        private AR_InputControl() { }
        private static AR_InputControl instance;
        public static AR_InputControl Instance
        {
            set { instance = value; }
            get
            {
                if (instance == null)
                    instance = new AR_InputControl();
                return instance;
            }
        }

        public void GetInputDetection()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }


    }
}
