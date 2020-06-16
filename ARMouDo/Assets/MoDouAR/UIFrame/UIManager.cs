/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MoDouAR
{
    /// <summary>
    /// UI管理
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        /// <summary>
        /// 单例
        /// </summary>
        private static UIManager instance;
        /// <summary>
        /// 得到单例
        /// </summary>
        /// <returns></returns>
        public static UIManager GetInstance()
        {
            if (instance == null)
            {
                instance = new GameObject("UIManager").AddComponent<UIManager>();
                //DontDestroyOnLoad(instance);
            }
            return instance;
        }
        /// <summary>
        /// UI根节点
        /// </summary>
        private Transform transCanvasForms;
        /// <summary>
        /// 普通UI显示节点
        /// </summary>
        public Transform transNormalUI;

        /// <summary>
        /// 固定UI显示节点
        /// </summary>
        public Transform transFixedUI;

        /// <summary>
        /// 弹窗UI显示节点
        /// </summary>
        public Transform transPopUI;

        /// <summary>
        /// 缓存所有UI窗体
        /// </summary>
        public Dictionary<string, WindowsBase> allUIForms;
        /// <summary>
        /// 当前显示的UI窗体
        /// </summary>
        private Dictionary<string, WindowsBase> currentShowUIForms;
        /// <summary>
        /// 定义“栈”集合，存储显示当前所有[反向切换]窗体
        /// </summary>
        public Stack<WindowsBase> stackCurrentUIForms;

        // Use this for initialization
        void Awake()
        {
            if (instance != null)
            {
                if (instance != this)
                    Destroy(gameObject);
            }
          
            else
                instance = this;
            //DontDestroyOnLoad(this);
            allUIForms = new Dictionary<string, WindowsBase>();
            currentShowUIForms = new Dictionary<string, WindowsBase>();
            stackCurrentUIForms = new Stack<WindowsBase>();
            if (GameObject.FindGameObjectWithTag(SystemDefineTag.UICanvas2D) != null)
            {
                transCanvasForms = GameObject.FindGameObjectWithTag(SystemDefineTag.UICanvas2D).transform;
                transNormalUI = transCanvasForms.Find(SystemDefineTag.NormalPath);
                transFixedUI = transCanvasForms.Find(SystemDefineTag.FixedPath);
                transPopUI = transCanvasForms.Find(SystemDefineTag.PopPath);
            }
        }
        /// <summary>
        /// 关闭（返回上一个）窗体
        /// </summary>
        /// <param name="uiFormName">UI窗体名称</param>
        public void CloseUIForm(string winName)
        {
            WindowsBase baseUiForm;                          //窗体基类

            //参数检查
            if (string.IsNullOrEmpty(winName)) return;
            //“所有UI窗体”集合中，如果没有记录，则直接返回
            allUIForms.TryGetValue(winName, out baseUiForm);
            if (baseUiForm == null) return;
            //根据窗体不同的显示类型，分别作不同的关闭处理
            switch (baseUiForm.CurrentUIType.uiFormShowMode)
            {
                case UIFormShowMode.Normal:
                    //普通窗体的关闭
                    ExitUIForms(winName);
                    break;
                case UIFormShowMode.ReverseChange:
                    //反向切换窗体的关闭
                    PopUIFroms();
                    break;
                case UIFormShowMode.HideOther:
                    //隐藏其他窗体关闭
                    ExitUIFormsAndDisplayOther(winName);
                    break;

                default:
                    break;
            }
        }
        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="wind"></param>
        public void ShowUIForm(Wind winName)
        {
            if (allUIForms.ContainsKey(winName.name))
            {
                //print(winName.name);
                WindowsBase baseUI = allUIForms[winName.name].GetComponent<WindowsBase>();
                if (baseUI.CurrentUIType.isClearStack)
                    ClearStackArray();
                switch (baseUI.CurrentUIType.uiFormShowMode)
                {
                    case UIFormShowMode.Normal:
                        LoadUIToCurrentCache(winName.name);
                        break;
                    case UIFormShowMode.ReverseChange:
                        PushUIFormToStack(winName.name);
                        break;
                    case UIFormShowMode.HideOther:
                        EnterUIFormsAndHideOther(winName.name);
                        break;
                    default:
                        break;
                }

            }
        }
        /// <summary>
        /// 退出指定UI窗体
        /// </summary>
        /// <param name="strUIFormName">UI窗体名称</param>
        private void ExitUIForms(string strUIFormName)
        {
            WindowsBase baseUIForm;                          //窗体基类

            //"正在显示集合"中如果没有记录，则直接返回。
            currentShowUIForms.TryGetValue(strUIFormName, out baseUIForm);
            if (baseUIForm == null) return;
            //指定窗体，标记为“隐藏状态”，且从"正在显示集合"中移除。
            baseUIForm.Hiding();
            currentShowUIForms.Remove(strUIFormName);
        }
        /// <summary>
        /// (“隐藏其他”属性)关闭窗体，且显示其他窗体
        /// </summary>
        /// <param name="strUIName">打开的指定窗体名称</param>
        private void ExitUIFormsAndDisplayOther(string strUIName)
        {
            WindowsBase baseUIForm;                          //UI窗体基类


            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;

            currentShowUIForms.TryGetValue(strUIName, out baseUIForm);
            if (baseUIForm == null) return;

            //当前窗体隐藏状态，且“正在显示”集合中，移除本窗体
            baseUIForm.Hiding();
            currentShowUIForms.Remove(strUIName);

            //把“正在显示集合”与“栈集合”中所有窗体都定义重新显示状态。
            foreach (WindowsBase baseUI in currentShowUIForms.Values)
            {
                baseUI.ReDisplay();
            }
            foreach (WindowsBase staUI in stackCurrentUIForms)
            {
                staUI.ReDisplay();
            }
        }
        /// <summary>
        /// （“反向切换”属性）窗体的出栈逻辑
        /// </summary>
        private void PopUIFroms()
        {
            if (stackCurrentUIForms.Count >= 2)
            {
                //出栈处理
                WindowsBase topUIForms = stackCurrentUIForms.Pop();
                //做隐藏处理
                topUIForms.Hiding();
                //出栈后，下一个窗体做“重新显示”处理。
                WindowsBase nextUIForms = stackCurrentUIForms.Peek();
                nextUIForms.ReDisplay();
            }
            else if (stackCurrentUIForms.Count == 1)
            {
                //出栈处理
                WindowsBase topUIForms = stackCurrentUIForms.Pop();
                //做隐藏处理
                topUIForms.Hiding();
            }
        }
        /// <summary>
        /// 创建窗体
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject CreatWindows(string path,string wName)
        {
            WindowsBase windows=null;
            GameObject wndObj = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            if (wndObj == null) return null;
            wndObj.name = wName;
            windows = wndObj.GetComponent<WindowsBase>();
            switch (windows.CurrentUIType.uiFormType)
            {
                case UIFormType.Normal:                 //普通窗体节点
                    wndObj.transform.SetParent(transNormalUI, false);
                    break;
                case UIFormType.Fixed:                  //固定窗体节点
                    wndObj.transform.SetParent(transFixedUI, false);
                    break;
                case UIFormType.PopUp:                  //弹出窗体节点
                    wndObj.transform.SetParent(transPopUI, false);
                    break;
                default:
                    break;
            }
            wndObj.SetActive(false);
            wndObj.transform.localPosition = Vector3.zero;
            wndObj.transform.localScale = Vector3.one;
            wndObj.transform.localRotation = Quaternion.identity;
            if (!allUIForms.ContainsKey(wName))
                allUIForms.Add(wName, windows);
            else
                print(wName);
            return wndObj;
        }
        /// <summary>
        /// 把当前窗体加载到“当前窗体”集合中
        /// </summary>
        /// <param name="uiFormName"></param>
        private void LoadUIToCurrentCache(string uiFormName)
        {
            WindowsBase baseUi;                              //UI窗体基类
            WindowsBase baseUIFormFromAllCache;              //从“所有窗体集合”中得到的窗体

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
            WindowsBase baseUI;                             //UI窗体

            //判断“栈”集合中，是否有其他的窗体，有则“冻结”处理。
            if (stackCurrentUIForms.Count > 0)
            {
                WindowsBase topUIForm = stackCurrentUIForms.Peek();
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
                if(!stackCurrentUIForms.Contains(baseUI))
                stackCurrentUIForms.Push(baseUI);
            }
            else
            {
                Debug.Log("baseUIForm==null,Please Check, 参数 uiFormName=" + uiFormName);
            }
        }
        /// <summary>
        /// (“隐藏其他”属性)打开窗体，且隐藏其他窗体
        /// </summary>
        /// <param name="strUIName">打开的指定窗体名称</param>
        private void EnterUIFormsAndHideOther(string strUIName)
        {
            WindowsBase baseUIForm;                          //UI窗体基类
            WindowsBase baseUIFormFromALL;                   //从集合中得到的UI窗体基类


            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;

            currentShowUIForms.TryGetValue(strUIName, out baseUIForm);
            if (baseUIForm != null) return;

            //把“正在显示集合”与“栈集合”中所有窗体都隐藏。
            foreach (WindowsBase baseUI in currentShowUIForms.Values)
            {
                baseUI.Hiding();
            }
            foreach (WindowsBase staUI in stackCurrentUIForms)
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
        /// 是否清空“栈集合”中de数据
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
    }
}