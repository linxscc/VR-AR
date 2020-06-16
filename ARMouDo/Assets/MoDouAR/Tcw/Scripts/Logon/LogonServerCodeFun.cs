using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using System;

namespace MoDouAR
{
    /// <summary>
    /// 登录 [服务器验证] 
    /// </summary>
    public class LogonServerCodeFun
    {
        /// <summary>
        /// 当执行成功
        /// </summary>
        public delegate void OnLogonServerSuccess();
        /// <summary>
        /// 当执行失败
        /// </summary>
        /// <param name="error">错误信息</param>
        public delegate void OnLogonServerLost(string error);
        /// <summary>
        /// 验证码Token
        /// </summary>
        private string vcodeToken = "";

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="codeAPI">API</param>
        /// <param name="userPhoneNumber">手机号</param>
        /// <param name="onLogonServerSuccess">成功回调</param>
        /// <param name="onLogonServerLost">失败回调</param>
        public void UserSendConde(string codeAPI, string userPhoneNumber, OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            TCoroutine.Instance.TStartCoroutine(SendCode_(codeAPI, userPhoneNumber, onLogonServerSuccess, onLogonServerLost));
        }
        /// <summary>
        /// 验证码+vcodeToken    账号密码登录
        /// </summary>
        /// <param name="detectionAPI">API</param>
        /// <param name="userPhoneNumber">手机号</param>
        /// <param name="codeNumber">验证码</param>
        /// <param name="onLogonServerSuccess">成功回调</param>
        /// <param name="onLogonServerLost">失败回调</param>
        /// <returns></returns>
        public void UserDetectionCode(string detectionAPI, string userPhoneNumber, string codeNumber, OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            TCoroutine.Instance.TStartCoroutine(DetectionCode_(detectionAPI, userPhoneNumber, codeNumber, onLogonServerSuccess, onLogonServerLost));
        }
        /// <summary>
        /// 账号+密码 登录账号
        /// </summary>
        /// <param name="registerAPI">API</param>
        /// <param name="userPhoneNumber">手机号</param>
        /// <param name="userPassword">密码</param>
        /// <param name="onLogonServerSuccess">成功回调</param>
        /// <param name="onLogonServerLost">失败回调</param>
        /// <returns></returns>
        public void UserLogon(string logonAPI, string userPhoneNumber, string userPassword, OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            TCoroutine.Instance.TStartCoroutine(RegisterUser(logonAPI, userPhoneNumber, userPassword, onLogonServerSuccess, onLogonServerLost));
        }
        /// <summary>
        /// 验证Token 信息
        /// </summary>
        /// <param name="onLogonServerSuccess">成功回调</param>
        /// <param name="onLogonServerLost">失败回调</param>
        public void UserTokenCodeing(OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            TCoroutine.Instance.TStartCoroutine(DetectionUserToken(onLogonServerSuccess, onLogonServerLost));
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="registerAPI">API</param>
        /// <param name="userPhoneNumber">密码</param>
        /// <param name="onLogonServerSuccess">成功回调</param>
        /// <param name="onLogonServerLost">失败回调</param>
        /// <returns></returns>
        public void UserRestPassword(string restPasAPI, string userPassword, OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            TCoroutine.Instance.TStartCoroutine(RestPassword(restPasAPI, userPassword, onLogonServerSuccess, onLogonServerLost));
        }
        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="registerAPI">API</param>
        /// <param name="userPhoneNumber">手机号</param>
        /// <param name="userPassword">密码</param>
        /// <param name="onLogonServerSuccess">成功回调</param>
        /// <param name="onLogonServerLost">失败回调</param>
        /// <returns></returns>
        public void UserRegister(string registerAPI, string userPhoneNumber, string userPassword, OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            TCoroutine.Instance.TStartCoroutine(RegisterUser(registerAPI, userPhoneNumber, userPassword, onLogonServerSuccess, onLogonServerLost));
        }


        #region  私有方法
        private IEnumerator SendCode_(string codeAPI, string userPhoneNumber, OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            WWWForm wwwform = new WWWForm();
            wwwform.AddField("mobile", userPhoneNumber);
            WWW www = new WWW("http://ar.vplace.com.cn/api/sendCode?", wwwform);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                onLogonServerLost("验证码发送失败:" + www.error);
            }
            else
            {
                ServerReturnData da_ = JsonFx.Json.JsonReader.Deserialize<ServerReturnData>(www.text);
                if (da_ == null)  //网络断开
                {
                    onLogonServerLost("验证码发送失败:" + "网络已断开......");
                    yield return null;
                }
                else
                {
                    if (da_.msg == null)  //成功
                    {
                        vcodeToken = da_.token;
                        onLogonServerSuccess();
                    }
                    else
                        onLogonServerLost("验证码发送失败:" + "请稍后重试......");
                }
            }
        }
        private IEnumerator DetectionCode_(string detectionAPI, string userPhoneNumber, string codeNumber, OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            if (LogonCacheData.serverReturnData != null)
            {
                WWWForm w = new WWWForm();
                w.AddField("mobile", userPhoneNumber);
                w.AddField("vcode", codeNumber);
                w.AddField("vcodeToken", vcodeToken);
                WWW www = new WWW(detectionAPI, w);
                yield return www;
                try
                {
                    ServerReturnData da_ = JsonFx.Json.JsonReader.Deserialize<ServerReturnData>(www.text);
                    if (da_ == null)  //网络断开
                        onLogonServerLost("网络已断开......");
                    else
                    {
                        if (da_.msg == null)  //成功
                        {
                            UserData usM = null; ;
                            if (LogonCacheData.serverReturnData.user == null)
                            {
                                usM = new UserData();
                                usM.mobile = userPhoneNumber;
                                LogonCacheData.serverReturnData.user = usM;
                            }
                            else
                                LogonCacheData.serverReturnData.user.mobile = userPhoneNumber;
                            LogonCacheData.serverReturnData.token = da_.token;
                            FileTools.CreateFile(LogonCacheData.GetScreenShotConfigPath, LogonCacheData.GetUserConfig, JsonFx.Json.JsonWriter.Serialize(LogonCacheData.serverReturnData));
                            onLogonServerSuccess();
                        }
                        else
                            onLogonServerLost("验证失败:" + "请稍后重试......");
                    }
                }
                catch (Exception e)
                {
                    onLogonServerLost("验证失败:" + www.error);
                }
            }
            else
                onLogonServerLost("请先获取验证码");
        }
        private IEnumerator LogonMoDouAR(string logonAPI, string userPhoneNumber, string userPassword, OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            WWWForm w = new WWWForm();
            w.AddField("mobile", userPhoneNumber);
            w.AddField("password", userPassword);
            WWW www = new WWW(logonAPI, w);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                onLogonServerLost("登录失败:" + www.error);
            }
            else
            {
                ServerReturnData da_ = JsonFx.Json.JsonReader.Deserialize<ServerReturnData>(www.text);
                if (da_ == null)  //网络断开
                {
                    onLogonServerLost("网络已断开......");
                    yield return null;
                }
                else
                {
                    if (da_.msg == null)  //成功
                    {
                        UserData usM = null; ;
                        if (LogonCacheData.serverReturnData.user == null)
                        {
                            usM = new UserData();
                            usM.mobile = userPhoneNumber;
                            usM.password = userPassword;
                            LogonCacheData.serverReturnData.user = usM;
                        }
                        else
                        {
                            LogonCacheData.serverReturnData.user.mobile = userPhoneNumber;
                            LogonCacheData.serverReturnData.user.password = userPassword;
                        }
                        LogonCacheData.serverReturnData.token = da_.token;
                        FileTools.CreateFile(LogonCacheData.GetScreenShotConfigPath, LogonCacheData.GetUserConfig, JsonFx.Json.JsonWriter.Serialize(LogonCacheData.serverReturnData));
                        onLogonServerSuccess();
                    }
                    else
                        onLogonServerLost(da_.msg);
                }
            }
        }
        private IEnumerator DetectionUserToken(OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            ServerReturnData da_ = null;
            if (FileFolders.FileExist(LogonCacheData.GetScreenShotConfigPath, LogonCacheData.GetUserConfig))
            {
                LogonCacheData.serverReturnData = FileTools.ReadText<ServerReturnData>(LogonCacheData.GetScreenShotConfigPath + LogonCacheData.GetUserConfig);
                if (!string.IsNullOrEmpty(LogonCacheData.serverReturnData.token))  //若存在Token编码 → 访问服务器
                {
                    WWW www = new WWW(LogonCacheData.sendTokenAPI + "token=" + LogonCacheData.serverReturnData.token);
                    yield return www;
                    if (!string.IsNullOrEmpty(www.error))
                    {
                        onLogonServerLost("Token验证失败" + www.error);
                    }
                    else
                    {
                        da_ = JsonFx.Json.JsonReader.Deserialize<ServerReturnData>(www.text);
                        // 验证
                        if (da_ == null)
                        {
                            onLogonServerLost("Token验证失败");
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(da_.token))
                            {
                                LogonCacheData.serverReturnData.token = da_.token;
                                FileTools.CreateFile(LogonCacheData.GetScreenShotConfigPath, LogonCacheData.GetUserConfig, JsonFx.Json.JsonWriter.Serialize(LogonCacheData.serverReturnData));
                            }
                            onLogonServerSuccess();
                        }
                    }
                }
                else
                    onLogonServerLost("Token验证失败");
            }
            else
            {
                onLogonServerLost("用户未注册账号");
            }
        }
        private IEnumerator RestPassword(string restPasAPI, string userPassword, OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            WWWForm wwwform = new WWWForm();
            wwwform.AddField("password", userPassword);
            wwwform.AddField("token", LogonCacheData.serverReturnData.token);
            WWW www = new WWW(restPasAPI, wwwform);
            yield return www;
            try
            {
                ServerReturnData da_ = JsonFx.Json.JsonReader.Deserialize<ServerReturnData>(www.text);
                if (da_ == null)  //网络断开
                {
                    onLogonServerLost("网络已断开......");
                }
                else
                {
                    if (da_.msg == null)  //成功
                    {
                        UserData usM = null; ;
                        if (LogonCacheData.serverReturnData.user == null)
                        {
                            usM = new UserData();
                            usM.password = userPassword;
                            LogonCacheData.serverReturnData.user = usM;
                        }
                        else
                            LogonCacheData.serverReturnData.user.password = userPassword;
                        FileTools.CreateFile(LogonCacheData.GetScreenShotConfigPath, LogonCacheData.GetUserConfig, JsonFx.Json.JsonWriter.Serialize(LogonCacheData.serverReturnData));
                        onLogonServerSuccess();
                    }
                    else
                        onLogonServerLost(da_.msg);
                }
            }
            catch (Exception e)
            {
                onLogonServerLost("重置失败:" + e);
            }
        }
        private IEnumerator RegisterUser(string registerAPI, string userPhoneNumber, string userPassword, OnLogonServerSuccess onLogonServerSuccess, OnLogonServerLost onLogonServerLost)
        {
            WWWForm w = new WWWForm();
            w.AddField("mobile", userPhoneNumber);
            w.AddField("password", userPassword);
            WWW www = new WWW(registerAPI, w);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                onLogonServerLost("注册失败:" + www.error);
            }
            else
            {
                ServerReturnData da_ = JsonFx.Json.JsonReader.Deserialize<ServerReturnData>(www.text);
                if (da_ == null)  //网络断开
                {
                    onLogonServerLost("网络已断开......");
                    yield return null;
                }
                else
                {
                    if (da_.msg == null)  //成功
                    {
                        UserData usM = new UserData();
                        usM.mobile = userPhoneNumber;
                        usM.password = userPassword;
                        da_.user = usM;
                        LogonCacheData.serverReturnData = da_;
                        FileTools.CreateFile(LogonCacheData.GetScreenShotConfigPath, LogonCacheData.GetUserConfig, JsonFx.Json.JsonWriter.Serialize(LogonCacheData.serverReturnData));
                        onLogonServerSuccess();
                    }
                    else
                        onLogonServerLost(da_.msg);
                }
            }
        }
        #endregion  //...
    }
}
