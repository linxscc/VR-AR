using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using UnityEngine.UI;


namespace Tools_XYRF
{
    /// <summary>
    /// 数组 集合 相互转换
    /// </summary>
    public class ConvertArrayOrList
    {
        /// <summary>
        /// 任意类型数组转换
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        static public Transform[] ConvertToArray(object[] objs)
        {
            Transform[] tras_ = new Transform[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                tras_[i] = objs[i] as Transform;
            }
            return tras_;
        }

        /// <summary>
        /// 任意类型数组转换
        /// </summary>
        /// <param name="tk_">数组</param>
        /// <returns></returns>
        static public T[] FromTypeArray<T, TK>(TK[] tk_)
        {
            T[] tArray = new T[tk_.Length];
            for (int i = 0; i < tk_.Length; i++)
            {
                tArray[i] = FromType<T, TK>(tk_[i]);
            }
            return tArray;
        }

        /// <summary>
        /// 任意类型 转换
        /// </summary>
        /// <param name="tk_">待转换的值</param>
        /// <returns></returns>
        static public T FromType<T, TK>(TK tk_)
        {
            try
            {
                return (T)Convert.ChangeType(tk_, typeof(T), CultureInfo.InvariantCulture);
            }
            catch
            {
                return default(T);
            }
        }

        static public Transform[] ConvertToArrayUI(Button[] objs)
        {
            Transform[] tras_ = new Transform[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                tras_[i] = objs[i].transform;
            }
            return tras_;
        }
        static public Transform[] ConvertToArrayUI(Slider[] objs)
        {
            Transform[] tras_ = new Transform[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                tras_[i] = objs[i].transform;
            }
            return tras_;
        }
        static public Transform[] ConvertToArrayUI(Toggle[] objs)
        {
            Transform[] tras_ = new Transform[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                tras_[i] = objs[i].transform;
            }
            return tras_;
        }
        static public Transform[] ConvertToArrayUI(Dropdown[] objs)
        {
            Transform[] tras_ = new Transform[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                tras_[i] = objs[i].transform;
            }
            return tras_;
        }
        static public Transform[] ConvertToArrayUI(InputField[] objs)
        {
            Transform[] tras_ = new Transform[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                tras_[i] = objs[i].transform;
            }
            return tras_;
        }
        static public Transform[] ConvertToArrayUI(Text[] objs)
        {
            Transform[] tras_ = new Transform[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                tras_[i] = objs[i].transform;
            }
            return tras_;
        }
        static public Transform[] ConvertToArrayUI(Image[] objs)
        {
            Transform[] tras_ = new Transform[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                tras_[i] = objs[i].transform;
            }
            return tras_;
        }

    }
}
