using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UI_XYRF;


namespace Screen_XYRF
{
    /// <summary>
    /// 判别屏幕方向
    /// </summary>
    public class ScreenDirectionRotation
    {
        private ScreenDirectionRotation()
        {
        }
        private static ScreenDirectionRotation instance;
        public static ScreenDirectionRotation Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenDirectionRotation();
                return instance;
            }
            set { instance = null; }
        }

        /// <summary>
        /// 计算方向
        /// </summary>
        public void ChooseDirection()
        {
            float xT = Input.acceleration.x;
            float yT = Input.acceleration.y;
            if (xT >= 0.8f)
                Global.currentScreenDirection = ScreenDirection.verticalTo;
            else if (xT <= -0.8f)
                Global.currentScreenDirection = ScreenDirection.verticalBack;
            else if (yT >= 0.8f)
                Global.currentScreenDirection = ScreenDirection.horizontalRight;
            else if (yT <= -0.8f)
                Global.currentScreenDirection = ScreenDirection.horizontalLeft;
        }
    }
}
