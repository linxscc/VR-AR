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
	public class SetViewControl  
	{

        public SetView sideBarView;
        private GameObject sideBarViewObj;
        private SetViewControl()
        {
            sideBarViewObj = Resources.Load<GameObject>(Global.setView);
            sideBarViewObj = GameObject.Instantiate(sideBarViewObj);
            sideBarViewObj.transform.parent = GameObject.FindGameObjectWithTag(Tag.mainUICanvas).transform;
            sideBarViewObj.transform.localPosition = new Vector3(0, 0, 0);
            sideBarViewObj.transform.localScale = Vector3.zero;
            sideBarView = sideBarViewObj.GetComponent<SetView>();
           
        }
        private static SetViewControl singleton;
        public static SetViewControl Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new SetViewControl();
                return singleton;
            }
            set
            {
                singleton = value;
            }
        }
        public void Open(Transform tran=null)
        {
            if (tran != null)
            {
                sideBarViewObj.transform.parent = tran;
                sideBarViewObj.GetComponent<RectTransform>().offsetMin = Vector3.zero;
                sideBarViewObj.GetComponent<RectTransform>().offsetMax = Vector3.zero;
                Debug.Log(sideBarViewObj.GetComponent<RectTransform>().offsetMin);
            }
            sideBarViewObj.transform.localScale = Vector3.one;

        }
        public void Close()
        {
            sideBarViewObj.transform.localScale = Vector3.zero;
        }
    }
}
