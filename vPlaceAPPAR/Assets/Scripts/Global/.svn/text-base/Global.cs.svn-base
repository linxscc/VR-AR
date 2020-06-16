/*
 *    日期:2017/6/30
 *    作者:
 *    标题:
 *    功能:全局变量
*/
using UnityEngine;
using System.Collections;
using System.IO;
using PlaceAR;
using System.Collections.Generic;
using PlaceAR.LabelDatas;
using UI_XYRF;
using vPlace_zpc;
using vPlace_FW;

public delegate void CallBack();
public delegate void CallBack<T>(T t);
public delegate void CallBack<T, J>(T t, J j);
public delegate void CallBack<T, J, K>(T t, J j, K k);
public enum OperatorMode
{
    SelectMode,
    BrowserMode,
    ARMode,
    MRModel
}


#region App启动页 数据

/// <summary>
/// 启动页 缓存数据
/// </summary>
public class EnableData
{
    /// <summary>
    /// 是否 更新启动页面数据   isUpdatePageData=1 更新 ,  isUpdatePageData=0 不更新
    /// </summary>
    public int isUpdatePageData;

    /// <summary>
    /// 是否播放[强制]   isForceBroad=1 播放, isForceBroad=0 不强制播放
    /// </summary>
    public int isForceBroad;

    /// <summary>
    /// 该版本 启动页播放次数
    /// </summary>
    public int broadcastNumber;

    /// <summary>
    /// 启动页  至少播放 ? 秒
    /// </summary>
    public int minBroadTime;

    /// <summary>
    /// 进入主页一直存在的 文本
    /// </summary>
    public string mainText;

    /// <summary>
    /// 进入主页一直存在的 图片URL
    /// </summary>
    public string mainTextureURL;

    /// <summary>
    /// 进入主页一直存在的 模型URL
    /// </summary>
    public string mainModeURL;

    /// <summary>
    /// 页面数据 数组
    /// </summary>
    public PageData[] pageData;
}
/// <summary>
/// 页面数据
/// </summary>
public class PageData
{
    /// <summary>
    /// 名字
    /// </summary>
    public string pageName;

    /// <summary>
    /// ID
    /// </summary>
    public int pageID;

    /// <summary>
    /// 停留时间
    /// </summary>
    public float pageWaitTime;

    /// <summary>
    /// 文本
    /// </summary>
    public string pageText;

    /// <summary>
    /// 图片 URL
    /// </summary>
    public string pageTextureURL;

    /// <summary>
    /// 模型 URL
    /// </summary>
    public string pageModeURL;

}
#endregion
#region 用户登录 数据

/// <summary>
/// 用户信息
/// </summary>
public class UserData
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public int userId;
    /// <summary>
    /// 用户名字
    /// </summary>
    public string username;
    /// <summary>
    /// 用户手机号
    /// </summary>
    public string mobile;
    /// <summary>
    /// 用户密码
    /// </summary>
    public string password;
    /// <summary>
    /// 创建时间
    /// </summary>
    public string createTime;

}

/// <summary>
/// 服务器返回信息 
/// </summary>
public class VcodeData
{
    /// <summary>
    /// 服务器code
    /// </summary>
    public int code;
    /// <summary>
    /// 服务器expire
    /// </summary>
    public int expire;
    /// <summary>
    /// 服务器验证token
    /// </summary>
    public string token;
    /// <summary>
    /// 服务器msg
    /// </summary>
    public string msg;
    /// <summary>
    /// 用户信息
    /// </summary>
    public UserData user;
}
#endregion  

/// <summary>
/// 全局变量
/// </summary>
public class Global
{
    #region App启动页相关
    /// <summary>
    /// App启动页 缓存数据
    /// </summary>
    public static EnableData enableData = new EnableData();
    /// <summary>
    /// 更新启动页信息 API
    /// </summary>
    public static string UpdatePageDataAPI = "";
    /// <summary>
    /// 启动页配置 文本名
    /// </summary>
    public static string SendAppEnableName = "SendAppEnableName.txt";
    #endregion
    #region 用户登录相关

    /// <summary>
    /// 用户手机号码
    /// </summary>
    public static string userPhoneNumber;
    /// <summary>
    /// 用户登录状态
    /// </summary>
    public static bool userLogonState = false;
    /// <summary>
    /// 用户发送验证码状态
    /// </summary>
    public static bool isSendCode = false;
    /// <summary>
    /// 发送验证码 下次可发送等待时间
    /// </summary>
    public static int codeMinute = 60;
    /// <summary>
    /// 服务器验证token 文本名
    /// </summary>
    public static string SendTokenInfoName = "SendTokenInfo.txt";
    /// <summary>
    /// 服务器 数据信息 → 验证相关
    /// </summary>
    public static VcodeData SendTokenInfo;
    /// <summary>
    /// 发送短信 API
    /// </summary>
    public static string SendmessageAPI = "http://ar.bdxht.com/api/sendCode?mobile=";
    /// <summary>
    /// 登录API
    /// </summary>
    public static string LogonAppAPI = "http://ar.bdxht.com/api/login?mobile=";
    /// <summary>
    /// 发送Token信息 API
    /// </summary>
    public static string SendTokenAPI = "http://ar.bdxht.com/api/userInfo?";
    #endregion //用户登录相关信息...
    #region  ARKit相关
    /// <summary>
    /// 当前选择的 对象[对象父级空物体]
    /// </summary>
    public static GameObject currentSelectObjectFather;
    /// <summary>
    /// 对象本身
    /// </summary>
    public static GameObject currentSelectObjcetChild;
    /// <summary>
    /// StartCanvas  Canvas
    /// </summary>
    public static GameObject StartCanvas;
    /// <summary>
    /// ARPanel_Control UI 面板组件 
    /// </summary>
    public static GameObject aRPanel_Control;
    /// <summary>
    /// ARPanel_ShowImage UI 面板组件 
    /// </summary>
    public static GameObject aRPanel_ShowImage;
    /// <summary>
    /// ARkit_Control 控制器[相机包含在内]
    /// </summary>
    //public static GameObject aRkit_Control_T;
    /// <summary>
    /// ARKit 对象放置位置提示框
    /// </summary>
    public static GameObject promptPanelTra;
    /// <summary>
    /// ARKit 地形面板
    /// </summary>
    public static GameObject planeTerrainTra;
    /// <summary>
    /// ARkit 阴影面板
    /// </summary>
    public static GameObject planeShadowTra;
    /// <summary>
    /// 粒子特效 点云
    /// </summary>
    public static GameObject particlePrefab;
    /// <summary>
    /// ARKit识别平面显示框
    /// </summary>
    public static GameObject aRKit_PlaneAnchor;
    /// <summary>
    /// ARPanel_Control UI 面板组件预制件 名字
    /// </summary>
    public static readonly string ARPanel_Control_TYZJname = "ARPrefbas/UI/Panel_Control";
    /// <summary>
    /// ARPanel_ShowImage UI 面板组件预制件 名字
    /// </summary>
    public static readonly string ARPanel_ShowImage_TYZJname = "ARPrefbas/UI/Panel_ShowImage";

    /// <summary>
    /// ARkit_Control_T 控制器预制件 名字
    /// </summary>
    public static readonly string ARkit_Control_TYZJname = "ARPrefbas/ARKit/ARkit_Control_T";
    /// <summary>
    /// ARKit 对象放置位置提示框  名字
    /// </summary>
    public static readonly string promptPanelTraYZJname = "ARPrefbas/ARKit/promptPanel";
    /// <summary>
    /// ARKit 地形面板 预制件名字
    /// </summary>
    public static readonly string planeTerrainYZJname = "ARPrefbas/ARKit/PlaneTerrain";
    /// <summary>
    /// ARkit 阴影面板 预制件名字
    /// </summary>
    public static readonly string planeShadowYZJname = "ARPrefbas/ARKit/PlaneShadow";
    /// <summary>
    /// ARKit 点云 预制 名字
    /// </summary>
    public static readonly string pointCloudParticlePrefabYZJname = "ARPrefbas/ARKit/ParticlePrefab";
    /// <summary>
    /// ARKit识别平面显示框 预制名字
    /// </summary>
    public static readonly string ARKit_PlaneAnchorYZJname = "ARPrefbas/ARKit/ARKit_PlaneAnchor";

    /// <summary>
    /// 生成模型的层名
    /// </summary>
    public static readonly string modelLayerMaskName = "model";
    /// <summary>
    /// 3DuiCanvas
    /// </summary>
    public static string canvas3D = "MRPrefab/Canvas3D";
    /// <summary>
    /// 生成模型的层
    /// </summary>
    [HideInInspector]
    public static LayerMask modelLayerMask;
    /// <summary>
    /// 生成模型的标签
    /// </summary>
    [HideInInspector]
    public static readonly string modelTag = "model";
    /// <summary>
    /// Canvas 标签
    /// </summary>
    [HideInInspector]
    public static readonly string startCanvas = "UICamera";
    /// <summary>
    /// AR场景 地形层名
    /// </summary>
    public static readonly string planeTerrainLayerMaskName = "Terrain";
    /// <summary>
    /// AR场景 地形层
    /// </summary>
    public static LayerMask planeTerrainLayerMask;
    /// <summary>
    /// 地形的标签
    /// </summary>
    public static readonly string planeTerrainTag = "terrain";

    /// <summary>
    /// 当前屏幕方向
    /// </summary>
    public static ScreenDirection currentScreenDirection = ScreenDirection.horizontalLeft;
    #endregion  //结束...


    /// <summary>
    /// 3DIUI
    /// </summary>
    public static string typeButton= "MRPrefab/Type3DUI";
    public static string childButton = "MRPrefab/ChildItem3D";
    /// <summary>
    /// AppId
    /// </summary>
    public static string AppleId = "1276058309";
    /// <summary>
    /// 模型按钮
    /// </summary>
    public static string childItem = "UIPrefabs/ChildItem";
    /// <summary>
    /// 选择模型菜单预制物
    /// </summary>
    public static string scrollGridItem = "UIPrefabs/ScrollGridItem";
    /// <summary>
    /// 选择模型菜单
    /// </summary>
    public static string scrollMenu = "UIPrefabs/ScrollMenu";
    /// <summary>
    /// 设置菜单
    /// </summary>
    public static string setView = "UIPrefabs/SetView";
    /// <summary>
    /// 侧边菜单
    /// </summary>
    public static string sideBarView = "UIPrefabs/SideBarView";
    /// <summary>
    /// 提示菜单
    /// </summary>
    public static string printMenu = "UIPrefabs/PrintMenu";
    /// <summary>
    /// 按钮预制物
    /// </summary>
    public static string itemChlid = "UIPrefabs/Item";
    /// <summary>
    /// 下载管理
    /// </summary>
    public static string downLoadView = "UIPrefabs/DownLoadView";
    /// <summary>
    /// 下载管理按钮
    /// </summary>
    public static string downChild = "UIPrefabs/DownChild";
    /// <summary>
    /// 下载菜单
    /// </summary>
    public static string downloadMenu = "UIPrefabs/DownloadMenu";
    /// <summary>
    /// 用户协议界面
    /// </summary>
    public static string userAgreementView = "UIPrefabs/UserAgreement";
    /// <summary>
    /// 开始场景
    /// </summary>
    public static string startScene = "Start";
    /// <summary>
    /// 模型浏览界面
    /// </summary>
    public static string modelBrowser = "ModelBrowserMainView";
    /// <summary>
    /// 启动界面
    /// </summary>
    public static string enablScene = "EnablScene";
    /// <summary>
    /// 判断是否从开始界面进入AR，否则就是模型浏览界面
    /// </summary>
    public static bool isStartSceneEnterAR = true;
    /// <summary>
    /// 服务器地址
    /// </summary>
    public static string severUrl = "ftp://192.168.0.110/lib/";
    /// <summary>
    /// 获取模型类型地址
    /// </summary>
    public static string getModeTypeUrl = "http://ar.vplace.com.cn/api/getModelType";//"http://ar.bdxht.com/api/getModelType";//
    /// <summary>
    /// 获取指定模型类型信息地址
    /// </summary>
    public static string getModelList = "http://ar.vplace.com.cn/api/getModelList?typeId=";//"http://ar.bdxht.com/api/getModelList?typeId=";//

    /// <summary>
    /// 模型的操作模式
    /// </summary>
    private static OperatorMode operatorModel = OperatorMode.SelectMode;
    public static OperatorMode OperatorModel
    {
        get { return operatorModel; }
        set
        {
            operatorModel = value;
            switch (operatorModel)
            {
                case OperatorMode.SelectMode:
                    StartSceneControl.Singleton.Open();
                    ARKitControl.Instance.IntoOther();
                    BrowserTypeViewContol.Instance.Close();
                    isStartSceneEnterAR = true;//解决浏览界面AR和模型选择按钮没有隐藏
                   // ModelControl.GetInstance().DestroyModel();
                   // UIManager.GetInstance().ShowSelectModelBtn();
                    //UIManager.GetInstance().CloseUIForm(ProjectConstDefine.MODELBROWSER_MODELINTRODUCTIONVIEW);
                    ModelBrowserControl.Singleton.Close();
                    break;
                case OperatorMode.BrowserMode:
                    ARKitControl.Instance.IntoOther();
                    break;
                case OperatorMode.ARMode:
                    ARKitControl.Instance.IntoAR();
                    break;
                case OperatorMode.MRModel:
                    ARKitControl.Instance.IntoMR();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 验证token
    /// </summary>
    public static string token;

    public static Dictionary<int, List<ItemData>> itemData = new Dictionary<int, List<ItemData>>();
    private static Camera cam;
    public static Camera camera
    {
        get
        {
            // if (cam == null)
            // {

            //  }
            switch (operatorModel)
            {
                case OperatorMode.SelectMode:
                    cam = GameObject.FindGameObjectWithTag(Tag.camera).GetComponent<Camera>();
                    break;
                case OperatorMode.BrowserMode:
                    cam = GameObject.FindGameObjectWithTag(Tag.camera).GetComponent<Camera>();
                    break;
                case OperatorMode.ARMode:
                    cam = GameObject.FindGameObjectWithTag(Tag.mainCamera).GetComponent<Camera>();
                    break;
                case OperatorMode.MRModel:
                    cam = GameObject.FindGameObjectWithTag(Tag.mainCamera).GetComponent<Camera>();
                    break;
                default:
                    //cam = GameObject.FindGameObjectWithTag(Tag.camera).GetComponent<Camera>();
                    break;
            }
            return cam;
        }
    }
    /// <summary>
    /// 不在wifi下是否下载
    /// </summary>
    private static bool atwifi = false;
    public static bool Atwifi
    {
        get { return atwifi; }
        set
        {
            atwifi = value;
            if (atwifi == false)
                DownPrefab.downPrefab.StopLoad();
        }
    }

    /// <summary>
    /// 本地地址
    /// </summary>
    public static string LocalUrl
    {
        get
        {
#if UNITY_ANDROID
            return Application.persistentDataPath + "/lib/";
#elif UNITY_IPHONE
            return Application.persistentDataPath + "/lib/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
                string url=  Application.persistentDataPath + "/lib/";
               
               
               // if (!Directory.Exists(url))
               // {
                   // Directory.CreateDirectory(url);
              //  }
                return url;
#endif

        }
    }
}
