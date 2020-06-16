using UnityEngine;
using System.Collections;

namespace ModelViewerProject.UI
{
    using Model;
    using Stage;
    using Label3D;
    using Label;
    using Animate;
    using System.Collections.Generic;

    public class UIController : MonoBehaviour
    {

        public ToggleButton DimensionalToggle;
        public ToggleButton AssembleToggle;

        public ZoomInOutUIGroup ZoomGroup;

        public ModelRotateUIGroup RotateGroup;

        public ModelController Model;

        public StageController Stage;

        public Label2DUIController LabelUIs;

        void Start()
        {

            // Model.OnInit ( );
            //LabelUIs.OnInit ( Model.Label3Ds.labels );
        }

        void OnEnable()
        {

            DimensionalToggle.OnToggle += AllToggle_OnToggle;
            AssembleToggle.OnToggle += AllToggle_OnToggle;

            ZoomGroup.BeginZoomIn += ZoomGroup_BeginZoomIn;
            ZoomGroup.BeginZoomOut += ZoomGroup_BeginZoomOut;
            ZoomGroup.EndZoomIn += ZoomGroup_EndZoomIn;
            ZoomGroup.EndZoomOut += ZoomGroup_EndZoomOut;

            RotateGroup.OnBackHome += RotateGroup_OnBackHome;
            RotateGroup.OnRotate += RotateGroup_OnRotate;
            RotateGroup.OnStatic += RotateGroup_OnStatic;

            ScaleHandler.OnOverflowMax += ScaleHandler_OnOverflowMax;
            ScaleHandler.OnOverflowMin += ScaleHandler_OnOverflowMin;

            Label2DUI.OnClick += Label2DUI_OnClick;

            Model.OnInitModel += Model_OnInitModel;
        }

        private void Model_OnInitModel(object sender, System.EventArgs e)
        {
            LabelUIs.OnInit(Model.Label3Ds.labels);
            AssembleToggle.OnEnableOther();
            ZoomGroup.EnableAll();
            RotateGroup.HomeButton.interactable = false;
        }

        private void Label2DUI_OnClick(object sender, System.EventArgs e)
        {

            // print("enter");
            LabelUIEventArgs lue = e as LabelUIEventArgs;
            if ((sender as Label2DUI).labelChild.Count == 0)
                Model.HideOthersButOne(lue.label3D,null);
            else
            {
                Model.HideOthersBut(lue.label3D, sender as Label2DUI);
                // LabelUIs.group[(sender as Label2DUI).mLabel3D.Group].RefreshChildButton(true);
                foreach (KeyValuePair<string, Label2DUI> kvp in LabelUIs.group)
                {
                    if (kvp.Key == (sender as Label2DUI).mLabel3D.Group)
                    {
                        if (!(sender as Label2DUI).isShowChild)
                            kvp.Value.RefreshChildButton(true);
                        else
                        {
                            kvp.Value.RefreshChildButton(false);
                            kvp.Value.RenderToNormal();
                        }
                    }
                    else
                    {
                        kvp.Value.RefreshChildButton(false);
                        kvp.Value.RenderToNormal();
                    }
                       

                }

            }
            RotateGroup.SetHomeToInteractable();

            lue.Used = true;
        }

        private void ScaleHandler_OnOverflowMin(object sender, System.EventArgs e)
        {
            //Debug.Log ( "Scale Overflow Min!" );
            //print(11);
            ZoomGroup.DisableZoomOut();
        }

        private void ScaleHandler_OnOverflowMax(object sender, System.EventArgs e)
        {
            //Debug.Log ( "Scale Overflow Max!" );
            ZoomGroup.DisableZoomIn();
        }

        void OnDisable()
        {

            DimensionalToggle.OnToggle -= AllToggle_OnToggle;
            AssembleToggle.OnToggle -= AllToggle_OnToggle;

            ZoomGroup.BeginZoomIn -= ZoomGroup_BeginZoomIn;
            ZoomGroup.BeginZoomOut -= ZoomGroup_BeginZoomOut;
            ZoomGroup.EndZoomIn -= ZoomGroup_EndZoomIn;
            ZoomGroup.EndZoomOut -= ZoomGroup_EndZoomOut;

            RotateGroup.OnBackHome -= RotateGroup_OnBackHome;
            RotateGroup.OnRotate -= RotateGroup_OnRotate;
            RotateGroup.OnStatic -= RotateGroup_OnStatic;


            ScaleHandler.OnOverflowMax -= ScaleHandler_OnOverflowMax;
            ScaleHandler.OnOverflowMin -= ScaleHandler_OnOverflowMin;

            Label2DUI.OnClick -= Label2DUI_OnClick;

            Model.OnInitModel -= Model_OnInitModel;
        }


        private void RotateGroup_OnStatic(object sender, System.EventArgs e)
        {
            Model.StopRotate();
        }

        private void RotateGroup_OnRotate(object sender, System.EventArgs e)
        {
            RotateEventArgs rea = e as RotateEventArgs;

            if (rea != null)
            {

                rea.Used = true;

                Model.StartRotate(rea.Direction);

                Label2DUI.ReleaseAllLabels();
            }
        }

        private void RotateGroup_OnBackHome(object sender, System.EventArgs e)
        {
            Model.BackHome();
            ZoomGroup.EnableAll();

            Label2DUI.ReleaseAllLabels();
        }

        private void ZoomGroup_EndZoomOut(object sender, System.EventArgs e)
        {
            Model.StopScale();
        }

        private void ZoomGroup_EndZoomIn(object sender, System.EventArgs e)
        {
            Model.StopScale();
        }

        private void ZoomGroup_BeginZoomOut(object sender, System.EventArgs e)
        {
            // print("enter");
            Model.StartScale(-1);
        }

        private void ZoomGroup_BeginZoomIn(object sender, System.EventArgs e)
        {
            Model.StartScale(1);

        }

        /// <summary>
        /// 单选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllToggle_OnToggle(object sender, System.EventArgs e)
        {
            UIEventArgs uie = e as UIEventArgs;

            if (uie != null)
            {

                switch (uie.Args)
                {
                    case "Assemble":
                        Model.OnAssemble();
                        break;
                    case "Disassemble":
                        Model.OnDisassemble();
                        break;
                    case "2D":
                        Stage.Render2D(true);
                        break;
                    case "3D":
                        Stage.Render2D(false);
                        break;
                    default:
                        break;
                }

                uie.Used = true;
            }


        }

        //public void TestDown ( )
        //{
        //    Debug.Log ( "Down" );
        //}

        //public void TestUp ( )
        //{
        //    Debug.Log ( "Up" );
        //}
    }
}
