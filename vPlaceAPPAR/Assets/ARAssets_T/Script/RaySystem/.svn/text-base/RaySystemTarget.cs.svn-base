using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tools_XYRF
{
    public class RaySystemTarget
    {

        /// <summary>
        /// 获得 射线打中目标的坐标 
        /// </summary>
        /// <param name="InputPos_">输入点</param>
        /// <param name="tag">目标标签</param>
        /// <param name="layer">目标层</param>
        /// <returns>return 目标的坐标, return Vector3.zero 为空</returns>
        static public Vector3 GetRayTargetPoint(Vector3 InputPos_, string tag_, LayerMask layer_)
        {
            RaycastHit[] hits;
            Ray ray;
            ray = Camera.main.ScreenPointToRay(InputPos_);
            hits = Physics.RaycastAll(ray, 1000f);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.tag == tag_ && hits[i].transform.gameObject.layer == layer_)
                    return hits[i].point;
            }
            return Vector3.zero;
        }
        /// <summary>
        /// 获得 射线打中的目标
        /// </summary>
        /// <param name="InputPos_">输入点</param>
        /// <param name="tag_">目标标签</param>
        /// <param name="layer_">目标层</param>
        /// <returns>return 目标</returns>
        static public Transform GEtRayTargetTra(Vector3 InputPos_, string tag_, LayerMask layer_)
        {
            RaycastHit[] hits;
            Ray ray;
            ray = Camera.main.ScreenPointToRay(InputPos_);
            hits = Physics.RaycastAll(ray, 1000f);
            for (int i = 0; i < hits.Length; i++)
            {
                if (layer_ == Global.modelLayerMask)
                {
                    if (hits[i].transform.tag == tag_)
                        return hits[i].transform;
                }
                else
                {
                    if (hits[i].transform.tag == tag_ && hits[i].transform.gameObject.layer == layer_)
                        return hits[i].transform;
                }
            }
            return null;
        }
    }
}
