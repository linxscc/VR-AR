/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;

namespace PlaceAR
{
	public class FramesPerSecondControl  
	{
        private FramesPerSecond framesPerSecond;

        private FramesPerSecondControl()
        {
            GameObject obj = new GameObject();
            
            framesPerSecond= obj.AddComponent<FramesPerSecond>();
            framesPerSecond.OnInit();
        }
        private static FramesPerSecondControl singleton;
        public static FramesPerSecondControl Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new FramesPerSecondControl();
                return singleton;
            }
        }
        public  void SetValue(int frames=30)
        {
            framesPerSecond.SetValue(frames);
        }
    }
}
