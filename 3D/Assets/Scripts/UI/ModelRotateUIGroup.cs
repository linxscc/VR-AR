using UnityEngine;
using System.Collections;

namespace ModelViewerProject.UI
{
    using UnityEngine.UI;
    using System;
    using Label;
    using System.Collections.Generic;
    using Label3D;

    public class RotateEventArgs : System.EventArgs
    {
        public bool Used { set; get; }
        public Vector2 Direction { set; get; }
    }

    public class ModelRotateUIGroup : MonoBehaviour
    {
        public Label2DUIController label2DUIController;
        public Button HomeButton;

        public LongTapButton LeftButton;
        public LongTapButton RightButton;
        public LongTapButton UpButton;
        public LongTapButton DownButton;


        public event EventHandler OnBackHome;

        public event EventHandler OnStatic;
        public event EventHandler OnRotate;
        public ToggleButton toggleButton;
        public ToggleButton toggleButton2D3D;
        public ZoomInOutUIGroup groupZoom;

        void Awake()
        {
            //HomeButton.interactable = false;
            //toggleButton = transform.FindChild("Toggle Dis/Assemble").GetComponent<ToggleButton>();
           // toggleButton2D3D= transform.FindChild("Toggle 2/3D").GetComponent<ToggleButton>();
        }

        public void BackHome()
        {
            if (OnBackHome != null)
            {
               // foreach (KeyValuePair<string, Label3DHandler> item in label2DUIController.group)
               // {
                    //item.Value.RefreshChildButton(false);
             //   }
                OnBackHome(this, System.EventArgs.Empty);
                HomeButton.interactable = false;
            }
        }

        public void StopRotate()
        {
            if (OnStatic != null)
            {
                OnStatic(this, System.EventArgs.Empty);
            }
        }

        public void RotateToLeft()
        {
            var dir = Vector2.left;
            HandleOnRotate(dir);
        }

        public void RotateToRight()
        {
            var dir = Vector2.right;
            HandleOnRotate(dir);
        }

        public void RotateToUp()
        {
            var dir = Vector2.up;
            HandleOnRotate(dir);
        }

        public void RotateToDown()
        {
            var dir = Vector2.down;
            HandleOnRotate(dir);
        }

        void HandleOnRotate(Vector2 dir)
        {
            if (OnRotate != null)
            {
                RotateEventArgs e = new RotateEventArgs() { Direction = dir };
                OnRotate(this, e);
                if (e.Used)
                {
                    SetHomeToInteractable();
                }
            }
        }

        public void SetHomeToInteractable()
        {
            HomeButton.interactable = true;
        }
        private void Update()
        {

            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    RotateToLeft();
            //}
            //if (Input.GetKeyUp(KeyCode.A))
            //{
            //    StopRotate();
            //}
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    RotateToUp();
            //}
            //if (Input.GetKeyUp(KeyCode.W))
            //{
            //    StopRotate();
            //}
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    RotateToDown();
            //}
            //if (Input.GetKeyUp(KeyCode.S))
            //{
            //    StopRotate();
            //}
            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    RotateToRight();
            //}
            //if (Input.GetKeyUp(KeyCode.D))
            //{
            //    StopRotate();
            //}
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    BackHome();
            //}
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    groupZoom.StartZoomIn();
            //}
            //if (Input.GetKeyUp(KeyCode.Q))
            //{
            //    groupZoom.StopZoomIn();
            //}
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    groupZoom.StartZoomOut();
            //}
            //if (Input.GetKeyUp(KeyCode.E))
            //{
            //    groupZoom.StopZoomOut();
            //}
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (toggleButton.isOneEnable)
                    toggleButton.OnButtonClicked("Assemble");
                else
                    toggleButton.OnButtonClicked("Disassemble");
               // toggleButton.isOneEnable = !toggleButton.isOneEnable;
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (toggleButton2D3D.isOneEnable)
                    toggleButton2D3D.OnButtonClicked("2D");
                else
                    toggleButton2D3D.OnButtonClicked("3D");
               // toggleButton2D3D.isOneEnable = !toggleButton2D3D.isOneEnable;
            }
        }
    }
}