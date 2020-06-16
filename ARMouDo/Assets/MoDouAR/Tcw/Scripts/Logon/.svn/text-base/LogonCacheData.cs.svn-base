using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MoDouAR
{
    /// <summary>
    /// 登录缓存数据
    /// </summary>
    public class LogonCacheData : MonoBehaviour
    {
        /// <summary>
        /// 用户登录状态
        /// </summary>
        static public bool logonState = false;
        /// <summary>
        /// 服务器 数据信息 → 验证相关
        /// </summary>
        static public ServerReturnData serverReturnData=new ServerReturnData();


        /// <summary>
        /// 获取验证码 API
        /// </summary>
        public const string sendCodeAPI = "http://ar.vplace.com.cn/api/sendCode?";
        /// <summary>
        /// 验证码+Token 登录账号API
        /// </summary>
        public const string detectionCodeAPI = "http://ar.vplace.com.cn/api/login?";
        /// <summary>
        /// 注册账号 API
        /// </summary>
        public const string registerAPI = "http://ar.vplace.com.cn/api/register?";
        /// <summary>
        /// 账号+密码 登录账号API
        /// </summary>
        public const string logonAPI = "http://ar.vplace.com.cn/api/loginByPwd?";
        /// <summary>
        /// 重置密码 API
        /// </summary>
        public const string restPasswordAPI = "http://ar.vplace.com.cn/api/setPwd?";
        /// <summary>
        /// Token验证 API
        /// </summary>
        public const string sendTokenAPI = "http://ar.vplace.com.cn/api/userInfo?";

        /// <summary>
        /// 用户配置文本 名字
        /// </summary>
        static public string GetUserConfig
        {
            get
            {
                return "ModouARUserConfig.txt";
            }
        }
        /// <summary>
        /// 配置文本 路径
        /// </summary>
        static public string GetScreenShotConfigPath
        {
            get
            {
#if UNITY_ANDROID
            return Application.persistentDataPath + "/lib/"+"UserMessage/";
#elif UNITY_IPHONE
                return Application.persistentDataPath + "/lib/" + "UserMessage/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
                string url=  Application.persistentDataPath + "/lib/"+"UserMessage/";
                return url;
#endif
            }
        }
    }

    /// <summary>
    /// 服务器返回信息 
    /// </summary>
    public class ServerReturnData
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
}
