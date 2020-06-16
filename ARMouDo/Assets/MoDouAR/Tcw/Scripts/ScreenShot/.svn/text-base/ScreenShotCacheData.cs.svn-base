using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MoDouAR
{
    /// <summary>
    /// 截图缓存数据
    /// </summary>
    public class ScreenShotCacheData
    {

        /// <summary>
        /// 截图缓存信息
        /// </summary>
        static public TextureData shotCachData = new TextureData();
        /// <summary>
        /// 缓存的截图 key=id
        /// </summary>
        static public Dictionary<int, Texture2D> shotCachTexture = new Dictionary<int, Texture2D>();
        static public Dictionary<int, Sprite> shotCachSprite = new Dictionary<int, Sprite>();
        /// <summary>
        /// 缩略纹理 key=id
        /// </summary>
        static public Dictionary<int, Texture2D> shotCachThumbnails = new Dictionary<int, Texture2D>();
        /// <summary>
        /// 最大缓存数
        /// </summary>
        static public int cacheMaxCount = 45;

        /// <summary>
        /// 最后一个截图
        /// </summary>
        static public Texture2D lastTexture;

        /// <summary>
        /// 截图ID
        /// </summary>
        static public int ScreenShotID
        {
            get
            {
                if (shotCachData == null)
                    shotCachData = new TextureData();
                return shotCachData.dataPhoto.Count;
            }
        }

        /// <summary>
        /// 截图名字
        /// </summary>
        static public string ScreenShotName
        {
            get
            {
                return "墨斗AR";
            }
        }

        /// <summary>
        /// 配置文本名
        /// </summary>
        static public string GetScreenShotConfigName
        {
            get
            {
                return "ModouARScreenShotConfig.txt";
            }
        }

        /// <summary>
        /// 截图路径
        /// </summary>
        static public string GetScreenShotPath
        {
            get
            {
#if UNITY_ANDROID
            return Application.persistentDataPath + "/lib/"+"ScreenShots/Texture/";
#elif UNITY_IPHONE
                return Application.persistentDataPath + "/lib/" + "ScreenShots/Texture/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
                string url=  Application.persistentDataPath + "/lib/"+"ScreenShots/Texture/";
                return url;
#endif
            }
        }
        /// <summary>
        /// 视频路径
        /// </summary>
        static public string GetScreenVideoPath
        {
            get
            {
#if UNITY_ANDROID
            return Application.persistentDataPath + "/lib/"+"ScreenShots/Video/";
#elif UNITY_IPHONE
                return Application.persistentDataPath + "/lib/" + "ScreenShots/Video/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
                string url=  Application.persistentDataPath + "/lib/"+"ScreenShots/Video/";
                return url;
#endif
            }
        }
        /// <summary>
        /// 配置路径
        /// </summary>
        static public string GetScreenShotConfigPath
        {
            get
            {
#if UNITY_ANDROID
            return Application.persistentDataPath + "/lib/"+"ScreenShots/Config/";
#elif UNITY_IPHONE
                return Application.persistentDataPath + "/lib/" + "ScreenShots/Config/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
                string url=  Application.persistentDataPath + "/lib/"+"ScreenShots/Config/";
                return url;
#endif
            }
        }


    }
}
