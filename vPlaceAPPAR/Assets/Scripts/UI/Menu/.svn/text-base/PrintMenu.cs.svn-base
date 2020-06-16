/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

namespace PlaceAR
{
    public class PrintMenu : MonoBehaviour
    {

        public Text text;
        /// <summary>
        /// 停留时间
        /// </summary>
        private float time;
        /// <summary>
        /// 动画时间
        /// </summary>
        private float animatorTime = 0.5f;
        [SerializeField]
        private Image background;
        public void Open(string txt, float time)
        {
            transform.SetSiblingIndex(transform.parent.childCount - 1);
            background.color = new Color(background.color.r, background.color.g, background.color.b,0);
            this.time = time;
            text.text = txt;
            Tweener t = transform.DOLocalMoveY(0f, animatorTime);
            t.OnComplete(Complete);
            t.SetEase(Ease.InQuad);
            background.DOColor(new Color(background.color.r, background.color.g, background.color.b, 1), animatorTime ).SetEase(Ease.InQuart); 
            text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 1), animatorTime).SetEase(Ease.InQuart);
        }
        private void Complete()
        {
            Invoke("Close", time);
        }
        public void Close()
        {
            Tweener t = transform.DOLocalMoveY(-411, animatorTime);
            t.SetEase(Ease.OutQuad);
            background.DOColor(new Color(background.color.r, background.color.g, background.color.b, 0), animatorTime).SetEase(Ease.OutQuart);
            text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 0), animatorTime).SetEase(Ease.OutQuart);
        }
        void OnDisable()
        {
            PrintMenuControl.Singleton = null;
        }
    }
}
