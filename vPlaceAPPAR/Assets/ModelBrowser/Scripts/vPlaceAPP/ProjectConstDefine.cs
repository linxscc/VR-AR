/***
 * 
 *    Title: vPlaceAPP-ModelBrowserView
 *           主题:  项目中常量定义
 *    Description: 
 *           功能: 给项目中的常量加以定义，便于使用
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

using PlaceAR.LabelDatas;
namespace vPlace_zpc
{
    public class ProjectConstDefine
    {
        #region UI窗体名称常量定义

        /// <summary>
        /// 主界面
        /// </summary>
        public const string MODELBROWSER_MAINVIEW = "ModelBrowserMainView";

        /// <summary>
        /// 模型选择界面
        /// </summary>
        public const string MODELBROWSER_SELECTMODELVIEW = "SelectModelView";

        /// <summary>
        /// 浏览方式界面
        /// </summary>
        public const string MODELBROWSER_BROWSERTYPEVIEW = "UIPrefabs/ModelBrowser/BrowserTypeView";

        /// <summary>
        /// 模型简介界面
        /// </summary>
        public const string MODELBROWSER_MODELINTRODUCTIONVIEW = "ModelIntroductionView";

        /// <summary>
        /// 选择模型父级
        /// </summary>
        public const string MODELBROWSER_MODELSCONTENT = "ModelsContent";

        /// <summary>
        /// 模型类目父级
        /// </summary>
        public const string MODELBROWSER_MODELSTYPECONTENT = "ModelsTypeContent";

        /// <summary>
        /// 模型类目选择按钮
        /// </summary>
        public const string MODELBROWSER_MODELTYPEBTN = "UIPrefabs/ModelBrowser/ModelTypeBtn";

        /// <summary>
        /// 模型选择按钮
        /// </summary>
        public const string MODELBROWSER_MODELOBJBTN = "UIPrefabs/ModelBrowser/ModelObjBtn";

        /// <summary>
        /// 浏览方式按钮
        /// </summary>
        public const string MODELBROWSER_BROWSERTYPEITEM = "UIPrefabs/ModelBrowser/BrowserTypeItem";

        /// <summary>
        /// 通用按钮子级
        /// </summary>
        public const string MODELBROWSER_BUTTONCHILD = "UIPrefabs/ModelBrowser/ButtonChild";

        /// <summary>
        /// AR界面
        /// </summary>
        public const string ARVIEW = "ARScene";

        /// <summary>
        /// 模型选择界面
        /// </summary>
        public const string MAINVIEW = "Start";
        #endregion


        #region 全局变量
        /// <summary>
        /// 配置文件信息
        /// </summary>
        public static LabelDataList labelDataList = null;

        /// <summary>
        /// 当前选择模型名称
        /// </summary>
        public static string selectedModelName = null;

        /// <summary>
        /// 当前选择子模型名称
        /// </summary>
        public static string selectedModelChildName = null;

        /// <summary>
        /// 当前选择模型简介
        /// </summary>
        public static string selectedModelDescription = null;

        /// <summary>
        /// 当前选择子模型简介
        /// </summary>
        public static string selectedModelChildDescription = null;

        /// <summary>
        /// 模型配置是否存在
        /// </summary>
        public static bool hasConfig = false;
        #endregion


        #region
        #endregion


        #region
        #endregion


        #region
        #endregion
    }
}