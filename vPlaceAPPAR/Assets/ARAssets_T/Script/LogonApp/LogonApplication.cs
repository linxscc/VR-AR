using MiniJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JsonFx.Json;
using Tools_XYRF;
using PlaceAR;

namespace UI_XYRF
{

    public class LogonApplication : MonoBehaviour
    {
        /// <summary>
        /// 登录UI框
        /// </summary>
        public Transform loginView;
        /// <summary>
        /// 手机号输入UI
        /// </summary>
        private InputField nUMnumber;
        /// <summary>
        /// 发送验证码按钮
        /// </summary>
        private Transform codeText;
        /// <summary>
        ///  发送验证码 + 登录 按钮提示字体 颜色变换 灰色 57f,72f,99f
        /// </summary>
        private Color textColor = new Color(0.2235f, 0.2823f, 0.3882f);
        /// <summary>
        /// 发送验证码 + 登录 按钮提示字体 颜色变换 255f
        /// </summary>
        private Color newTextColor = new Color(1, 1, 1, 1);
        /// <summary>
        /// 验证码输入UI
        /// </summary>
        public InputField codeNumber;
        /// <summary>
        /// 登录按钮
        /// </summary>
        private Button moveBtn;
        /// <summary>
        /// 登录文字
        /// </summary>
        private Text loginText;
        /// <summary>
        /// Mask
        /// </summary>
        private Button mask;
        /// <summary>
        /// SiderBarBGShadow
        /// </summary>
        private Image siderBarBGShadow;
        /// <summary>
        /// DownLoadView
        /// </summary>
       // private Image downLoadView;
        /// <summary>
        /// LoginBtnHead
        /// </summary>
        public Image loginBtnHead;
        /// <summary>
        /// UserAccount
        /// </summary>
        public Text userAccount;


        /// <summary>
        /// 用户输入的验证码
        /// </summary>
        [HideInInspector]
        public string userCode;
        /// <summary>
        /// 用户设置
        /// </summary>
        private SetView setView;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
          
            Global.userPhoneNumber = null;
            Init_LogonApplication();
            moveBtn.interactable = false;
            loginText.color = textColor;
            if (Global.userLogonState)  //登录成功
            {
                Sprite spr_ = ResourcesLod_T.ResourcesLoad_SPR("ARTexture", "sidebar-btn-avatar-1@3x");
                loginBtnHead.sprite = spr_;
                userAccount.text = Global.SendTokenInfo.user.mobile;
            }
            else
                userAccount.text = "立即登录";

            setView = transform.GetComponentInChildren<SetView>();
        }

        private void Init_LogonApplication()
        {
            List<Transform> traChids_ = new List<Transform>();
            for (int c = 0; c < transform.childCount; c++)
            {
                transform.GetChild(c).gameObject.SetActive(true);
            }
            traChids_.AddRange(transform.GetComponentsInChildren<Transform>());
            for (int i = 0; i < traChids_.Count; i++)
            {
                switch (traChids_[i].name)
                {
                    case "LoginView":
                        loginView = traChids_[i];
                        break;
                    case "NUMnumber":
                        nUMnumber = traChids_[i].GetComponent<InputField>();
                        break;
                    case "CodeText":
                        codeText = traChids_[i];
                        break;
                    case "CodeNumber":
                        codeNumber = traChids_[i].GetComponent<InputField>();
                        break;
                    case "MoveBtn":
                        moveBtn = traChids_[i].GetComponent<Button>();
                        break;
                    case "LoginText":
                        loginText = traChids_[i].GetComponent<Text>();
                        break;
                    case "Mask":
                        mask = traChids_[i].GetComponent<Button>();
                        break;
                    case "SiderBarBGShadow":
                        siderBarBGShadow = traChids_[i].GetComponent<Image>();
                        break;
                    case "DownLoadView":
                        // downLoadView = traChids_[i].GetComponent<Image>();
                        break;
                    case "LoginBtnHead":
                        loginBtnHead = traChids_[i].GetComponent<Image>();
                        break;
                    case "UserAccount":
                        userAccount = traChids_[i].GetComponent<Text>();
                        break;
                }
            }
            loginView.gameObject.SetActive(false);
            loginView.localScale = Vector3.one;
            // downLoadView.gameObject.SetActive(false);
        }

        /// <summary>
        /// 发送验证码...
        /// </summary>
        public void SendingCode()
        {
            DetectionInputNumber();  //检测用户输入的手机号
            if (!string.IsNullOrEmpty(Global.userPhoneNumber))
            {
                StartCoroutine(DetectionLogoning.Instance.Post_LogonApp(Global.SendmessageAPI + Global.userPhoneNumber, 1));
                codeText.GetChild(0).gameObject.SetActive(false);
                codeText.GetChild(1).gameObject.SetActive(true);
                codeText.GetChild(1).GetComponent<Text>().text = Global.codeMinute.ToString() + "s";
                InvokeRepeating("CodeTime", 1, 1f);   // 验证码自动计时
                codeNumber.text = null;
            }
        }

        private void DetectionInputNumber()
        {
            Global.userPhoneNumber = null;
            if (nUMnumber.text.Length < 11 || string.IsNullOrEmpty(nUMnumber.text))  //错误的手机号
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                    IOSMessage.Create("错误", "请输入正确的手机号", "好的");
                else if (Application.platform == RuntimePlatform.Android) { }
                else
                    Debug.Log("手机号位数异常,请输入正确的手机号");
            }
            else  //正确的手机号输入
            {
                Global.userPhoneNumber = nUMnumber.text;
            }
        }
        public void Changed()
        {
            if (nUMnumber.text.Length >= 1)
            {
                codeText.GetChild(0).GetChild(0).GetComponent<Text>().color = newTextColor;
                codeText.GetChild(0).GetComponent<Button>().interactable = true;
            }
            else
            {
                codeText.GetChild(0).GetChild(0).GetComponent<Text>().color = textColor;
                codeText.GetChild(0).GetComponent<Button>().interactable = false;
            }
        }
        /// <summary>
        /// 输入手机号后 发送短信状态改变
        /// </summary>
        public void ChangeLater()
        {
            //  print("21");
            Global.isSendCode = false;  //未发送验证码
        }

        private void CodeTime()
        {
            int a = int.Parse(codeText.GetChild(1).GetComponent<Text>().text.Split('s')[0]);
            codeText.GetChild(1).GetComponent<Text>().text = (a - 1).ToString() + "s";
            if (a == 0) //如果倒计时结束 
            {
                codeText.GetChild(0).gameObject.SetActive(true);
                codeText.GetChild(1).gameObject.SetActive(false);
                CancelInvoke("CodeTime");
            }
            if (!Global.isSendCode && a >= 55)  //  验证码发送失败
            {
                codeText.GetChild(0).gameObject.SetActive(true);
                codeText.GetChild(1).gameObject.SetActive(false);
                string messing = "服务器未连接...";
                string sendMessing = "验证码发送失败";
                if (Application.internetReachability == NetworkReachability.NotReachable)   //没有网络时
                    messing = "网络已断开...";
                if (Application.platform == RuntimePlatform.IPhonePlayer)  //IPhone平台
                    IOSMessage.Create(sendMessing, messing, "好的");
                else if (Application.platform == RuntimePlatform.Android)  //Android平台
                {
                }
                else
                    Debug.Log(sendMessing + ":" + messing);
                CancelInvoke("CodeTime");
            }
        }

        /// <summary>
        /// 登陆成功后 发送验证码UI处理
        /// </summary>
        public void LogonSucess()
        {
            codeText.GetChild(0).gameObject.SetActive(true);
            codeText.GetChild(1).gameObject.SetActive(false);
            CancelInvoke("CodeTime");
        }

        /// <summary>
        /// 清空输入的手机号
        /// </summary>
        public void ClearInputNumber()
        {
            nUMnumber.text = null;
            codeText.GetChild(0).GetChild(0).GetComponent<Text>().color = textColor;
            codeText.GetChild(0).GetComponent<Button>().interactable = false;
        }

        /// <summary>
        /// 检测验证码输入...
        /// </summary>
        public void DetectionCode()
        {
            userCode = null;
            if (codeNumber.text.Length >= 1 && nUMnumber.text.Length >= 11 && Global.isSendCode)
            {
                loginText.color = newTextColor;
                moveBtn.interactable = true;
            }
            else
            {
                loginText.color = textColor;
                moveBtn.interactable = false;
            }
        }

        /// <summary>
        /// 登录按钮...
        /// </summary>
        public void OnClickLogonApp()
        {
            // 1.检测 + 记录 用户输入的验证码   
            if (GetUserCode())
            {
                // 2. 信息合格 执行登录
                StartCoroutine(DetectionLogoning.Instance.Post_LogonApp(Global.LogonAppAPI
                    + Global.userPhoneNumber + "&vcode=" + userCode + "&vcodeToken=" + Global.SendTokenInfo.token, 2));
            }
            else  // 3.信息不合格 
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                    IOSMessage.Create("错误", "请正确输入", "好的");
                else if (Application.platform == RuntimePlatform.Android) { }
                else
                    Debug.Log("错误,请正确输入");
            }
        }

        private bool GetUserCode()
        {
            userCode = null;
            if (codeNumber.text.Length < 4 || string.IsNullOrEmpty(codeNumber.text) ||
                string.IsNullOrEmpty(Global.userPhoneNumber) || !Global.isSendCode)  //错误的验证码
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                    IOSMessage.Create("错误", "请输入正确的验证码", "好的");
                else if (Application.platform == RuntimePlatform.Android) { }
                else
                    Debug.Log("错误,请输入正确的验证码");
                return false;
            }
            else  //正确的验证码输入
            {
                userCode = codeNumber.text;
                return true;
            }
        }

        /// <summary>
        /// 打开设置
        /// </summary>
        public void OpenSetView()
        {
            string a = "立即登录";
            if (Global.userLogonState)
                a = "退出登录";
            if (setView == null)
                setView = transform.GetComponentInChildren<SetView>();
            setView.LoginBtn.GetChild(0).GetComponent<Text>().text = a;
        }

        /// <summary>
        /// 打开用户协议
        /// </summary>
        public void OpenUserAgreement()
        {
            UserAgreementViewControl.Singleton.Open();
        }
        /// <summary>
        /// 关闭用户协议
        /// </summary>
        public void CloseUserAgreement()
        {
            UserAgreementViewControl.Singleton.Close();
        }

        private static LogonApplication instance;
        public static LogonApplication Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }

    }
}
