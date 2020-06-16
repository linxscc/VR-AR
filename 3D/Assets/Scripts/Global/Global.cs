using UnityEngine;
using System.Collections;
using ModelViewerProject.Label3D;
public delegate void CallBack<T,F>(T t,F f);
public delegate void CallBack<T>(T t);
public delegate void CallBack();
/// <summary>
/// 选择场景动画类型
/// </summary>
public enum AnimaiomType
{
    上下运动 = 0, 左右运动 = 1, 前后运动 = 2, 左右旋转 = 3, 上下旋转 = 4, 无动画 = 5
}
/// <summary>
/// 模型操作模式
/// </summary>
public enum ControlType
{
    拆解模式 = 0, 剖面模式 = 1, 逐层模式 = 2, 显微镜 = 3, 其他 = 4
}
/// <summary>
/// 全局变量
/// </summary>
public class Global
{
    /// <summary>
    /// 3D相机
    /// </summary>
    public const string stereo = "Prefab/Stereo Camera";
    /// <summary>
    /// AR视频录制按钮路径
    /// </summary>
    public const string ARCAPTUREBUTTONURL = "Prefab/UI/CaptureModel";
    /// <summary>
    /// AR启动预设存放路径
    /// </summary>
    public const string EASYARURL = "Prefab/EasyAR_Startup";
    /// <summary>
    /// 设置菜单
    /// </summary>
    public const string seMenuUrl = "Prefab/UI/SetMenu";
    /// <summary>
    /// 选择菜单
    /// </summary>
    public const string choiceMenuControlUrl = "Prefab/UI/ChoiceMenu";
    /// <summary>
    /// 确认菜单
    /// </summary>
    public const string confirmMenuControlUrl = "Prefab/UI/ConfirmMenu";
    /// <summary>
    /// 选择菜单按钮
    /// </summary>
    public const string buttonPrefab = "Prefab/UI/Button";
    /// <summary>
    /// 提示UI
    /// </summary>
    public const string hintInfoUrl = "Prefab/UI/HintInfo";
    /// <summary>
    /// 标题栏组
    /// </summary>
    public const string itemfoUrl = "Prefab/UI/Item";
    /// <summary>
    /// 组件按钮
    /// </summary>
    public const string buttonChildUrl = "Prefab/UI/ButtonChild";
    /// <summary>
    /// 菜单动画时长
    /// </summary>
    public static float animationLength = 0.3f;
    /// <summary>
    /// 根目录
    /// </summary>
    public static string Url
    {
        get
        {
            if (Application.isWebPlayer)
            {
                return Application.dataPath;
            }
            else
            {
                return "file://" + Application.dataPath;
            }
        }
    }
    private static UserDataNew userDataNew;
    /// <summary>
    /// 配置文件
    /// </summary>
    public static UserDataNew UserDataNew
    {
        get { return userDataNew; }
        set
        {
            userDataNew = value;
            //  StereoCam.stereoCam.parallaxDistance = float.Parse(value.stereoCamera.point);
            // StereoCam.stereoCam.eyeDistance = float.Parse(value.stereoCamera.eyeDistance);
            // StereoCam.stereoCam.prefabPoint.position = new Vector3(StereoCam.stereoCam.prefabPoint.position.x, StereoCam.stereoCam.prefabPoint.position.y, float.Parse(value.stereoCamera.modelDistance));

        }
    }
    public static string modelName = "001_IRONMAN";
    /// <summary>
    /// 是否预设拆解拆分状态
    /// </summary>
    public static bool isAssemble = false;
    /// <summary>
    /// 当前选择模型的数据
    /// </summary>
    public static LabelDataList labelDataList;
    /// <summary>
    /// 2D，3D模式
    /// </summary>
    public static bool is2D = true;
    /// <summary>
    /// 是否开始录制AR视频
    /// </summary>
    public static bool isCapturing = false;
}
