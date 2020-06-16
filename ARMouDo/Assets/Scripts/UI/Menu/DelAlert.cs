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
    public class DelAlert : MonoBehaviour
    {
        private CallBack callBack;
        public void Open(CallBack callBack)
        {
            gameObject.SetActive(true);
            this.callBack = callBack;
            transform.localScale = Vector3.one;
        }
        public void Cancle()
        {
            transform.localScale = Vector3.zero;
        }
        public void Delete()
        {
           
            Cancle();
            callBack();
        }
    }
}