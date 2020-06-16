/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace MoDouAR
{
    /// <summary>
    /// 上方菜单
    /// </summary>
    public class TitleMenu : Window<TitleMenu>
    {
        public override int ID
        {
            get
            {
                return 0;
            }
        }

        public override string Name
        {
            get
            {
                return "TitleMenu";
            }
        }

        public override string Path
        {
            get
            {
                return "UI/TitleMenu";
            }
        }
        public override UIType CurrentUIType
        {
            get
            {
                currentUIType.uiFormType = UIFormType.Normal;
                return base.CurrentUIType;
            }

            set
            {
                base.CurrentUIType = value;
            }
        }
        /// <summary>
        /// 功能按钮
        /// </summary>
        public Button[] button;
        public Button set;
        public override void Start()
        {
            base.Start();
            set.onClick.AddListener(Set);
            for (int i = 0; i < button.Length; i++)
            {
                button[i].transform.Find("BackGround").gameObject.SetActive(false);
                switch (button[i].name)
                {
                    case "Browse":
                        button[i].onClick.AddListener(Browse);
                        break;
                    case "AR":
                        button[i].onClick.AddListener(AR);
                        break;
                    case "MrGlasses":
                        button[i].onClick.AddListener(MRClasses);
                        break;
                    default:
                        break;
                }
            }
            switch (Global.OperatorModel)
            {
                case OperatorMode.BrowserMode:
                    Browse();
                    break;
                case OperatorMode.ARMode:
                    AR();
                    break;
                case OperatorMode.MRModel:
                    MRClasses();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 设置菜单
        /// </summary>
        public void Set()
        {
            PanelSetupEntrance.Instance.CreatWindow();
            PanelSetupEntrance.Instance.Open();
        }
        /// <summary>
        /// 模型浏览
        /// </summary>
        public void Browse()
        {
            Global.OperatorModel = OperatorMode.BrowserMode;
            //SetBackGround("Browse");
        }
        /// <summary>
        /// AR模式
        /// </summary>
        public void AR()
        {
            Global.OperatorModel = OperatorMode.ARMode;
           // SetBackGround("AR");
        }
        /// <summary>
        /// mr菜单
        /// </summary>
        public void MRClasses()
        {
         
            ARKitControl.Instance.IntoAR();
            MrControl.Instance.CreatWindow();
            MrControl.Instance.Open();
            BottomMenu.Instance.Close();
            TakePhoto.Instance.Close();
            Destroy(BottomMenu.Instance.InsObj);
            BottomMenu.Instance.InsObj = null;
            // ARKitControl.Instance.lastOperator = Global.OperatorModel;
            //Global.OperatorModel = OperatorMode.MRModel;
            SetBackGround("MrGlasses");
        }
        public void SetBackGround(string name)
        {
            for (int i = 0; i < button.Length; i++)
            {
                if (button[i].name == name)
                    button[i].transform.Find("BackGround").gameObject.SetActive(true);
                else
                    button[i].transform.Find("BackGround").gameObject.SetActive(false);
            }
        }
    }
}