/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using vPlace_FW;

namespace PlaceAR
{
    public class ScrollMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject close;
        public GameObject typeMenu;
        [SerializeField]
        private GameObject ground;
        public GameObject childGrid;
        public void Close()
        {
            if (Global.OperatorModel == OperatorMode.BrowserMode)
                UIManager.GetInstance().ShowSelectModelBtn();
            transform.localScale =new Vector3(0.0001f, 0.0001f, 0.0001f);
            //close.SetActive(false);
            //typeMenu.SetActive(false);
            //ground.SetActive(false);
            //ChildMenu.SetActive(false);
        }
        public void Open()
        {
            //if (Global.operatorModel == OperatorMode.BrowserMode)
                //transform.SetParent(UIManager.GetInstance().transFixedUI);
            transform.localScale = Vector3.one;
            //close.SetActive(true);
            //typeMenu.SetActive(true);
            //ground.SetActive(true);
            //ChildMenu.SetActive(true);
        }
        private void OnDisable()
        {
            //ScrollMenuControl.Singleton = null;
        }
    }
}
