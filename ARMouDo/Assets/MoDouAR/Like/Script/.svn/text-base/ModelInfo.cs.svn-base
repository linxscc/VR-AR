/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;

namespace MoDouAR
{
    /// <summary>
    /// 显示下载界面模型信息
    /// </summary>
    public class ModelInfo : Window<ModelInfo>
    {
        public override int ID
        {
            get
            {
                return 3;
            }
        }

        public override string Name
        {
            get
            {
                return "ModelInfo";
            }
        }

        public override string Path
        {
            get
            {
                return "UI/ModelInfo";
            }
        }
        public override UIType CurrentUIType
        {
            get
            {
                currentUIType.uiFormShowMode = UIFormShowMode.ReverseChange;
                currentUIType.uiFormType = UIFormType.PopUp;
                currentUIType.uiFormLucencyType = UIFormLucencyType.ImPenetrable;
                return base.CurrentUIType;
            }

            set
            {
                base.CurrentUIType = value;
            }
        }
        private Transform backGround;
        private Transform BackGroung
        {
            get
            {
                if (backGround == null)
                    backGround = WndObject.transform.Find("BackGround");
                return backGround;
            }
        }
        private Transform close;
        private Transform Closebtn
        {
            get
            {
                if (close == null)
                    close = WndObject.transform.Find("Close");
                return close;
            }
        }
        public Text prefabName;
        public Text info;
        public GameObject offcial;
        public Button downButton;
        public Button downLoding;
        public Image progress;
        public Image image;
        public Button closeB;
        private bool isLoad = false;
        private CallBack down;
        private CallBack stop;
        private ChildItemData data;
        private LoadState State
        {
            get { return data.state; }
            set
            {
                data.state = value;
                switch (value)
                {
                    case LoadState.down:
                        downButton.gameObject.SetActive(true);
                        downLoding.gameObject.SetActive(false);
                        break;
                    case LoadState.download:
                        downButton.gameObject.SetActive(false);
                        downLoding.gameObject.SetActive(true);
                        progress.fillAmount = 0;
                        break;
                    case LoadState.downOver:
                        downButton.gameObject.SetActive(false);
                        downLoding.gameObject.SetActive(false);
                        progress.fillAmount = 0;
                        break;
                    default:
                        break;
                }

            }
        }
        public override void Awake()
        {
            base.Awake();
            downButton.onClick.AddListener(Down);
            downLoding.onClick.AddListener(Stop);
            closeB.onClick.AddListener(Close);
            BackGroung.localPosition = new Vector3(BackGroung.localPosition.x, -1310, BackGroung.localPosition.z);
        }
        public void Open(ChildItemData data, CallBack down = null, CallBack stop = null)
        {
            //print(UIManager.GetInstance().stackCurrentUIForms.Count);
            if (UIManager.GetInstance().stackCurrentUIForms.Count == 0)//根据栈中是否有窗体设置返回键角度
                closeB.transform.eulerAngles = new Vector3(0, 0, 90);
            else
                closeB.transform.eulerAngles = Vector3.zero;
            isLoad = false;
            base.Open();
            this.down = down;
            this.stop = stop;
            this.data = data;
            switch (data.state)
            {
                case LoadState.down:
                    downButton.gameObject.SetActive(true);
                    downLoding.gameObject.SetActive(false);
                    break;
                case LoadState.download:
                    downButton.gameObject.SetActive(false);
                    downLoding.gameObject.SetActive(true);
                    StartCoroutine(LoadProgress(data.www));
                    break;
                case LoadState.downOver:
                    downButton.gameObject.SetActive(false);
                    downLoding.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
            prefabName.text = data.item.name;
            info.text = data.item.info;
            // Sprite sprite = Sprite.Create(data.texture, new Rect(0, 0, data.texture.width, data.texture.height), Vector2.zero);
            image.sprite = data.sprite;
            Closebtn.gameObject.SetActive(true);
            Tweener tw = BackGroung.DOLocalMoveY(-60, 0.3f);
        }
        private void Stop()
        {
            State = LoadState.down;
            if (stop != null)
                stop();
        }
        private void Down()
        {
            State = LoadState.download;

            if (down != null)
                down();
            StartCoroutine(LoadProgress(data.www));
        }
        private IEnumerator LoadProgress(WWW www)
        {

            while (!isLoad)
            {
                //print(www.progress);
                progress.fillAmount = www.progress;
                if (www.progress == 1)
                {
                    State = LoadState.downOver;
                    isLoad = true;
                }

                yield return new WaitForFixedUpdate();
            }
        }
        public override void Close()
        {
            // base
            Closebtn.gameObject.SetActive(false);
            Tweener tw = BackGroung.DOLocalMoveY(-1270, 0.3f);
            // tw.SetEase(Ease.InQuad);
            tw.OnComplete(Complete);
        }
        /// <summary>
        /// 关闭动画回调
        /// </summary>
        private void Complete()
        {
            base.Close();
            //UIMaskManager.GetInstance().CancelMaskWindow();
        }
    }
}