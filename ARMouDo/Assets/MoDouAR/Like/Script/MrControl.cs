/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System;

namespace MoDouAR
{
    public class MrControl : Window<MrControl>
    {
        public override int ID
        {
            get
            {
                return 8;
            }
        }

        public override string Name
        {
            get
            {
                return "MrControl";
            }
        }

        public override string Path
        {
            get
            {
                return "UI/MrControl";
            }
        }
        public void OpenMr()
        {
            ARKitControl.Instance.lastOperator = Global.OperatorModel;
            
            Global.OperatorModel = OperatorMode.MRModel;
            Close();

        }
    }
}