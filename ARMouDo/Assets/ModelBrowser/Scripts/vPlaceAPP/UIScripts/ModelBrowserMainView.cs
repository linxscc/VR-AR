/***
 * 
 *    Title: vPlaceAPP-ModelBrowserView
 *           主题:  APP-模型浏览主界面
 *    Description: 
 *           功能: 模型浏览界面中各个功能的入口及展示页面
 *           1: 
 *           2: 
 *           3: 
 *           4: 
 *                          
 *    Date: 
 *    Version: 
 *	  Author: 
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using vPlace_FW;
using UnityEngine.UI;
using PlaceAR.LabelDatas;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UI_XYRF;
using PlaceAR;
using ARKit_T;

namespace vPlace_zpc
{
    public class ModelBrowserMainView : MonoBehaviour
    {
        #region 字段

        /// <summary>
        /// Canvas
        /// </summary>
        private Transform startCanvas = null;

        /// <summary>
        /// 模型选择按钮
        /// </summary>
        public GameObject selectModelBtn = null;

        /// <summary>
        /// AR按钮
        /// </summary>
        public GameObject enterARBtn = null;

        /// <summary>
        /// 显微镜缩放条
        /// </summary>
        public GameObject zoomInMode = null;

        /// <summary>
        /// 退回开始界面按钮
        /// </summary>
        public GameObject goBackBtn = null;

        /// <summary>
        /// 浏览方式按钮
        /// </summary>
        public GameObject browserTypeBtn = null;

        /// <summary>
        /// 模型简介按钮
        /// </summary>
        public GameObject modelIntroBtn = null;

        /// <summary>
        /// 当前模型名称
        /// </summary>
        public Text currentModelName = null;

        /// <summary>
        /// 是否打开浏览方式界面
        /// </summary>
        private bool isOpenBrowserTypeView = true;

        /// <summary>
        /// 是否打开模型简介界面
        /// </summary>
        private bool isOpenModelIntroView = true;

        /// <summary>
        /// 浏览方式按钮初始图
        /// </summary>
        private Sprite browserBtnOriginSprite = null;

        /// <summary>
        /// 浏览方式按钮按下图
        /// </summary>
        private Sprite browserBtnPressedSprite = null;

        /// <summary>
        /// 模型简介按钮初始图
        /// </summary>
        private Sprite modelIntroBtnOriginSprite = null;

        /// <summary>
        /// 模型简介按钮按下图
        /// </summary>
        private Sprite modelIntroBtnPressedSprite = null;

       // private static ModelBrowserMainView instance = null;
       // public static ModelBrowserMainView GetInstance()
      //  {
           // if (instance == null)
           // {

               // GameObject obj = new GameObject("MainView");
               // DontDestroyOnLoad(obj);
               // instance = obj.AddComponent<ModelBrowserMainView>();
           // }
           // return instance;
       // }
        #endregion


        #region 方法

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            OnInit();
           // if (instance != null)
          //  {
               // if (instance != this)
                   // Destroy(gameObject);
          //  }
         //   else
               // instance = this;

            //给本窗体的按钮注册事件
            EventTriggerListener.Get(enterARBtn).onClick = EnterARView;

            EventTriggerListener.Get(selectModelBtn).onClick = OpenSelectModelView;

            EventTriggerListener.Get(goBackBtn).onClick = GoBackStartView;

        }
        #endregion

        /// <summary>
        /// 从模型浏览界面进入AR界面
        /// </summary>
        /// <param name="obj"></param>
        private void EnterARView(GameObject obj)
        {
            Global.OperatorModel = OperatorMode.ARMode;
            Global.isStartSceneEnterAR = false;
            BrowserTypeViewContol.Instance.Close();
            UIManager.GetInstance().ShowSelectModelBtn();
            ModelBrowserControl.Singleton.Close();
            UIProcessing.Instance.OPenOrCloseARCanvasUI(true);
            ModelControl.GetInstance().EnterAR();
        }

        /// <summary>
        /// 打开模型选择界面
        /// </summary>
        /// <param name="obj"></param>
        private void OpenSelectModelView(GameObject obj)
        {
            BrowserTypeViewContol.Instance.Close();
            ScrollMenuControl.Singleton.Open();
            UIManager.GetInstance().HideSeleModelBtn();
            if (!isOpenBrowserTypeView)
                browserTypeBtn.GetComponent<Image>().sprite = browserBtnOriginSprite;
            isOpenBrowserTypeView = true;
        }

        /// <summary>
        /// 退回主界面
        /// </summary>
        /// <param name="obj"></param>
        private void GoBackStartView(GameObject obj)
        {
            Global.isStartSceneEnterAR = true;
            Global.OperatorModel = OperatorMode.BrowserMode;
           // BrowserTypeViewContol.Instance.Close();
            ShowOrHideModelName(false);
            ModelControl.GetInstance().DestroyModel();
            UIManager.GetInstance().ShowSelectModelBtn();
            UIManager.GetInstance().CloseUIForm(ProjectConstDefine.MODELBROWSER_MODELINTRODUCTIONVIEW);
            ModelBrowserControl.Singleton.Close();
           // StartSceneControl.Singleton.Open();
        }

        /// <summary>
        /// 静态挂载函数-打开模型简介界面
        /// </summary>
        public void OpenModelIntroView()
        {
            ModelIntroBtn();
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        void OnInit()
        {
            browserBtnOriginSprite = browserTypeBtn.GetComponent<Image>().sprite;
            browserBtnPressedSprite = browserTypeBtn.GetComponent<Button>().spriteState.pressedSprite;
            modelIntroBtnOriginSprite = modelIntroBtn.GetComponent<Image>().sprite;
            modelIntroBtnPressedSprite = modelIntroBtn.GetComponent<Button>().spriteState.pressedSprite;
            //browserTypeBtn.GetComponent<Image>().sprite = browserTypeBtn.GetComponent<Button>().spriteState.disabledSprite;
            //modelIntroBtn.GetComponent<Image>().sprite = modelIntroBtn.GetComponent<Button>().spriteState.disabledSprite;
            browserTypeBtn.GetComponent<Button>().interactable = false;
            modelIntroBtn.GetComponent<Button>().interactable = false;
        }

        /// <summary>
        /// 在主界面显示模型名称
        /// </summary>
        /// <param name="isShow">显示还是隐藏</param>
        public void ShowOrHideModelName(bool isShow)
        {
            if (isShow)
            {
                currentModelName.gameObject.SetActive(isShow);
                currentModelName.text = ProjectConstDefine.selectedModelName;
            }
            else
                currentModelName.gameObject.SetActive(isShow);
        }

        /// <summary>
        /// 静态挂在函数-打开模型浏览功能UI
        /// </summary>
        public void BrowserTypeViewBtn()
        {
            if (ProjectConstDefine.hasConfig)
            {
                OpenBrowserViewAndCloseIntroView();
                BrowserTypeButtonSpriteInit(isOpenBrowserTypeView);
                if (isOpenBrowserTypeView)
                {
                    BrowserTypeViewContol.Instance.Open();
                    ScrollMenuControl.Singleton.Close(null);
                    UIManager.GetInstance().ShowSelectModelBtn();
                }
                else
                    BrowserTypeViewContol.Instance.Close();

                isOpenBrowserTypeView = !isOpenBrowserTypeView;
            }
            else
                Debug.Log("缺少配置文件，模型浏览禁用！");
        }

        /// <summary>
        /// 浏览类型按钮Sprite控制
        /// </summary>
        /// <param name="isPressed">是否按下该按钮</param>
        private void BrowserTypeButtonSpriteInit(bool isPressed)
        {
            if (isPressed)
                browserTypeBtn.GetComponent<Image>().sprite = browserBtnPressedSprite;
            else
                browserTypeBtn.GetComponent<Image>().sprite = browserBtnOriginSprite;
        }

        /// <summary>
        /// 模型简介按钮
        /// </summary>
        public void ModelIntroBtn()
        {
            if (ProjectConstDefine.hasConfig)
            {
                ModelIntroButtonSpriteInit(isOpenModelIntroView);
                if (isOpenModelIntroView)
                    UIManager.GetInstance().ShowUIForm(ProjectConstDefine.MODELBROWSER_MODELINTRODUCTIONVIEW);
                else
                    UIManager.GetInstance().CloseUIForm(ProjectConstDefine.MODELBROWSER_MODELINTRODUCTIONVIEW);

                isOpenModelIntroView = !isOpenModelIntroView;
            }
            else
                Debug.Log("缺少配置文件，模型简介禁用！");
        }

        /// <summary>
        /// 模型简介按钮Sprite控制
        /// </summary>
        /// <param name="isPressed">是否按下该按钮</param>
        private void ModelIntroButtonSpriteInit(bool isPressed)
        {
            if (isPressed)
                modelIntroBtn.GetComponent<Image>().sprite = modelIntroBtnPressedSprite;
            else
                modelIntroBtn.GetComponent<Image>().sprite = modelIntroBtnOriginSprite;
        }

        /// <summary>
        /// 打开浏览功能界面时关闭模型简介界面
        /// </summary>
        private void OpenBrowserViewAndCloseIntroView()
        {
            if (UIManager.GetInstance())
                UIManager.GetInstance().CloseUIForm(ProjectConstDefine.MODELBROWSER_MODELINTRODUCTIONVIEW);
            if (!isOpenModelIntroView)
                modelIntroBtn.GetComponent<Image>().sprite = modelIntroBtnOriginSprite;
            isOpenModelIntroView = true;
        }
        /// <summary>
        /// 浏览类型按钮Sprite、模型简介按钮Sprite控制
        /// </summary>
        /// <param name="isEnable">是否禁用该按钮</param>
        public void BtnUIControl(bool isEnable)
        {
            if (isEnable)
            {
                modelIntroBtn.GetComponent<Button>().interactable = true;
                //modelIntroBtn.GetComponent<Image>().sprite = modelIntroBtnOriginSprite;
                if (ProjectConstDefine.labelDataList.controlType != 4)
                {
                    browserTypeBtn.GetComponent<Button>().interactable = true;
                    //browserTypeBtn.GetComponent<Image>().sprite = browserBtnOriginSprite;
                }
                else
                {
                    browserTypeBtn.GetComponent<Button>().interactable = false;
                    //browserTypeBtn.GetComponent<Image>().sprite = browserTypeBtn.GetComponent<Button>().spriteState.disabledSprite;
                }
            }
            else
            {
                browserTypeBtn.GetComponent<Button>().interactable = false;
                modelIntroBtn.GetComponent<Button>().interactable = false;
                //browserTypeBtn.GetComponent<Image>().sprite = browserTypeBtn.GetComponent<Button>().spriteState.disabledSprite;
                //modelIntroBtn.GetComponent<Image>().sprite = modelIntroBtn.GetComponent<Button>().spriteState.disabledSprite;
            }
        }

        /// <summary>
        /// 打开模型浏览UI主界面
        /// </summary>
        public void Open()
        {
            if (!startCanvas)
                startCanvas = GameObject.FindGameObjectWithTag("UICamera").transform;
            //startCanvas.GetComponent<StartCanvas>().rotaterUI.Add(selectModelBtn);
            //startCanvas.GetComponent<StartCanvas>().rotaterUI.Add(enterARBtn);
            transform.SetParent(startCanvas);
            transform.GetComponent<RectTransform>().sizeDelta = Vector2.one;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.SetAsLastSibling();
            gameObject.SetActive(true);
            if (Global.isStartSceneEnterAR)
                UIManager.GetInstance().HideSeleModelBtn();
            else
                ShowOrHideModelName(true);
        }

        /// <summary>
        /// 关闭模型浏览UI界面
        /// </summary>
        public void Close()
        {
            transform.SetParent(null);
            if (Global.OperatorModel == OperatorMode.BrowserMode)
                BtnUIControl(false);
            gameObject.SetActive(false);
        }
    }
}
