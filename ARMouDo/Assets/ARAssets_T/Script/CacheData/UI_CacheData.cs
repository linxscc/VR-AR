using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace UI_XYRF
{
    /// <summary>
    /// 缓存
    /// </summary>
    public class UI_CacheData
    {
        /// <summary>
        /// UI 类型缓存
        /// </summary>
        public Dictionary<UI_Type, List<Transform>> UI_TypeCache = new Dictionary<UI_Type, List<Transform>>();
        /// <summary>
        /// UI 面板
        /// </summary>
        public Dictionary<string, Transform> UI_Panel = new Dictionary<string, Transform>();
        /// <summary>
        /// 当前截屏 图片
        /// </summary>
        public Texture2D currentSelectTexture2dFromGalley;

        private UI_CacheData() { }
        private static UI_CacheData instance;
        public static UI_CacheData Instance
        {
            get
            {
                if (instance == null)
                    instance = new UI_CacheData();
                return instance;
            }
            set { instance = value; }
        }
    }

    /// <summary>
    /// UI类型
    /// </summary>
    public enum UI_Type
    {
        Button,
        Slider,
        Toogle,
        Dropdown,
        InputField,
        Image,
        Text
    }
    public enum ScreenDirection
    {
        /// <summary>
        /// 左横
        /// </summary>
        horizontalLeft,
        /// <summary>
        /// 右横
        /// </summary>
        horizontalRight,
        /// <summary>
        /// 竖正
        /// </summary>
        verticalTo,
        /// <summary>
        /// 竖反
        /// </summary>
        verticalBack
    }
}
