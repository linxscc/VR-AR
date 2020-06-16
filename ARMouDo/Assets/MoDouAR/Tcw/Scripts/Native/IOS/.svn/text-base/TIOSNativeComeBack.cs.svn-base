using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MoDouAR
{
    public class TIOSNativeComeBack : MonoBehaviour
    {
        //使用:   TIOSNativeComeBack.stopButton=delegate{};
        static public TIOSNativeHandle<string> stopButton = null;
        static public GameObject tIOSNative;
        static public bool TIOSNativeInit()
        {
            try
            {
                if (tIOSNative == null)
                {
                    tIOSNative = new GameObject();
                    tIOSNative.name = "TIOSNative";
                    tIOSNative.AddComponent<TIOSNativeComeBack>();
                    DontDestroyOnLoad(tIOSNative);
                }
                if (tIOSNative == null)
                    return false;
                return true;
            }
            catch (Exception e)
            {
                if (tIOSNative != null)
                    Destroy(tIOSNative);
                return false;
            }
        }
        private void IOSButtonClick(string message)
        {
            stopButton(message);
            stopButton = null;
        }


    }
}