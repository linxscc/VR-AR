/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlaceAR.LabelDatas;

namespace PlaceAR
{
	public class LayoutGround : MonoBehaviour 
	{
        /// <summary>
        /// 默认组
        /// </summary>
        public List<ItemData> configLocal = new List<ItemData>();

        public void AddItem(List<ItemData> configLocal)
        {
            this.configLocal = configLocal;
        }

	}


}
