using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace ModelViewerProject.UI
{
    public class UIEventArgs : System.EventArgs
    {
        public bool Used { set; get; }
        public string Args { set; get; }
    }

    public class ToggleButton : MonoBehaviour
    {

        public bool isOneEnable;

        public Button ButtonOne;
        public Button ButtonOther;

        public event EventHandler OnToggle;

        void Awake()
        {
            OnEnableOther();
        }

        public void OnButtonClicked(string args)
        {

            if (OnToggle != null)
            {
                var e = new UIEventArgs() { Args = args };
                OnToggle(this, e);
                if (e.Used)
                {
                    UpdateView();
                }
            }

        }

        void UpdateView()
        {
            if (isOneEnable)
            {
                OnEnableOther();
            }
            else
            {
                OnEnableOne();
            }
        }

        public void OnEnableOne()
        {
            isOneEnable = true;
            ButtonOne.interactable = true;
            ButtonOther.interactable = false;
        }

        public void OnEnableOther()
        {
            isOneEnable = false;
            ButtonOne.interactable = false;
            ButtonOther.interactable = true;
        }

    }

}