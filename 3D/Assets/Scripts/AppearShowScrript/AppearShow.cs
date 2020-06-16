using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/* Time:2017.06.02 夕阳如风
    
*/
namespace Appear_XYRF
{
    /// <summary>
    /// 模型浏览器 显微镜模式控制器
    /// </summary>
    public class AppearShow
    {
        private GameObject appearShowController;
        private bool isExample = false;

        private static AppearShowCore appearShowCore;
        private AppearShow()
        {
            GameObject a = (GameObject)Resources.Load("Prefab/AppearShow/AppearShowController");
            GameObject b = Object.Instantiate(a);
            appearShowCore = b.GetComponent<AppearShowCore>();
            appearShowController = b;
            //初始化
            appearShowCore.Init();
        }
        private static AppearShow instance;
        public static AppearShow Instance
        {
            get
            {
                if (instance == null)
                    instance = new AppearShow();
                return instance;
            }
        }

        /// <summary>
        /// 开启 → 显微镜模式
        /// </summary>
        public void OpenAppearShow()
        {

        }

        /// <summary>
        /// 关闭 → 显微镜模式
        /// </summary>
        public void ClosedAppearShow()
        {
            instance = null;
            isExample = false;
            Object.Destroy(appearShowController);
            Object.Destroy(appearShowCore.panelAppearShow);
        }

        /// <summary>
        ///  更换→需要的透明材质球
        /// </summary>
        /// <param name="Tou">透明材质球</param>
        internal void ChangeMaterial(Material Tou)
        {
            appearShowCore.ChangeMaterial(Tou);
        }

    }
}
