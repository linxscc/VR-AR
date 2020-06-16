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
    /// 启动和其他控制
    /// </summary>
    public class MoDouARControl : MonoBehaviour
    {

        private static MoDouARControl instance;

        public static MoDouARControl Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("ModelControll");
                    instance = obj.AddComponent<MoDouARControl>();
                }
                return instance;
            }
        }
        private void Awake()
        {
            instance = this;
            Open();
        }
        private void OnDestroy()
        {
            instance = null;
        }
        public void Open()
        {
            TitleMenu.Instance.CreatWindow();
            TitleMenu.Instance.Open();
            BottomMenu.Instance.CreatWindow();
            BottomMenu.Instance.Open();
            BottomMenu_BrowserWindow.Instance.CreatWindow();
            BottomMenu_BrowserWindow.Instance.Open();
        }
    }
}