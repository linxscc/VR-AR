/*
 *    日期:2017,7,26
 *    作者:
 *    标题:
 *    功能:首页功能管理
*/
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UI_XYRF;
using MoDouAR;

namespace PlaceAR
{
    public class MenuCentre : MonoBehaviour
    {
        public static MenuCentre menuCentre;
        public CallBack callModelScene;
        private bool isLoad = false;
        public void Start()
        {
            menuCentre = this;
            //FramesPerSecondControl.Singleton.SetValue(30);
        }
        /// <summary>
        /// 个人中心按钮
        /// </summary>
        public void OpenPersonalCenter()
        {
            SideBarViewControl.Singleton.Open(GetComponent<DownPrefab>().buttonItem);

        }
        /// <summary>
        /// 打开arkit相机
        /// </summary>
        public void OpenARKIT()
        {
            
            Global.OperatorModel = OperatorMode.ARMode;
            ARKitControl.Instance.IntoAR(); ;
            Global.isStartSceneEnterAR = true;
            UIProcessing.Instance.OPenOrCloseARCanvasUI(true);
            //        StartSceneControl.Singleton.Close();
            //foreach (KeyValuePair<int, ItemChild> kvp in GetComponent<DownPrefab>().buttonItem)
            //{
            //    if (kvp.Value.configLocal.Count >= 0 && !isLoad)
            //    {
            //        isLoad = true;
            //        //    AsyncOperation async = SceneManager.LoadSceneAsync(Global.sceneAR);
            //        UIProcessing.Instance.OPenOrCloseARCanvasUI(true);
            //        //eneManager.
            //    }

            //Tag
            // }
        }
        public void OpenMR()
        {
            
            Global.OperatorModel = OperatorMode.MRModel;
            ARKitControl.Instance.IntoMR();
            StartSceneControl.Singleton.Close();
            // Global.isStartSceneEnterAR = true;
            //SceneManager.LoadScene("MR");
           // UIProcessing.Instance.OPenOrCloseARCanvasUI(true);
        }
        /// <summary>
        /// 打开模型浏览
        /// </summary>
        public void OpenModelControl()
        {
           // ARKitControl.Instance.OnInit();
            // Application.LoadLevel(1);
            //AsyncOperation async = SceneManager.LoadSceneAsync(Global.sceneBrowse);

        }
    }
}
