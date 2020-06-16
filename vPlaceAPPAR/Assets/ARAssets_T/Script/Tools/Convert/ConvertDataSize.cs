using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tools_XYRF
{
    /// <summary>
    /// 数据转换处理
    /// </summary>
    public class ConvertDataSize
    {
        /// <summary>
        /// 返回集合中最大值 or 最小值[界定 +- 1000]
        /// </summary>
        /// <param name="da">集合</param>
        /// <param name="max">true =最大, false=最小</param>
        /// <returns></returns>
        public static object ChangeSize(List<int> da, bool max)
        {
            if (max)  //取最大值
            {
                int maxOrMin = -1000;
                for (int i = 0; i < da.Count; i++)
                {
                    if (da[i] > maxOrMin)
                        maxOrMin = da[i];
                }
                return maxOrMin;
            }
            else  //取最小值
            {
                int maxOrMin = 1000;
                for (int i = 0; i < da.Count; i++)
                {
                    if (da[i] < maxOrMin)
                        maxOrMin = da[i];
                }
                return maxOrMin;
            }
        }
    }
}
