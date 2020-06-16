/*
 *    日期:2017/7/10
 *    作者:
 *    标题:输出提示窗口
 *    功能:
*/
using UnityEngine;
using System.Collections;

namespace PlaceAR
{
    public class PrintMenuControl
    {
        private PrintMenu printMenu;
        private GameObject printMenuObj;
        private PrintMenuControl()
        {
            printMenuObj = Resources.Load<GameObject>(Global.printMenu);
            printMenuObj = GameObject.Instantiate(printMenuObj);
            printMenuObj.transform.parent = GameObject.FindGameObjectWithTag(Tag.mainUICanvas).transform;
            printMenuObj.transform.localPosition = new Vector3(0, -411, 0);
            printMenuObj.transform.localScale = Vector3.one;
            printMenu = printMenuObj.GetComponent<PrintMenu>();
        }
        private static PrintMenuControl singleton;
        public static PrintMenuControl Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new PrintMenuControl();
                return singleton;
            }
            set
            {
                singleton = value;
            }
        }
        public void Open(string txt,float time=2f)
        {
            printMenu.Open(txt, time);
        }
        public void Close()
        {
            printMenu.Close();
        }
    }
}
