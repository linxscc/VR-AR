using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace ModelViewerProject.UI.Label
{
    using System;
    using Label3D;
    using System.Collections.Generic;




    public class Label2DUI : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        public static event EventHandler OnClick;

        Color normalColor = Color.white;
        Color hoverColor = new Color(0f, 1f, 1f);
        Color passedColor = new Color(0f, 0.25f, 0.5f);

        int textSize = 32;


        public Label3DHandler mLabel3D;

        TextMesh textMesh;
        string text;
        /// <summary>
        ///所属子模型
        /// </summary>
        private List<Label3DHandler> labelChild = new List<Label3DHandler>();
        /// <summary>
        /// 实例化出来的子按钮
        /// </summary>
        private List<Label2DUI> label2DUI = new List<Label2DUI>();
        public static Label2DUI SelectedUI;
        private Label2DUI labelPrefab;
        /// <summary>
        /// 是否显示子菜单
        /// </summary>
        public bool isShowChild = false;

        public static void ReleaseAllLabels()
        {
            if (SelectedUI == null)
                return;
            SelectedUI.RefreshChildButton(false);
            SelectedUI.RenderToNormal();
           
           
            SelectedUI = null;
   
        }

        public void OnInit(Label3DHandler label3D, Label2DUI labelPrefab)
        {
            SelectedUI = this;
            this.labelPrefab = labelPrefab;
            mLabel3D = label3D;

            text = label3D.Description;


            textMesh = GetComponentInChildren<TextMesh>();

            RenderToNormal();
            if (labelChild.Count == 0)
                textMesh.text = string.Format("<size={1}>{0}</size>", text, textSize);
            else
                textMesh.text = string.Format("<size={1}>{0}</size>", label3D.Group, textSize);

        }
        public void OnInit(Label3DHandler label3D)
        {
            OnInit(label3D, null);

        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (this == SelectedUI)
                return;
            RenderToHover();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (this == SelectedUI)
                return;
            RenderToNormal();
        }
        /// <summary>
        /// 设置是否显示子菜单
        /// </summary>
        /// <param name="isShow"></param>
        public void RefreshChildButton(bool isShow)
        {
            isShowChild = isShow;
            foreach (Label2DUI child in label2DUI)
            {
                if (isShow)
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (labelChild.Count > 0 && label2DUI.Count == 0)
            {
                foreach (Label3DHandler chilid in labelChild)
                {
                    Label2DUI labelUIchild = labelPrefab.Spawn();
                    labelUIchild.transform.parent = transform.FindChild("ChildMenu");
                    labelUIchild.transform.localPosition = Vector3.zero;
                    labelUIchild.OnInit(chilid);
                    label2DUI.Add(labelUIchild);
                }
            }
           // isShowChild = !isShowChild;
          //  if (!isShowChild)
            //{
                //RefreshChildButton(false);
          //  }
           // if (this == SelectedUI)
              //  return;

            if (OnClick != null)
            {
                LabelUIEventArgs e = new LabelUIEventArgs() { Direction = mLabel3D.LocalPosition, label3D = this.mLabel3D };
                OnClick(this, e);
                if (labelChild.Count > 0)
                {
                    //for (int i = 0; i < labelChild.Count; i++)
                    //{
                    //    Label2DUI labelUI = labelPrefab.Spawn();
                    //    labelUI.transform.parent = transform;
                    //    // labelUI.transform.localPosition = Vector3.zero;
                    //    labelUI.transform.localPosition = new Vector3(0.18f, 0.03f-(i+1)*0.09f, 0); ;
                    //    labelUI.OnInit(labelChild[i], labelPrefab);
                    //    label2DUI.Add(labelUI);
                    //}

                }
                if (e.Used)
                {
                    if (SelectedUI != null)
                    {
                        SelectedUI.RenderToNormal();
                    }
                    RenderToPressed();
                    SelectedUI = this;
                }

            }
        }

        public void RenderToNormal()
        {
            textMesh.color = normalColor;
            // textMesh.text = string.Format ( "<size={1}>{0}</size>", text, titleSize );
        }

        void RenderToHover()
        {

            textMesh.color = hoverColor;
            //textMesh.text = string.Format ( "<size={1}>{0}</size>", text, titleSize );
        }

        void RenderToPressed()
        {
            textMesh.color = passedColor;
            // textMesh.text = string.Format ( "<size={1}>{0}</size>", text, titleSize );
        }
    }
}
