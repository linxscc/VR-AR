using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MoDouAR
{
    /// <summary>
    /// 账号登录
    /// </summary>
    public class LogonApp : Window<LogonApp>
    {
        public delegate void OnOpenSucess();
        public OnOpenSucess onOpenSucess = null;
        public delegate void OnOpenLost(string error);
        public OnOpenLost onOpenLost = null;
        #region  继承方法
        public override int ID
        {
            get
            {
                return 1;
            }
        }

        public override string Name
        {
            get
            {
                return "LogonApp";
            }
        }

        public override string Path
        {
            get
            {
                return "UItcw/LogonApp";
            }
        }
        public override UIType CurrentUIType
        {
            get
            {
                currentUIType.uiFormType = UIFormType.PopUp;
                return base.CurrentUIType;
            }
            set
            {
                base.CurrentUIType = value;
            }
        }
        #endregion //结束...

        /// <summary>
        /// 初始化需要 显示的UI
        /// </summary>
        public Transform[] needShow;
        /// <summary>
        /// 初始化需要 隐藏的UI
        /// </summary>
        public Transform[] needHide;
        /// <summary>
        /// 初始化需要 透明度0.5f的字体
        /// </summary>
        public Transform[] needAlpha;
        /// <summary>
        /// 初始化需要  不可点击的按钮
        /// </summary>
        public Transform[] needInteractable;
        /// <summary>
        /// 初始化需要 缓存输入清空的 InputFiled
        /// </summary>
        public Transform[] needInputFiled;


        /// <summary>
        /// 动画
        /// </summary>
        public AnimationCurve curve;
        /// <summary>
        /// 所有的按钮
        /// </summary>
        public Dictionary<string, Transform> tars = new Dictionary<string, Transform>();
        /// <summary>
        /// UI处理
        /// </summary>
        private UIPro uIPro;
        /// <summary>
        /// 登录相关
        /// </summary>
        private LogonServerCodeFun logonFun;
        public override void Start() { }
        public override void Open()
        {
            base.Open();
            LogonApp_Logon();
        }

        /// <summary>
        /// 打开 登录界面
        /// </summary>
        public void LogonApp_Logon()
        {
            LogonAppData();
            //打开 成功
            onOpenSucess =
                delegate
                {
                    ButtonLogonTop();
                };
            //打开失败
            onOpenLost = OnOpenLosts;
            LogonApp_();
        }
        private void OnOpenLosts(string error)
        {
            Debug.Log(error);
        }
        private void LogonAppData()
        {
            Transform[] bus = transform.GetComponentsInChildren<Transform>();
            for (int i = 0; i < bus.Length; i++)
            {
                if (!tars.ContainsKey(bus[i].name))
                {
                    tars.Add(bus[i].name, bus[i]);
                    switch (bus[i].name)
                    {
                        case "ButtonComeback":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(ButtonComeback);
                            break;
                        case "ButtonLogonTop":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(ButtonLogonTop);
                            break;
                        case "ButtonRegisteredTop":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(ButtonRegisteredTop);
                            break;
                        case "ShowPasswordLogon":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(ShowPasswordLogon);
                            break;
                        case "ButtonGoLogon":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(ButtonGoLogon);
                            break;
                        case "ButtonForgetPassword":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(ButtonForgetPassword);
                            break;
                        case "ButtonUserAgreement":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(ButtonUserAgreement);
                            break;
                        case "GetCodeBack":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(GetCodeBack);
                            break;
                        case "ButtonGoBack":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(ButtonGoBack);
                            break;
                        case "GetCodeRegistered":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(GetCodeRegistered);
                            break;
                        case "ButtonGoRegistered":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(ButtonGoRegistered);
                            break;
                        case "ButtonComebackPassword":
                            tars[bus[i].name].GetComponent<Button>().onClick.AddListener(ButtonComebackPassword);
                            break;
                        default:
                            break;
                    }
                }
                else
                    Debug.Log("已经存在的键值名:    " + bus[i].name);
            }
        }
        /// <summary>
        /// 打开界面
        /// </summary>
        private void LogonApp_()
        {
            if (uIPro == null)
                uIPro = Singleton<UIPro>.Instance;
            if (logonFun == null)
                logonFun = Singleton<LogonServerCodeFun>.Instance;
            transform.localPosition = new Vector3(transform.localPosition.x, -1334f, 0f);
            transform.DOLocalMoveY(0f, 0.2f).SetEase(curve).OnStart(OpenRest);
        }
        /// <summary>
        /// 重置界面数据
        /// </summary>
        private void OpenRest()
        {
            try
            {
                //显示UI
                for (int i = 0; i < needShow.Length; i++)
                {
                    uIPro.UI_Show(needShow[i], true);
                }
                //隐藏UI
                for (int c = 0; c < needHide.Length; c++)
                {
                    uIPro.UI_Show(needHide[c], false);
                }
                //改变字体透明度
                for (int q = 0; q < needAlpha.Length; q++)
                {
                    uIPro.UI_TextAlpha(needAlpha[q], 0.5f);
                }
                //需要不可点击的 Button
                for (int z = 0; z < needInteractable.Length; z++)
                {
                    uIPro.UI_ButtonInteractable(needInteractable[z], false);
                }
                //清理InputFiled缓存
                for (int w = 0; w < needInputFiled.Length; w++)
                {
                    uIPro.UI_InputField(needInputFiled[w]);
                }
                tars["ButtonGoLogon"].GetComponent<Text>().color = new Color(1f, 1f, 1f);
                tars["ButtonGoBack"].GetComponent<Text>().color = new Color(1, 1, 1f);
                tars["ButtonGoRegistered"].GetComponent<Text>().color = new Color(1, 1, 1f);

                onOpenSucess();
            }
            catch (Exception e)
            {
                onOpenLost(e.Message);
            }
        }


        #region 登录
        /// <summary>
        /// 用户协议
        /// </summary>
        private void ButtonUserAgreement()
        {
            UserAgreementWindow.Instance.CreatWindow();
            UserAgreementWindow.Instance.Open();
        }
        /// <summary>
        /// 忘记密码
        /// </summary>
        private void ButtonForgetPassword()
        {
            uIPro.UI_Show(tars["ButtonComeback"], false);
            uIPro.UI_Show(tars["ContentGetBackPassword"], true);
            tars["ContentGetBackPassword"].localPosition = new Vector3(tars["ContentGetBackPassword"].localPosition.x, -Screen.height, 0);
            tars["ContentGetBackPassword"].DOLocalMoveY(667, 0.3f).SetEase(curve);

            uIPro.UI_Show(tars["Image0ButtonGoBack"], true);
            uIPro.UI_Show(tars["Image1ButtonGoBack"], false);
            tars["ButtonGoBack"].GetComponent<Text>().color = new Color(1, 1, 1f);
            uIPro.UI_TextModify(tars["ButtonGoBack"], "下一步");
            uIPro.UI_ButtonInteractable(tars["ButtonGoBack"], false);
            tars["InputNumberBack"].GetComponent<InputField>().text = "";
            tars["InputCodeBack"].GetComponent<InputField>().text = "";
            tars["InputPasswordBack"].GetComponent<InputField>().text = "";
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        private void ButtonGoLogon()
        {
            InputField inpNumber = tars["InputNumberLogon"].GetComponent<InputField>();
            InputField inpPassword = tars["InputPasswordLogon"].GetComponent<InputField>();
            logonFun.UserLogon(LogonCacheData.logonAPI, inpNumber.text, inpPassword.text,
                delegate
                {
                    //登录成功
                    LogonCacheData.logonState = true;
                    PanelSetupEntrance.instance.DecitionToken();
                    ButtonComeback();
                }, LogonError);
        }
        /// <summary>
        /// 登录账号失败
        /// </summary>
        /// <param name="error">错误信息</param>
        private void LogonError(string error)
        {
            //验证码+Token  登录失败
            if (!tars["PasswordErrorLogon"].gameObject.activeInHierarchy)
            {
                uIPro.UI_TextModify(tars["TextPasswordErrorLogon"], "密码错误");
                uIPro.UI_ImageAlpha(tars["PasswordErrorLogon"], 0);
                uIPro.UI_TextAlpha(tars["TextPasswordErrorLogon"], 0);
                uIPro.UI_Show(tars["PasswordErrorLogon"], true);
                tars["PasswordErrorLogon"].GetComponent<Image>().DOColor(new Color(255, 255, 255, 0.196f), 0.5f);
                tars["TextPasswordErrorLogon"].GetComponent<Text>().DOColor(new Color(255, 255, 255, 1), 0.5f);
                Invoke("PromptPasswordError", 1.5f);
            }
            LogonCacheData.logonState = false;
        }
        /// <summary>
        /// 密码错误 提示框[登录]
        /// </summary>
        private void PromptPasswordError()
        {
            tars["PasswordErrorLogon"].GetComponent<Image>().DOColor(new Color(255, 255, 255, 0), 0.5f);
            tars["TextPasswordErrorLogon"].GetComponent<Text>().DOColor(new Color(255, 255, 255, 0), 0.5f).OnKill(
                delegate
                {
                    uIPro.UI_Show(tars["PasswordErrorLogon"], false);
                });
        }
        /// <summary>
        /// 登录账号 ---显示密码
        /// </summary>
        private void ShowPasswordLogon()
        {
            InputField inpPassword = tars["InputPasswordLogon"].GetComponent<InputField>();
            switch (inpPassword.contentType)
            {
                case InputField.ContentType.Alphanumeric:
                    inpPassword.contentType = InputField.ContentType.Password;
                    break;
                case InputField.ContentType.Password:
                    inpPassword.contentType = InputField.ContentType.Alphanumeric;
                    break;
            }
            inpPassword.enabled = false;
            inpPassword.enabled = true;
        }

        /// <summary>
        /// Top登录 
        /// </summary>
        private void ButtonLogonTop()
        {
            uIPro.UI_TextAlpha(tars["ButtonLogonTop"], 1f);
            uIPro.UI_Show(tars["ImageButtonLogonTop"], true);
            uIPro.UI_TextAlpha(tars["ButtonRegisteredTop"], 0.5f);
            uIPro.UI_Show(tars["ImageButtonRegisteredTop"], false);

            uIPro.UI_Show(tars["ContentLogon"], true);
            uIPro.UI_Show(tars["ContentGetBackPassword"], false);
            uIPro.UI_Show(tars["ContentRegistered"], false);
            uIPro.UI_Show(tars["Bottom"], true);
            uIPro.UI_TextModify(tars["ButtonUserAgreement"], "登录即表示您同意《墨斗AR用户协议》");
        }

        /// <summary>
        /// 手机号+密码 输入检测
        /// </summary>
        public void PhonePasswordLogon()
        {
            InputField inpNumber = tars["InputNumberLogon"].GetComponent<InputField>();
            InputField inpPassword = tars["InputPasswordLogon"].GetComponent<InputField>();
            if (inpPassword.text.Length >= 8)
            {
                uIPro.UI_ButtonInteractable(tars["ShowPasswordLogon"], true);
            }
            else
                uIPro.UI_ButtonInteractable(tars["ShowPasswordLogon"], false);
            if (inpNumber.text.Length >= 1 && inpPassword.text.Length >= 8)
            {
                uIPro.UI_ButtonInteractable(tars["ButtonGoLogon"], true);
                tars["ButtonGoLogon"].GetComponent<Text>().color = new Color(0.2392f, 0.8627f, 1f);  //#3DDCFF 字体颜色
                uIPro.UI_Show(tars["ImageBlack0Logon"], false);
                uIPro.UI_Show(tars["ImageBlack1Logon"], true);
            }
            else
            {
                uIPro.UI_ButtonInteractable(tars["ButtonGoLogon"], false);
                tars["ButtonGoLogon"].GetComponent<Text>().color = new Color(1f, 1f, 1f);
                uIPro.UI_Show(tars["ImageBlack0Logon"], true);
                uIPro.UI_Show(tars["ImageBlack1Logon"], false);
            }
        }
        #endregion  //......


        #region 注册
        /// <summary>
        /// 下一步 ---注册
        /// </summary>
        private void ButtonGoRegistered()
        {
            InputField inpNumber = tars["InputNumberRegistered"].GetComponent<InputField>();
            //注册
            if (tars["PhonePasswordRegistered"].gameObject.activeInHierarchy)
            {
                InputField inpPassword = tars["InputPasswordRegistered"].GetComponent<InputField>();
                logonFun.UserRestPassword(LogonCacheData.restPasswordAPI, inpPassword.text,
                    delegate
                    {
                        //注册成功
                        LogonCacheData.logonState = true;
                        PanelSetupEntrance.instance.DecitionToken();
                        ButtonComeback();
                    }, RegisterError);
            }
            else   //下一步
            {
                InputField inpCode = tars["InputCodeRegistered"].GetComponent<InputField>();
                logonFun.UserDetectionCode(LogonCacheData.detectionCodeAPI, inpNumber.text, inpCode.text,
                  delegate
                  {
                      //验证码+codeToken  登录成功
                      uIPro.UI_Show(tars["PhoneNumberRegistered"], false);
                      uIPro.UI_Show(tars["PhonePasswordRegistered"], true);
                      uIPro.UI_Show(tars["PhoneCodeBackRegistered"], false);
                      tars["ButtonGoRegistered"].GetComponent<Text>().color = new Color(1, 1, 1f);
                      uIPro.UI_TextModify(tars["ButtonGoRegistered"], "注册");
                      uIPro.UI_Show(tars["Image0ButtonGoRegistered"], true);
                      uIPro.UI_Show(tars["Image1ButtonGoRegistered"], false);
                      tars["ButtonRegistered"].localPosition = new Vector3(tars["ButtonRegistered"].localPosition.x, -392, 0);
                      tars["ButtonRegistered"].DOLocalMoveY(-180f, 0.3f).SetEase(curve);
                  },
                  delegate
                  {
                      //验证码+codeToken  登录失败
                      PromptOpen("验证码错误");
                  });
            }
        }
        /// <summary>
        /// 注册失败
        /// </summary>
        /// <param name="error">错误信息</param>
        private void RegisterError(string error)
        {
            Debug.Log(error);
            if (error == "数据库中已存在该记录")
            {
                PromptOpen("账号已存在");
            }
        }
        /// <summary>
        /// 输入错误 提示框[注册]
        /// </summary>
        /// <param name="str">提示语</param> 
        private void PromptOpen(string str)
        {
            if (!tars["PasswordErrorButtonGoRegistered"].gameObject.activeInHierarchy)
            {
                uIPro.UI_TextModify(tars["Text2ButtonGoRegistered"], str);
                uIPro.UI_ImageAlpha(tars["PasswordErrorButtonGoRegistered"], 0);
                uIPro.UI_TextAlpha(tars["Text2ButtonGoRegistered"], 0);
                uIPro.UI_Show(tars["PasswordErrorButtonGoRegistered"], true);
                tars["PasswordErrorButtonGoRegistered"].GetComponent<Image>().DOColor(new Color(255, 255, 255, 0.196f), 0.5f);
                tars["Text2ButtonGoRegistered"].GetComponent<Text>().DOColor(new Color(255, 255, 255, 1), 0.5f);
                Invoke("PromptClose", 1.5f);
            }
        }
        private void PromptClose()
        {
            tars["PasswordErrorButtonGoRegistered"].GetComponent<Image>().DOColor(new Color(255, 255, 255, 0), 0.5f);
            tars["Text2ButtonGoRegistered"].GetComponent<Text>().DOColor(new Color(255, 255, 255, 0), 0.5f).OnKill(
                delegate
                {
                    uIPro.UI_Show(tars["PasswordErrorButtonGoRegistered"], false);
                });
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        private void GetCodeRegistered()
        {
            InputField inpNumber = tars["InputNumberRegistered"].GetComponent<InputField>();
            logonFun.UserSendConde(LogonCacheData.sendCodeAPI, inpNumber.text,
                delegate
                {
                    InvokeRepeating("CodeTimeRegistered", 0f, 1f);
                }, GetCodeErrorRegistered);
        }
        private void GetCodeErrorRegistered(string error)
        {
            Debug.Log(error);
        }
        private void CodeTimeRegistered()
        {
            if (tars["GetCodeRegistered"].GetComponent<Button>().interactable)
            {
                uIPro.UI_ButtonInteractable(tars["GetCodeRegistered"], false);
                uIPro.UI_TextModify(tars["GetCodeRegistered"], "59s");
            }
            else
            {
                string[] arr_ = tars["GetCodeRegistered"].GetComponent<Text>().text.Split('s');
                int dex = int.Parse(arr_[0]);
                if (dex > 1)
                {
                    dex -= 1;
                    uIPro.UI_TextModify(tars["GetCodeRegistered"], dex.ToString() + "s");
                }
                else
                {
                    uIPro.UI_TextModify(tars["GetCodeRegistered"], "获取验证码");
                    uIPro.UI_ButtonInteractable(tars["GetCodeRegistered"], true);
                    CancelInvoke("CodeTimeRegistered");
                }
            }
        }
        /// <summary>
        /// Top注册
        /// </summary>
        private void ButtonRegisteredTop()
        {
            uIPro.UI_Show(tars["PhoneNumberRegistered"], true);
            uIPro.UI_Show(tars["PhonePasswordRegistered"], false);
            uIPro.UI_Show(tars["PhoneCodeBackRegistered"], true);
            tars["ButtonRegistered"].localPosition = new Vector3(tars["ButtonRegistered"].localPosition.x, -392f, 0);
            tars["ButtonGoRegistered"].GetComponent<Text>().color = new Color(1, 1, 1f);
            uIPro.UI_TextModify(tars["ButtonGoBack"], "下一步");
            uIPro.UI_Show(tars["Image0ButtonGoRegistered"], true);
            uIPro.UI_Show(tars["Image1ButtonGoRegistered"], false);
            tars["InputNumberRegistered"].GetComponent<InputField>().text = "";
            tars["InputCodeRegistered"].GetComponent<InputField>().text = "";
            tars["InputPasswordBack"].GetComponent<InputField>().text = "";
            uIPro.UI_ButtonInteractable(tars["GetCodeRegistered"], false);
            uIPro.UI_ButtonInteractable(tars["ButtonGoRegistered"], false);

            uIPro.UI_TextAlpha(tars["ButtonLogonTop"], 0.5f);
            uIPro.UI_Show(tars["ImageButtonLogonTop"], false);
            uIPro.UI_TextAlpha(tars["ButtonRegisteredTop"], 1f);
            uIPro.UI_Show(tars["ImageButtonRegisteredTop"], true);
            uIPro.UI_Show(tars["ContentLogon"], false);
            uIPro.UI_Show(tars["ContentGetBackPassword"], false);
            uIPro.UI_Show(tars["ContentRegistered"], true);
            uIPro.UI_Show(tars["Bottom"], true);
            uIPro.UI_TextModify(tars["ButtonUserAgreement"], "注册即表示您同意《墨斗AR用户协议》");
        }

        /// <summary>
        /// 手机号 输入检测[注册账号]
        /// </summary>
        public void PhoneNumberRegistered()
        {
            InputField inpNumber = tars["InputNumberRegistered"].GetComponent<InputField>();
            if (inpNumber.text.Length >= 1)
                uIPro.UI_ButtonInteractable(tars["GetCodeRegistered"], true);
            else
                uIPro.UI_ButtonInteractable(tars["GetCodeRegistered"], false);
        }
        /// <summary>
        /// 验证码 输入检测[注册账号]
        /// </summary>
        public void PhoneCodeRegistered()
        {
            InputField inpNumber = tars["InputNumberRegistered"].GetComponent<InputField>();
            InputField inpCode = tars["InputCodeRegistered"].GetComponent<InputField>();
            if (inpCode.text.Length >= 1 && inpNumber.text.Length >= 1)
            {
                uIPro.UI_ButtonInteractable(tars["ButtonGoRegistered"], true);
                tars["ButtonGoRegistered"].GetComponent<Text>().color = new Color(0.2392f, 0.8627f, 1f);  //#3DDCFF 字体颜色
                uIPro.UI_Show(tars["Image0ButtonGoRegistered"], false);
                uIPro.UI_Show(tars["Image1ButtonGoRegistered"], true);
            }
            else
            {
                uIPro.UI_ButtonInteractable(tars["ButtonGoRegistered"], false);
                tars["ButtonGoRegistered"].GetComponent<Text>().color = new Color(1, 1, 1f);
                uIPro.UI_Show(tars["Image0ButtonGoRegistered"], true);
                uIPro.UI_Show(tars["Image1ButtonGoRegistered"], false);
            }
        }
        /// <summary>
        /// 密码 输入检测[注册账号]
        /// </summary>
        public void PhonePasswordRegistered()
        {
            InputField inpPassword = tars["InputPasswordRegistered"].GetComponent<InputField>();
            if (inpPassword.text.Length >= 8)
            {
                uIPro.UI_ButtonInteractable(tars["ButtonGoRegistered"], true);
                tars["ButtonGoRegistered"].GetComponent<Text>().color = new Color(0.2392f, 0.8627f, 1f);  //#3DDCFF 字体颜色
                uIPro.UI_Show(tars["Image0ButtonGoRegistered"], false);
                uIPro.UI_Show(tars["Image1ButtonGoRegistered"], true);
            }
            else
            {
                uIPro.UI_ButtonInteractable(tars["ButtonGoRegistered"], false);
                tars["ButtonGoRegistered"].GetComponent<Text>().color = new Color(1, 1, 1f);
                uIPro.UI_Show(tars["Image0ButtonGoRegistered"], true);
                uIPro.UI_Show(tars["Image1ButtonGoRegistered"], false);
            }
        }
        #endregion  //......


        #region 找回密码
        /// <summary>
        /// 下一步--重置密码
        /// </summary>
        private void ButtonGoBack()
        {
            //重置密码
            if (tars["PhonePasswordBack"].gameObject.activeInHierarchy)
            {
                InputField inpNumber = tars["InputNumberBack"].GetComponent<InputField>();
                InputField inpPassword = tars["InputPasswordBack"].GetComponent<InputField>();
                logonFun.UserRestPassword(LogonCacheData.restPasswordAPI, inpPassword.text, delegate
 {
     //重置成功 →  开始通过 账号+密码登录
     logonFun.UserLogon(LogonCacheData.logonAPI, inpNumber.text, inpPassword.text, delegate
{
    //登录成功
    PanelSetupEntrance.instance.DecitionToken();
    uIPro.UI_Show(tars["ButtonComeback"], true);
    tars["ContentGetBackPassword"].DOLocalMoveY(-Screen.height / 2, 0.25f).SetEase(curve).OnKill(delegate
        {
            uIPro.UI_Show(tars["ContentGetBackPassword"], false);
        });
    LogonCacheData.logonState = true;
    ButtonComeback();
}, RestPasswordError);
 }, RestPasswordError);
            }
            else   //下一步
            {
                InputField inpNumber = tars["InputNumberBack"].GetComponent<InputField>();
                InputField inpCode = tars["InputCodeBack"].GetComponent<InputField>();
                logonFun.UserDetectionCode(LogonCacheData.detectionCodeAPI, inpNumber.text, inpCode.text,
                  delegate
                  {
                      //验证码 验证通过
                      uIPro.UI_Show(tars["PhoneNumberBack"], false);
                      uIPro.UI_Show(tars["PhonePasswordBack"], true);
                      uIPro.UI_Show(tars["PhoneCodeBack"], false);
                      tars["ButtonGoBack"].GetComponent<Text>().color = new Color(1, 1, 1f);
                      uIPro.UI_TextModify(tars["ButtonGoBack"], "登录");
                      uIPro.UI_Show(tars["Image0ButtonGoBack"], true);
                      uIPro.UI_Show(tars["Image1ButtonGoBack"], false);
                      tars["ButtonBack"].localPosition = new Vector3(tars["ButtonBack"].localPosition.x, -392, 0);
                      tars["ButtonBack"].DOLocalMoveY(-180f, 0.3f).SetEase(curve);
                  },
                  delegate
                  {
                      //验证码 验证失败
                      if (!tars["PasswordErrorButtonGoBack"].gameObject.activeInHierarchy)
                      {
                          uIPro.UI_TextModify(tars["Text2ButtonGoBack"], "验证码错误");
                          uIPro.UI_ImageAlpha(tars["PasswordErrorButtonGoBack"], 0);
                          uIPro.UI_TextAlpha(tars["Text2ButtonGoBack"], 0);
                          uIPro.UI_Show(tars["PasswordErrorButtonGoBack"], true);
                          tars["PasswordErrorButtonGoBack"].GetComponent<Image>().DOColor(new Color(255, 255, 255, 0.196f), 0.5f);
                          tars["Text2ButtonGoBack"].GetComponent<Text>().DOColor(new Color(255, 255, 255, 1), 0.5f);
                          Invoke("PromptCloseBack", 1.5f);
                      }
                  });
            }
        }
        /// <summary>
        /// 重置密码 失败
        /// </summary>
        /// <param name="error">错误信息</param>
        private void RestPasswordError(string error)
        {
            Debug.Log(error);
        }

        /// <summary>
        /// 输入错误 提示框[找回密码]
        /// </summary>
        private void PromptCloseBack()
        {
            tars["PasswordErrorButtonGoBack"].GetComponent<Image>().DOColor(new Color(255, 255, 255, 0), 0.5f);
            tars["Text2ButtonGoBack"].GetComponent<Text>().DOColor(new Color(255, 255, 255, 0), 0.5f).OnKill(
                delegate
                {
                    uIPro.UI_Show(tars["PasswordErrorButtonGoBack"], false);
                });
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        private void GetCodeBack()
        {
            InputField inpNumber = tars["InputNumberBack"].GetComponent<InputField>();
            logonFun.UserSendConde(LogonCacheData.sendCodeAPI, inpNumber.text,
                delegate
                {
                    InvokeRepeating("CodeTimeGetBack", 0f, 1f);
                }, GetCodeErrorBack);
        }
        /// <summary>
        /// 验证码发送失败 [找回密码]
        /// </summary>
        /// <param name="error">错误信息</param>
        private void GetCodeErrorBack(string error)
        {
            Debug.Log(error);
        }
        private void CodeTimeGetBack()
        {
            if (tars["GetCodeBack"].GetComponent<Button>().interactable)
            {
                uIPro.UI_ButtonInteractable(tars["GetCodeBack"], false);
                uIPro.UI_TextModify(tars["GetCodeBack"], "59s");
            }
            else
            {
                string[] arr_ = tars["GetCodeBack"].GetComponent<Text>().text.Split('s');
                int dex = int.Parse(arr_[0]);
                if (dex > 1)
                {
                    dex -= 1;
                    uIPro.UI_TextModify(tars["GetCodeBack"], dex.ToString() + "s");
                }
                else
                {
                    uIPro.UI_TextModify(tars["GetCodeBack"], "获取验证码");
                    uIPro.UI_ButtonInteractable(tars["GetCodeBack"], true);
                    CancelInvoke("CodeTimeGetBack");
                }
            }
        }
        /// <summary>
        /// 返回上一级 
        /// </summary>
        private void ButtonComebackPassword()
        {
            //返回 登录界面
            if (!tars["PhonePasswordBack"].gameObject.activeInHierarchy)
            {
                uIPro.UI_Show(tars["ButtonComeback"], true);
                tars["ContentGetBackPassword"].DOLocalMoveY(-Screen.height / 2, 0.25f).SetEase(curve).OnKill(
                    delegate
                    {
                        uIPro.UI_Show(tars["ContentGetBackPassword"], false);
                    });
            }
            else  //返回 找回密码界面
            {
                tars["ButtonBack"].localPosition = new Vector3(tars["ButtonBack"].localPosition.x, -180f, 0);
                tars["ButtonBack"].DOLocalMoveY(-392f, 0.3f).SetEase(curve).OnKill(
                    delegate
                    {
                        uIPro.UI_Show(tars["PhoneNumberBack"], true);
                        uIPro.UI_Show(tars["PhonePasswordBack"], false);
                        uIPro.UI_Show(tars["PhoneCodeBack"], true);
                        uIPro.UI_TextModify(tars["ButtonGoBack"], "下一步");
                    });
            }
        }

        /// <summary>
        ///  手机号 输入检测[找回密码]
        /// </summary>
        public void PhoneNumberBack()
        {
            InputField inpNumber = tars["InputNumberBack"].GetComponent<InputField>();
            if (inpNumber.text.Length >= 1)
                uIPro.UI_ButtonInteractable(tars["GetCodeBack"], true);
            else
                uIPro.UI_ButtonInteractable(tars["GetCodeBack"], false);
        }
        /// <summary>
        ///   验证码 输入检测[找回密码]
        /// </summary>
        public void PhoneCodeBack()
        {
            InputField inpCode = tars["InputCodeBack"].GetComponent<InputField>();
            if (inpCode.text.Length >= 1)
            {
                uIPro.UI_ButtonInteractable(tars["ButtonGoBack"], true);
                tars["ButtonGoBack"].GetComponent<Text>().color = new Color(0.2392f, 0.8627f, 1f);  //#3DDCFF 字体颜色
                uIPro.UI_Show(tars["Image0ButtonGoBack"], false);
                uIPro.UI_Show(tars["Image1ButtonGoBack"], true);
            }
            else
            {
                uIPro.UI_ButtonInteractable(tars["ButtonGoBack"], false);
                tars["ButtonGoBack"].GetComponent<Text>().color = new Color(1, 1, 1f);
                uIPro.UI_Show(tars["Image0ButtonGoBack"], true);
                uIPro.UI_Show(tars["Image1ButtonGoBack"], false);
            }
        }
        /// <summary>
        /// 密码 输入检测[找回密码]
        /// </summary>
        public void PhonePasswordBack()
        {
            InputField inpPassword = tars["InputPasswordBack"].GetComponent<InputField>();
            if (inpPassword.text.Length >= 8)
            {
                uIPro.UI_ButtonInteractable(tars["ButtonGoBack"], true);
                tars["ButtonGoBack"].GetComponent<Text>().color = new Color(0.2392f, 0.8627f, 1f);  //#3DDCFF 字体颜色
                uIPro.UI_Show(tars["Image0ButtonGoBack"], false);
                uIPro.UI_Show(tars["Image1ButtonGoBack"], true);
            }
            else
            {
                uIPro.UI_ButtonInteractable(tars["ButtonGoBack"], false);
                tars["ButtonGoBack"].GetComponent<Text>().color = new Color(1, 1, 1f);
                uIPro.UI_Show(tars["Image0ButtonGoBack"], true);
                uIPro.UI_Show(tars["Image1ButtonGoBack"], false);
            }
        }
        #endregion  //......


        /// <summary>
        /// 返回上一级
        /// </summary>
        private void ButtonComeback()
        {
            transform.DOLocalMoveY(-1334f, 0.15f).OnKill(CloseLogonPanel);
        }
        private void CloseLogonPanel()
        {
            Close();
        }
    }
}
