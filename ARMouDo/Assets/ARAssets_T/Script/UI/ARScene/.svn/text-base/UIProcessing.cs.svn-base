using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Tools_XYRF;
using ARKit_T;
using PlaceAR;
using InitARData_T;
using vPlace_zpc;


namespace UI_XYRF
{
    public class UIProcessing
    {
        private UIProcessing() { }
        private static UIProcessing instance;
        public static UIProcessing Instance
        {
            get
            {
                if (instance == null)
                    instance = new UIProcessing();
                return instance;
            }
            set { instance = value; }
        }


        /// <summary>
        /// UI显示与关闭
        /// </summary>
        /// <param name="type">UI类型</param>
        /// <param name="uiName">UI名字</param>
        /// <param name="bol">UI显隐</param>
        public void UI_ShowOrClose(UI_Type type, string uiName, bool bol)
        {
            foreach (var item in UI_CacheData.Instance.UI_TypeCache[type])
            {
                if (item.name == uiName)
                {
                    item.gameObject.SetActive(bol);
                }
            }
        }
        /// <summary>
        /// UI面板 显示与关闭
        /// </summary>
        /// <param name="panelName">面板名字</param>
        /// <param name="bol">UI显隐</param>
        public void UI_ShowOrClose(string panelName, bool bol)
        {
            UI_CacheData.Instance.UI_Panel[panelName].gameObject.SetActive(bol);
        }
        /// <summary>
        /// UI面板子对象 显示与关闭
        /// </summary>
        /// <param name="panelName">面板名字</param>
        /// <param name="bol_">面板子对象 显影</param>
        public void UI_ChildShowOrClose(string panelName, bool bol_)
        {
            for (int i = 0; i < UI_CacheData.Instance.UI_Panel[panelName].childCount; i++)
            {
                UI_CacheData.Instance.UI_Panel[panelName].GetChild(i).gameObject.SetActive(bol_);
            }
        }
        /// <summary>
        /// UI面板子对象 显示与关闭 / 一定名字显影
        /// </summary>
        /// <param name="panelName">面板名字</param>
        /// <param name="bol_">面板子对象 显影</param>
        /// <param name="Name"> 名字</param>
        public void UI_ChildShowOrClose(string panelName, bool bol_, string[] Name)
        {
            for (int i = 0; i < UI_CacheData.Instance.UI_Panel[panelName].childCount; i++)
            {
                for (int c = 0; c < Name.Length; c++)
                {
                    if (Name[c] == UI_CacheData.Instance.UI_Panel[panelName].GetChild(i).name)
                        UI_CacheData.Instance.UI_Panel[panelName].GetChild(i).gameObject.SetActive(bol_);
                }
            }
        }


        /// <summary>
        /// 获取UI 信息 Transform
        /// </summary>
        /// <param name="type">UI 类型</param>
        /// <param name="name">UI名字</param>
        /// <returns></returns>
        public Transform GetUI_Transform(UI_Type type, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                foreach (var item in UI_CacheData.Instance.UI_TypeCache[type])
                {
                    if (item.name == name)
                        return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 改变UI 比例 Transform
        /// </summary>
        /// <param name="type">UI 类型</param>
        /// <param name="name">UI名字</param>
        /// <param name="toScale">目标比例</param>
        /// <param name="t">时间</param>
        public void UI_TransformScale(UI_Type type, string name, Vector3 toScale, float t)
        {
            Transform tra_ = GetUI_Transform(type, name);
            tra_.localScale = Vector3.Lerp(tra_.localScale, toScale, t);
            if (tra_.localScale.x < 0f)
                tra_.localScale = new Vector3(0, 0, 0);
        }
        /// <summary>
        /// 改变UI 比例Transform
        /// </summary>
        /// <param name="tra_">对象</param>
        /// <param name="toScale">目标比例</param>
        /// <param name="t">时间</param>
        public void UI_TransformScale(Transform tra_, Vector3 toScale, float t)
        {
            tra_.localScale = Vector3.Lerp(tra_.localScale, toScale, t);
            if (tra_.localScale.x < 0f)
                tra_.localScale = new Vector3(0, 0, 0);
        }

        /// <summary>
        /// UI透明 处理 [所有子对象 透明变化]  每帧一次
        /// </summary>
        /// <param name="tra_">UI父对象</param>
        /// <param name="toAlpha">期望alpha值 [0-1]</param>
        /// <param name="dex">每帧变化值</param>
        /// <param name="add">增加 / 减少</param>
        public void UI_TransformAlpha(Transform tra_, float toAlpha, float dex, bool add)
        {
            for (int i = 0; i < tra_.childCount; i++)
            {
                if (tra_.GetChild(i).GetComponent<Image>().color.a > 0f && !add)
                {
                    float rgbA = tra_.GetChild(i).GetComponent<Image>().color.a;
                    rgbA -= dex;
                    Color rgb = new Color(255, 255, 255, rgbA);
                    tra_.GetChild(i).GetComponent<Image>().color = rgb;
                }
                else if (tra_.GetChild(i).GetComponent<Image>().color.a < 1f && add)
                {
                    float rgbA = tra_.GetChild(i).GetComponent<Image>().color.a;
                    rgbA += dex;
                    Color rgb = new Color(255, 255, 255, rgbA);
                    tra_.GetChild(i).GetComponent<Image>().color = rgb;
                }
            }
        }
        /// <summary>
        /// UI透明 处理 [所有子对象 透明变化]  瞬间完成
        /// </summary>
        /// <param name="tra_">UI父对象</param>
        /// <param name="toAlpha">期望alpha值 [0-1]</param>
        public void UI_TransformAlpha(Transform tra_, float toAlpha)
        {
            for (int i = 0; i < tra_.childCount; i++)
            {
                Color rgb = new Color(255, 255, 255, toAlpha);
                tra_.GetChild(i).GetComponent<Image>().color = rgb;
            }
        }

        /// <summary>
        /// 开启AR / 关闭AR
        /// </summary>
        /// <param name="open">true=开启</param>
        public void OPenOrCloseARCanvasUI(bool open)
        {

            if (open)  //开启AR场景
            {
                GameObject initDatas = new GameObject();
                initDatas.AddComponent<InitData_T>().OnInit() ;
                //Global.aRkit_Control_T.GetComponent<ARKitControl_T>().dlight.gameObject.SetActive(true);
            }
            else   // 关闭AR场景
            {
                //if (Global.aRkit_Control_T != null)
                   // Object.Destroy(Global.aRkit_Control_T.transform.GetComponent("ARKitControl_T"));     //删除绑定脚本  
                if (Global.promptPanelTra != null)
                    Object.Destroy(Global.promptPanelTra);
                if (Global.particlePrefab != null)
                    Object.Destroy(Global.particlePrefab);
               // if (Global.aRKit_PlaneAnchor != null)
                   // Object.Destroy(Global.aRKit_PlaneAnchor);

                if (Global.StartCanvas != null)
                {
                    Object.Destroy(Global.StartCanvas.transform.GetComponent("UI_Manager"));     //删除绑定脚本 
                    Object.Destroy(Global.StartCanvas.transform.GetComponent("ARKit_OnLineCacheData"));
                    //   Global.StartCanvas.transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                }

                if (Global.aRPanel_Control != null)
                    Object.Destroy(Global.aRPanel_Control);
                if (Global.aRPanel_ShowImage != null)
                    Object.Destroy(Global.aRPanel_ShowImage);


                if (Global.isStartSceneEnterAR)   //返回 主界面
                {
                    if (Global.currentSelectObjectFather != null)
                        Object.Destroy(Global.currentSelectObjectFather);   //删除 生成的模型

                    Global.OperatorModel = OperatorMode.BrowserMode;   //当前场景= 模型选择界面

                    //StartSceneControl.Singleton.Open();

                }
                else     //返回 模型浏览器
                {
                   // StartSceneControl.Singleton.Close(true);
                    ModelBrowserControl.Singleton.Open();

                    //模型信息初始化 
                    if (ProjectConstDefine.hasConfig)
                    {
                        if (Global.planeShadowTra != null)
                            GameObject.Destroy(Global.planeShadowTra);
                        Transform targetTra = null;
                        if (Global.currentSelectObjectFather.transform.childCount > 0)
                            targetTra = Global.currentSelectObjectFather.transform.GetChild(0);
                        if (targetTra != null)
                        {
                            //targetTra.position = Global.currentSelectObjectFather.transform.position;

                            Object.Destroy(targetTra.GetComponent("ScreenHaveMode"));     //删除绑定脚本 
                            Object.Destroy(targetTra.GetComponent("ARKiAnimation_T"));     //删除绑定脚本
                                                                                           //    Object.Destroy(targetTra.GetComponent("OnMouseEvent_T"));     //删除绑定脚本

                            if (targetTra.GetComponent<ARKit_Input>() == null)
                                targetTra.gameObject.AddComponent<ARKit_Input>();

                            targetTra.parent = ModelControl.GetInstance().transform;      // 还原父级
                            targetTra.transform.localPosition = ProjectConstDefine.labelDataList.transform.Position;
                            targetTra.localEulerAngles = ProjectConstDefine.labelDataList.transform.Rotation;
                            targetTra.localScale = ProjectConstDefine.labelDataList.transform.Scale;

                            ModelControl.GetInstance().model = targetTra.gameObject;
                            //ModelControl.GetInstance().SetShadowGround(targetTra.gameObject);

                            if (Global.currentSelectObjectFather != null)
                                GameObject.Destroy(Global.currentSelectObjectFather);
                        }
                    }
                    Global.OperatorModel = OperatorMode.BrowserMode;   //当前场景= 模型浏览器界面
                }
            }

        }
    }
}
