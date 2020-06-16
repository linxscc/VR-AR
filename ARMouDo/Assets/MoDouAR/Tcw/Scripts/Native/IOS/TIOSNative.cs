using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


namespace MoDouAR
{
    public class TIOSNative 
    {

        [DllImport("__Internal")]
        private static extern void FooPluginFunction();

        /// <summary>
        /// 创建按钮
        /// </summary>
        /// <param name="fun">点击按钮 触发方法</param>
        static public void CreateIOSButton(TIOSNativeHandle<string> fun)
        {
            if (TIOSNativeComeBack.TIOSNativeInit())
            {
                FooPluginFunction();
                TIOSNativeComeBack.stopButton = fun;
            }
        }

    }
}
