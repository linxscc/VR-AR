/***
 * 
 *    Title: UI框架
 *           主题: UI管理器
 *    Description: 
 *           功能: 整个UI框架的核心，实现框架绝大多数的功能实现
 *           1: UI窗体的加载
 *           2: UI窗体的缓存
 *           3: UI窗体基类的各种生命周期操作
 *           4: 
 *                          
 *    Date: 2017/07
 *    Version: 0.1
 *	  Author: 
 *    Modify Recoder: 
 *    
 *   软件开发原则：
 *   1：高内聚，低耦合
 *   2：方法的单一职责
 *   
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using vPlace_zpc;

namespace vPlace_FW
{
    public class UIManager : MonoBehaviour
    {


        #region 字段

        /// <summary>
        /// 主界面模型选择按钮
        /// </summary>
        public GameObject selectModelBtn = null;

        /// <summary>
        /// 主界面AR按钮
        /// </summary>
        public GameObject enterARBtn = null;

        /// <summary>
        /// 显微镜缩放条
        /// </summary>
        public GameObject zoomInMode = null;

        /// <summary>
        /// 单例
        /// </summary>
        private static UIManager instance;

        /// <summary>
        /// UI窗体预设路径（参数1：窗体预设名称， 2：窗体预设路径）
        /// </summary>
        private Dictionary<string, string> uiFormPaths;

        /// <summary>
        /// 缓存所有UI窗体
        /// </summary>
        public Dictionary<string, BaseUI> allUIForms;

        /// <summary>
        /// 当前显示的UI窗体
        /// </summary>
        private Dictionary<string, BaseUI> currentShowUIForms;

        /// <summary>
        /// 定义“栈”集合，存储显示当前所有[反向切换]窗体
        /// </summary>
        private Stack<BaseUI> stackCurrentUIForms;

        /// <summary>
        /// UI根节点
        /// </summary>
        private Transform transCanvasForms = null;

        /// <summary>
        /// 普通UI显示节点
        /// </summary>
        public Transform transNormalUI = null;

        /// <summary>
        /// 固定UI显示节点
        /// </summary>
        public Transform transFixedUI = null;

        /// <summary>
        /// 弹窗UI显示节点
        /// </summary>
        public Transform transPopUI = null;

        /// <summary>
        /// UI管理脚本节点
        /// </summary>
        private Transform transUIManager = null;
        #endregion


        #region 公有方法

        /// <summary>
        /// 得到单例
        /// </summary>
        /// <returns></returns>
        public static UIManager GetInstance()
        {
            if (instance == null)
            {
                instance = new GameObject("UIManager").AddComponent<UIManager>();
            }
            return instance;
        }

        /// <summary>
        /// 初始化核心数据，加载“UI窗体路径”到集合中
        /// </summary>
        private void Awake()
        {
            if (instance != null)
            {
                if (instance != this)
                    Destroy(gameObject);
            }
            else
                instance = this;

            /* 字段初始化 */
            uiFormPaths = new Dictionary<string, string>();
            allUIForms = new Dictionary<string, BaseUI>();
            currentShowUIForms = new Dictionary<string, BaseUI>();
            stackCurrentUIForms = new Stack<BaseUI>();

            //得到UI根节点
            transCanvasForms = GameObject.FindGameObjectWithTag(SystemDefine.SYS_TAG_CANVAS).transform;

            //UI存放路径存入字典
            InitUIFormPathData();
        }

        /// <summary>
        /// 显示(打开)UI窗体
        /// 功能：
        /// 1：根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        /// 2：根据不同的UI窗体的“显示模式”，分别左不同的加载处理
        /// </summary>
        /// <param name="uiFormName">UI预设窗体的名称</param>
        public void ShowUIForm(string uiFormName)
        {
            BaseUI baseUI = null;               //UI窗体基类

            if (string.IsNullOrEmpty(uiFormName)) return;

            baseUI = LoadFormToAllFormCatch(uiFormName);
            if (baseUI == null) return;

            if (baseUI.CurrentUIType.isClearStack)
                ClearStackArray();

            switch (baseUI.CurrentUIType.uiFormShowMode)
            {
                case UIFormShowMode.Normal:                 //“普通显示”窗口模式
                    //把当前窗体加载到“当前窗体”集合中。
                    LoadUIToCurrentCache(uiFormName);
                    break;
                case UIFormShowMode.ReverseChange:          //“反向切换”窗口模式
                    PushUIFormToStack(uiFormName);
                    break;
                case UIFormShowMode.HideOther:              //“隐藏其他”窗口模式
                    EnterUIFormsAndHideOther(uiFormName);
                    break;
                default:
                    break;
            }
            //打开新窗口，关闭其他窗口
            foreach (var uiName in allUIForms.Keys)
            {
                if (!uiName.Equals(uiFormName) && !uiName.Equals(ProjectConstDefine.MODELBROWSER_BROWSERTYPEVIEW))
                    CloseUIForm(uiName);
            }
        }

        /// <summary>
        /// 关闭（返回上一个）窗体
        /// </summary>
        /// <param name="uiFormName">UI窗体名称</param>
        public void CloseUIForm(string uiFormName)
        {
            BaseUI baseUiForm;                          //窗体基类

            //参数检查
            if (string.IsNullOrEmpty(uiFormName)) return;
            //“所有UI窗体”集合中，如果没有记录，则直接返回
            allUIForms.TryGetValue(uiFormName, out baseUiForm);
            if (baseUiForm == null) return;
            //根据窗体不同的显示类型，分别作不同的关闭处理
            switch (baseUiForm.CurrentUIType.uiFormShowMode)
            {
                case UIFormShowMode.Normal:
                    //普通窗体的关闭
                    ExitUIForms(uiFormName);
                    break;
                case UIFormShowMode.ReverseChange:
                    //反向切换窗体的关闭
                    PopUIFroms();
                    break;
                case UIFormShowMode.HideOther:
                    //隐藏其他窗体关闭
                    ExitUIFormsAndDisplayOther(uiFormName);
                    break;

                default:
                    break;
            }
        }

        #endregion


        #region 私有方法

        /// <summary>
        /// 初始化加载（根UI窗体）Canvas预设
        /// </summary>
        private void InitRootCanvasLoading()
        {
            ResourcesManager.GetInstance().LoadAsset(SystemDefine.SYS_PATH_CANVAS, false);
        }

        /// <summary>
        /// 根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        /// 功能： 检查“所有UI窗体”集合中，是否已经加载过，否则才加载。
        /// </summary>
        /// <param name="uiFormName">UI窗体（预设）名称</param>
        /// <returns></returns>
        private BaseUI LoadFormToAllFormCatch(string uiFormName)
        {
            BaseUI baseUIResult = null;      //加载的返回UI窗体基类

            allUIForms.TryGetValue(uiFormName, out baseUIResult);
            if (baseUIResult == null)
                baseUIResult = LoadUIForm(uiFormName);

            return baseUIResult;
        }

        /// <summary>
        /// 加载指定名称的“UI窗体”
        /// 功能：
        ///    1：根据“UI窗体名称”，加载预设克隆体。
        ///    2：根据不同预设克隆体中带的脚本中不同的“位置信息”，加载到“根窗体”下不同的节点。
        ///    3：隐藏刚创建的UI克隆体。
        ///    4：把克隆体，加入到“所有UI窗体”（缓存）集合中。
        /// 
        /// </summary>
        /// <param name="uiFormName">UI窗体名称</param>
	    private BaseUI LoadUIForm(string uiFormName)
        {
            string strUIFormPath = null;                   //UI窗体路径
            GameObject goCloneUIPrefab = null;             //创建的UI克隆体预设
            BaseUI baseUI = null;                          //窗体基类


            //根据UI窗体名称，得到对应的加载路径
            uiFormPaths.TryGetValue(uiFormName, out strUIFormPath);
            //根据“UI窗体名称”，加载“预设克隆体”
            if (!string.IsNullOrEmpty(strUIFormPath))
            {
                goCloneUIPrefab = ResourcesManager.GetInstance().LoadAsset(strUIFormPath, false);
            }
            //设置“UI克隆体”的父节点（根据克隆体中带的脚本中不同的“位置信息”）
            if (transCanvasForms != null && goCloneUIPrefab != null)
            {
                baseUI = goCloneUIPrefab.GetComponent<BaseUI>();
                if (baseUI == null)
                {
                    Debug.Log("baseUiForm==null! ,请先确认窗体预设对象上是否加载了baseUIForm的子类脚本！ 参数 uiFormName=" + uiFormName);
                    return null;
                }
                switch (baseUI.CurrentUIType.uiFormType)
                {
                    case UIFormType.Normal:                 //普通窗体节点
                        goCloneUIPrefab.transform.SetParent(transNormalUI, false);
                        break;
                    case UIFormType.Fixed:                  //固定窗体节点
                        goCloneUIPrefab.transform.SetParent(transFixedUI, false);
                        break;
                    case UIFormType.PopUp:                  //弹出窗体节点
                        goCloneUIPrefab.transform.SetParent(transPopUI, false);
                        break;
                    default:
                        break;
                }

                //设置隐藏
                goCloneUIPrefab.SetActive(false);
                //把克隆体，加入到“所有UI窗体”（缓存）集合中。
                allUIForms.Add(uiFormName, baseUI);
                return baseUI;
            }
            else
            {
                Debug.Log("transCanvasForms==null Or goCloneUIPrefabs==null!! ,Plese Check!, 参数uiFormName=" + uiFormName);
            }

            Debug.Log("出现不可以预估的错误，请检查，参数 uiFormName=" + uiFormName);
            return null;
        }

        /// <summary>
        /// 把当前窗体加载到“当前窗体”集合中
        /// </summary>
        /// <param name="uiFormName"></param>
        private void LoadUIToCurrentCache(string uiFormName)
        {
            BaseUI baseUi;                              //UI窗体基类
            BaseUI baseUIFormFromAllCache;              //从“所有窗体集合”中得到的窗体

            //如果“正在显示”的集合中，存在整个UI窗体，则直接返回
            currentShowUIForms.TryGetValue(uiFormName, out baseUi);
            if (baseUi != null) return;
            //把当前窗体，加载到“正在显示”集合中
            allUIForms.TryGetValue(uiFormName, out baseUIFormFromAllCache);
            if (baseUIFormFromAllCache != null)
            {
                currentShowUIForms.Add(uiFormName, baseUIFormFromAllCache);
                baseUIFormFromAllCache.Display();           //显示当前窗体
            }
        }

        /// <summary>
        /// UI窗体入栈
        /// </summary>
        /// <param name="uiFormName">窗体的名称</param>
        private void PushUIFormToStack(string uiFormName)
        {
            BaseUI baseUI;                             //UI窗体

            //判断“栈”集合中，是否有其他的窗体，有则“冻结”处理。
            if (stackCurrentUIForms.Count > 0)
            {
                BaseUI topUIForm = stackCurrentUIForms.Peek();
                //栈顶元素作冻结处理
                topUIForm.Freeze();
            }
            //判断“UI所有窗体”集合是否有指定的UI窗体，有则处理。
            allUIForms.TryGetValue(uiFormName, out baseUI);
            if (baseUI != null)
            {
                //当前窗口显示状态
                baseUI.Display();
                //把指定的UI窗体，入栈操作。
                stackCurrentUIForms.Push(baseUI);
            }
            else
            {
                Debug.Log("baseUIForm==null,Please Check, 参数 uiFormName=" + uiFormName);
            }
        }

        /// <summary>
        /// 退出指定UI窗体
        /// </summary>
        /// <param name="strUIFormName">UI窗体名称</param>
        private void ExitUIForms(string strUIFormName)
        {
            BaseUI baseUIForm;                          //窗体基类

            //"正在显示集合"中如果没有记录，则直接返回。
            currentShowUIForms.TryGetValue(strUIFormName, out baseUIForm);
            if (baseUIForm == null) return;
            //指定窗体，标记为“隐藏状态”，且从"正在显示集合"中移除。
            baseUIForm.Hiding();
            currentShowUIForms.Remove(strUIFormName);
        }

        /// <summary>
        /// （“反向切换”属性）窗体的出栈逻辑
        /// </summary>
        private void PopUIFroms()
        {
            if (stackCurrentUIForms.Count >= 2)
            {
                //出栈处理
                BaseUI topUIForms = stackCurrentUIForms.Pop();
                //做隐藏处理
                topUIForms.Hiding();
                //出栈后，下一个窗体做“重新显示”处理。
                BaseUI nextUIForms = stackCurrentUIForms.Peek();
                nextUIForms.ReDisplay();
            }
            else if (stackCurrentUIForms.Count == 1)
            {
                //出栈处理
                BaseUI topUIForms = stackCurrentUIForms.Pop();
                //做隐藏处理
                topUIForms.Hiding();
            }
        }

        /// <summary>
        /// (“隐藏其他”属性)打开窗体，且隐藏其他窗体
        /// </summary>
        /// <param name="strUIName">打开的指定窗体名称</param>
        private void EnterUIFormsAndHideOther(string strUIName)
        {
            BaseUI baseUIForm;                          //UI窗体基类
            BaseUI baseUIFormFromALL;                   //从集合中得到的UI窗体基类


            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;

            currentShowUIForms.TryGetValue(strUIName, out baseUIForm);
            if (baseUIForm != null) return;

            //把“正在显示集合”与“栈集合”中所有窗体都隐藏。
            foreach (BaseUI baseUI in currentShowUIForms.Values)
            {
                baseUI.Hiding();
            }
            foreach (BaseUI staUI in stackCurrentUIForms)
            {
                staUI.Hiding();
            }

            //把当前窗体加入到“正在显示窗体”集合中，且做显示处理。
            allUIForms.TryGetValue(strUIName, out baseUIFormFromALL);
            if (baseUIFormFromALL != null)
            {
                currentShowUIForms.Add(strUIName, baseUIFormFromALL);
                //窗体显示
                baseUIFormFromALL.Display();
            }
        }

        /// <summary>
        /// (“隐藏其他”属性)关闭窗体，且显示其他窗体
        /// </summary>
        /// <param name="strUIName">打开的指定窗体名称</param>
        private void ExitUIFormsAndDisplayOther(string strUIName)
        {
            BaseUI baseUIForm;                          //UI窗体基类


            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;

            currentShowUIForms.TryGetValue(strUIName, out baseUIForm);
            if (baseUIForm == null) return;

            //当前窗体隐藏状态，且“正在显示”集合中，移除本窗体
            baseUIForm.Hiding();
            currentShowUIForms.Remove(strUIName);

            //把“正在显示集合”与“栈集合”中所有窗体都定义重新显示状态。
            foreach (BaseUI baseUI in currentShowUIForms.Values)
            {
                baseUI.ReDisplay();
            }
            foreach (BaseUI staUI in stackCurrentUIForms)
            {
                staUI.ReDisplay();
            }
        }

        /// <summary>
        /// 是否清空“栈集合”中得数据
        /// </summary>
        /// <returns></returns>
        private bool ClearStackArray()
        {
            if (stackCurrentUIForms != null && stackCurrentUIForms.Count >= 1)
            {
                //清空栈集合
                stackCurrentUIForms.Clear();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 初始化“UI窗体预设”路径数据
        /// </summary>
        private void InitUIFormPathData()
        {
            //IConfigManager confiManager = new ConfigManagerByJson(SystemDefine.SYS_PATH_UIFORM_CONFIG_INFO);
            //if (confiManager != null)
            uiFormPaths.Add("ModelBrowserMainView", "UIPrefabs\\ModelBrowser\\ModelBrowserMainView");
            uiFormPaths.Add("SelectModelView", "UIPrefabs\\ModelBrowser\\SelectModelView");
            uiFormPaths.Add("BrowserTypeView", "UIPrefabs\\ModelBrowser\\BrowserTypeView");
            uiFormPaths.Add("ModelIntroductionView", "UIPrefabs\\ModelBrowser\\ModelIntroductionView");

        }
        #endregion


        #region 按钮状态设置

        /// <summary>
        /// 显示或隐藏模型选择及AR按钮
        /// </summary>
        public void ShowSelectModelBtn()
        {
            if (!selectModelBtn.activeSelf)
            {
                selectModelBtn.SetActive(true);
                enterARBtn.SetActive(true);
            }
        }

        /// <summary>
        /// 隐藏模型选择及AR按钮
        /// </summary>
        public void HideSeleModelBtn()
        {
            // print(12);
            selectModelBtn.SetActive(false);
            enterARBtn.SetActive(false);
        }


        #endregion
        /* #region 显示“UI管理器”内部核心数据，测试使用

         /// <summary>
         /// 显示"所有UI窗体"集合的数量
         /// </summary>
         /// <returns></returns>
         public int ShowALLUIFormCount()
         {
             if (allUIForms != null)
             {
                 return allUIForms.Count;
             }
             else
             {
                 return 0;
             }
         }

         /// <summary>
         /// 显示"当前窗体"集合中数量
         /// </summary>
         /// <returns></returns>
         public int ShowCurrentUIFormsCount()
         {
             if (currentShowUIForms != null)
             {
                 return currentShowUIForms.Count;
             }
             else
             {
                 return 0;
             }
         }

         /// <summary>
         /// 显示“当前栈”集合中窗体数量
         /// </summary>
         /// <returns></returns>
         public int ShowCurrentStackUIFormsCount()
         {
             if (stackCurrentUIForms != null)
             {
                 return stackCurrentUIForms.Count;
             }
             else
             {
                 return 0;
             }
         }
         #endregion
     */
    }
}
