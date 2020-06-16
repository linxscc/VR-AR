using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tools_XYRF
{
    /// <summary>
    /// 数据类型转换
    /// </summary>
    public class ConvertStringOrPos
    {
        /// <summary>
        /// string → Vector
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Vector3 StringToVector3(string str)
        {
            str = str.Replace("(", "").Replace(")", "");
            string[] s = str.Split(',');
            return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
        }
        /// <summary>
        /// Vector3 → String
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static string Vector3ToString(float x, float y, float z)
        {
            string vc = "(" + x + "," + y + "," + z + ")";
            return vc;
        }

    }
}
