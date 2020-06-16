/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UI_XYRF;
using DG.Tweening;

namespace PlaceAR
{
    /// <summary>
    /// 控制ui旋转
    /// </summary>
    public class StartCanvas : MonoBehaviour
    {

        /// <summary>
        /// 所有需要旋转的ui
        /// </summary>
        public List<GameObject> rotaterUI = new List<GameObject>();
        private bool m_IsBackCamera;
        /// <summary>
        /// 判断方向
        /// </summary>
        /// <returns></returns>
        private ScreenOrientation CheckOrientation()
        {

            if (Mathf.Abs(Input.gyro.gravity.z) <= 0.9f)
            {
                if (Mathf.Abs(Input.gyro.gravity.x) > Mathf.Abs(Input.gyro.gravity.y))
                {
                    if (Input.gyro.gravity.x > 0f)
                    {
                        return ScreenOrientation.LandscapeRight;
                    }
                    else
                    {
                        return ScreenOrientation.LandscapeLeft;
                    }
                }
                else if (Input.gyro.gravity.y > 0f)
                {
                    return !this.m_IsBackCamera ? ScreenOrientation.Portrait : ScreenOrientation.PortraitUpsideDown;
                }
                else
                {
                    return !this.m_IsBackCamera ? ScreenOrientation.PortraitUpsideDown : ScreenOrientation.Portrait;
                }
            }
            else return ScreenOrientation.Unknown;
        }
        private void RotatorUI(Vector3 qtn)
        {
            if (Global.OperatorModel == OperatorMode.ARMode)
            {
                for (int i = 0; i < rotaterUI.Count; i++)
                {
                    rotaterUI[i].transform.DORotate(qtn, 0.4f);
                }
            }
        }
        void LateUpdate()
        {
            switch (CheckOrientation())
            {
                case ScreenOrientation.Unknown:
                    break;
                case ScreenOrientation.Portrait:
                    Global.currentScreenDirection = ScreenDirection.horizontalRight;
                    RotatorUI(new Vector3(0, 0, 180));
                    break;
                case ScreenOrientation.PortraitUpsideDown:
                    Global.currentScreenDirection = ScreenDirection.horizontalLeft;
                    RotatorUI(Vector3.zero);
                    break;
                case ScreenOrientation.LandscapeLeft:
                    Global.currentScreenDirection = ScreenDirection.verticalBack;
                    RotatorUI(new Vector3(0, 0, 270));
                    break;
                case ScreenOrientation.LandscapeRight:
                    Global.currentScreenDirection = ScreenDirection.verticalTo;
                    RotatorUI(new Vector3(0, 0, 90));
                    break;
                case ScreenOrientation.AutoRotation:
                    break;
                default:
                    break;
            }
        }
    }
}