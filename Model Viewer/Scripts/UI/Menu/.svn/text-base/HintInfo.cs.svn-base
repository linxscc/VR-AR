/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PureMVCDemo
{
    /// <summary>
    /// 提示信息
    /// </summary>
    public class HintInfo : MonoBehaviour
    {
        [SerializeField]
        private Text text;
        public void Open(string txt,Vector3 pos)
        {
            transform.localPosition = pos;
            transform.SetSiblingIndex(transform.parent.childCount - 1);
            text.text = txt;
            transform.localScale = new Vector3(0.05f,0.05f,0.05f);
            
        }
        public void Close()
        {
            transform.localScale = Vector3.zero;
        }
        void OnDisable()
        {
            HintInfoControl.Singleton = null;
        }
    }
}
