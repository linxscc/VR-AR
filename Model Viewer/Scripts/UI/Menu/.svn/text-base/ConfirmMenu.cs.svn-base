/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
 */
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PureMVCDemo
{
    /// <summary>
    /// 提示窗口
    /// </summary>
    public class ConfirmMenu : MonoBehaviour
    {
        /// <summary>
        /// 确认按钮
        /// </summary>
        [SerializeField]
        public Button confirm;
        /// <summary>
        /// 取消按钮
        /// </summary>
        [SerializeField]
        public Button cancel;
        /// <summary>
        /// 显示的文字
        /// </summary>
        [SerializeField]
        private Text txt;
        [SerializeField]
        private Transform ground;
        [SerializeField]
        private GameObject backGround;
       
        void OnDisable()
        {
            ConfirmMenuControl.Singleton = null;
        }
        public void OnInit()
        {
            ground.localScale = Vector3.zero;
        }
        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="complete"></param>
        public void Open(string txt, TweenCallback complete)
        {
            Tweener t = ground.DOScale(Vector3.one,Global.animationLength);
            t.OnComplete(complete);
            backGround.SetActive(true);               
            this.txt.text = txt;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="complete"></param>
        public void Close(TweenCallback complete)
        {
            Tweener t = ground.DOScale(Vector3.zero, Global.animationLength);
            t.OnComplete(complete);
            backGround.SetActive(false);
        }
    }
}
