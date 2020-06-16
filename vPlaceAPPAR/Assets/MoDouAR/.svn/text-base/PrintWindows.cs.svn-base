/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace MoDouAR
{
    public class PrintWindows : Window<PrintWindows>
    {
        public override int ID
        {
            get
            {
                return 0;
            }
        }

        public override string Name
        {
            get
            {
                return "PrintWindows";
            }
        }

        public override string Path
        {
            get
            {
                return "PrintWindows";
            }
        }
        public override void Start()
        {
            base.Start();
          transform.Find("close").  GetComponent<Button>().onClick.AddListener(OnClick);
        }
        public override UIType CurrentUIType
        {
            get
            {
                currentUIType.uiFormShowMode = UIFormShowMode.ReverseChange;
                currentUIType.uiFormType = UIFormType.PopUp;
                currentUIType.uiFormLucencyType = UIFormLucencyType.Translucence;
                return base. CurrentUIType;
            }

            set
            {
                base.CurrentUIType = value;

            }
        }
        public void OnClick()
        {
            Close();
        }
    }
}