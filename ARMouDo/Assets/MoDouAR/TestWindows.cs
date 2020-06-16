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
using DG.Tweening;
namespace MoDouAR
{
    public class TestWindows : Window<TestWindows>
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
                return "TestWindows";
            }
        }

        public override string Path
        {
            get
            {
                return "TestWindows";
            }
        }
        public override UIType CurrentUIType
        {
            get
            {
                currentUIType.uiFormType = UIFormType.Fixed;
                return base.CurrentUIType;
            }

            set
            {
                base.CurrentUIType = value;
            }
        }
        // Use this for initialization
        public override void Start()
        {
            base.Start();
            GetComponentInChildren<Button>().onClick.AddListener(Onclick);
        }
        public void Onclick()
        {
            C();
        }
        public void C()
        {
            Close();
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}