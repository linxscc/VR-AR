/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
namespace MoDouAR
{
    /// <summary>
    /// 是否在视野范围内
    /// </summary>
    public class AtView : MonoBehaviour
    {
        public bool o;
        /// <summary>
        /// 不可见
        /// </summary>
        void OnBecameInvisible()
        {
            o = false;
            ARKitControl.Instance.SetPoint(name,false);

        }
        /// <summary>
        /// 可见
        /// </summary>
        void OnBecameVisible()
        {
            o = true;
            ARKitControl.Instance.SetPoint(name, true);

        }

    }
}