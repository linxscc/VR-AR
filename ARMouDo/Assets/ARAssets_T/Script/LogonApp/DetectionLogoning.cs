using DG.Tweening;
using MiniJSON;
using PlaceAR;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tools_XYRF;
using UnityEngine;


namespace UI_XYRF
{
    /// <summary>
    /// LogonApp 登录相关 ...
    /// </summary>
    public class DetectionLogoning : MonoBehaviour
    {
        //请求的Json数据
        private Dictionary<string, string> UserDic = new Dictionary<string, string>();
        // 发送验证码 + 登录
        //加入http 头信息  
        private Dictionary<string, string> JsonDicSend = new Dictionary<string, string>();
        //转换为字节 
        byte[] post_dataSend;


        #region Token验证
        /// <summary>
        /// 检测用户是否登录 / 是否登录过
        /// </summary>
        public void DetectionToken()
        {
            StartCoroutine(Detection_LogonData());
        }
        private IEnumerator Detection_LogonData()
        {
            VcodeData da_ = null;
            if (File.Exists(Global.LocalUrl + Global.SendTokenInfoName))
            {
                Global.SendTokenInfo = FileTools.ReadText<VcodeData>(Global.LocalUrl + Global.SendTokenInfoName);
                if (!string.IsNullOrEmpty(Global.SendTokenInfo.token))  //若存在Token编码 → 访问服务器
                {
                    WWW www = new WWW(Global.SendTokenAPI + "token=" + Global.SendTokenInfo.token);  //服务器端错误 待修正;
                    yield return www;

                    if (!string.IsNullOrEmpty(www.error))
                    {
                        Debug.LogError("error:" + www.error);
                    }
                    else
                    {
                        da_ = JsonFx.Json.JsonReader.Deserialize<VcodeData>(www.text);
                        // 验证
                        if (da_ == null)
                        {
                            Global.userLogonState = false;  //验证失败 
                            EventComeBack_T.OnUserQuits();
                        }
                        else
                        {
                            if (da_.msg == null)  //成功
                            {
                                Global.userLogonState = true;  //验证成功
                                da_.token = Global.SendTokenInfo.token;
                                da_.code = Global.SendTokenInfo.code;
                                da_.expire = Global.SendTokenInfo.expire;
                                da_.msg = Global.SendTokenInfo.msg;
                                EventComeBack_T.OnUserLogonings();
                            }
                            else
                            {
                                Global.userLogonState = false;  //验证失败
                                EventComeBack_T.OnUserQuits();
                            }
                        }
                    }
                }
                else
                {
                    Global.userLogonState = false;  //验证失败   
                    EventComeBack_T.OnUserQuits();
                }
            }
            else
            {
                Global.userLogonState = false;  //验证失败   
                EventComeBack_T.OnUserQuits();
            }
            //检测状态回馈
            DetectionState(da_);
        }
        private void DetectionState(VcodeData data_)
        {
            string sendMessing = null;
            string messing = null;
            if (Global.userLogonState)  //登录成功
            {
                Global.SendTokenInfo = data_;
                FileTools.CreateFile(Global.LocalUrl, Global.SendTokenInfoName, JsonFx.Json.JsonWriter.Serialize(Global.SendTokenInfo));
                //自动验证成功...
                Debug.Log("Token验证成功!");
            }
            else  //提示 验证失败...
            {
                sendMessing = "未登录";
                messing = "请登录账号...";
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    // IOSMessage.Create(sendMessing, messing, "好的");
                }
                else if (Application.platform == RuntimePlatform.Android)
                { }
                else
                    Debug.Log(sendMessing + ":" + messing);
            }
        }
        #endregion //Token验证 结束...

        #region 发送验证码 / 登录App
        /// <summary>
        /// 发送验证码 / 登录App
        /// </summary>
        /// <param name="url">API</param>
        /// <param name="send">1.发送信息 | 2.登录App </param>
        /// <returns></returns>
        public IEnumerator Post_LogonApp(string url, int send)
        {
            WWW www = new WWW(url, post_dataSend, JsonDicSend);
            yield return www;

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError("error:" + www.error);
            }
            else
            {
                VcodeData da_ = JsonFx.Json.JsonReader.Deserialize<VcodeData>(www.text);

                if (da_ == null)  //网络断开
                {
                    if (send == 1)
                        Global.isSendCode = false;  //验证码发送失败
                    else if (send == 2)
                    {
                        Global.userLogonState = false;  //登录失败  
                        EventComeBack_T.OnUserQuits();
                    }
                    yield return null;
                }
                else
                {
                    if (da_.msg == null)  //成功
                    {
                        Global.SendTokenInfo = da_;  //信息记录
                        if (send == 1)
                            Global.isSendCode = true;  //验证码发送成功
                        if (send == 2)  //如果登录App 成功 则 写入配置token
                        {
                            FileTools.CreateFile(Global.LocalUrl, Global.SendTokenInfoName, www.text);
                            Global.userLogonState = true;  //登录成功
                            EventComeBack_T.OnUserLogonings();
                        }
                    }
                    else
                    {
                        if (send == 1)  //发送信息失败
                            Global.isSendCode = false;  //验证码发送失败...
                        else if (send == 2)  //登录App失败
                        {
                            Global.userLogonState = false;  //登录失败    
                            EventComeBack_T.OnUserQuits();
                        }
                    }
                }
                // 3. 状态回馈
                LogoState(send);
            }
        }
        private void LogoState(int send)
        {
            string sendMessing = null;
            string messing = null;
            if (send == 1 && Global.isSendCode)  //发送验证码 成功
            {
                sendMessing = "成功";
                messing = "验证码发送成功...";
            }
            else if (send == 2)  //登录
            {
                if (Global.userLogonState)  //登录成功
                {
                    LogonApplication.Instance.loginView.DOScale(Vector3.zero, 0.3f);
                    Sprite spr_ = ResourcesLod_T.ResourcesLoad_SPR("ARTexture", "sidebar-btn-avatar-1@3x");
                    LogonApplication.Instance.loginBtnHead.sprite = spr_;
                    LogonApplication.Instance.userAccount.text = Global.userPhoneNumber;
                    LogonApplication.Instance.ClearInputNumber();
                    LogonApplication.Instance.codeNumber.text = null;
                    Debug.Log("登录成功!");
                }
                else   //登录失败
                {
                    sendMessing = "登录失败";
                    messing = "请输入正确的验证码...";
                }
            }
            //   if ((send == 1 && Global.isSendCode) || !Global.userLogonState)
            if (send == 2 && !Global.userLogonState)
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                    IOSMessage.Create(sendMessing, messing, "好的");
                else if (Application.platform == RuntimePlatform.Android)
                { }
                else
                    Debug.Log(sendMessing + ":" + messing);
            }
        }
        #endregion //发送验证码 / 登录App 结束...

        #region 登录状态改变 回调函数
        /// <summary>
        /// 当用户登录 成功 [回调方法]
        /// </summary>
        private void EventComeBack_T_OnUserLogoning()
        {
            //重置验证码发送UI
            LogonApplication.Instance.LogonSucess();
        }
        /// <summary>
        /// 当用户登录 失败 [回调方法]
        /// </summary>
        private void EventComeBack_T_OnUserQuit()
        {

        }
        #endregion //登录状态改变 回调函数 结束...




        private void Start()
        {
            instance = this;

            UserDic["height"] = "170";
            UserDic["weight"] = "62";

            //发送验证码 + 登录 相关信息
            JsonDicSend.Add("Content-Type", "application/json");
            JsonDicSend.Add("Accept", "application/json");
            string dataS = Json.Serialize(UserDic);
            post_dataSend = System.Text.UTF8Encoding.UTF8.GetBytes(dataS);

            DetectionToken();
        }

        private void OnEnable()
        {
            EventComeBack_T.OnUserLogoning += EventComeBack_T_OnUserLogoning;
            EventComeBack_T.OnUserQuit += EventComeBack_T_OnUserQuit;
        }
        private void OnDisable()
        {
            EventComeBack_T.OnUserLogoning -= EventComeBack_T_OnUserLogoning;
            EventComeBack_T.OnUserQuit -= EventComeBack_T_OnUserQuit;
        }

        private static DetectionLogoning instance;
        public static DetectionLogoning Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
