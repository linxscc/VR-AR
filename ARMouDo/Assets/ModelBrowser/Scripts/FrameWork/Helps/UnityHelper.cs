/***
 * 
 *    Title: UI框架
 *           主题: Unity 帮助脚本
 *    Description: 
 *           功能: 提供一些常用的功能方法实现，方便快速开发。
 *           1: 
 *           2: 
 *           3: 
 *           4: 
 *                          
 *    Date: 2017/07
 *    Version: 0.1
 *	  Author: 
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using System.Xml;

namespace vPlace_FW
{
    public class UnityHelper : MonoBehaviour
    {
        /// <summary>
        /// 查找子节点（递归）
        /// </summary>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">所查找子对象的名称</param>
        /// <returns></returns>
        public static Transform FindTheChildNode(GameObject goParent, string childName)
        {
            Transform searchTrans = null;             //查找结果

            if (childName.Equals(goParent.name))
            {
                searchTrans = goParent.transform;
                return searchTrans;
            }

            searchTrans = goParent.transform.Find(childName);
            if (searchTrans == null)
            {
                foreach (Transform trans in goParent.transform)
                {
                    searchTrans = FindTheChildNode(trans.gameObject, childName);
                    if (searchTrans != null)
                        return searchTrans;
                }
            }
            return searchTrans;
        }

        /// <summary>
        /// 获取特定子节点（对象）脚本
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static T GetTheChildNodeComponentScript<T>(GameObject goParent, string childName) where T : Component
        {
            Transform searchTrans = null;

            searchTrans = FindTheChildNode(goParent, childName);
            if (searchTrans != null)
                return searchTrans.gameObject.GetComponent<T>();
            else
                return null;
        }

        /// <summary>
        /// 给特定子节点添加脚本
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static T AddTheChildNodeComponentScript<T>(GameObject goParent, string childName) where T : Component
        {
            Transform searchTrans = null;

            searchTrans = FindTheChildNode(goParent, childName);
            if (searchTrans != null)
            {
                T[] componentScriptArray = searchTrans.GetComponents<T>();
                for (int i = 0; i < componentScriptArray.Length; ++i)
                {
                    if (componentScriptArray[i] != null)
                        Destroy(componentScriptArray[i]);
                }
                return searchTrans.gameObject.AddComponent<T>();
            }
            else
                return null;
        }

        /// <summary>
        /// 给子节点添加父对象
        /// </summary>
        /// <param name="parent">父对象</param>
        /// <param name="child">子对象</param>
        public static void AddChildNodeToParentNode(Transform parent, Transform child)
        {
            child.SetParent(parent, false);
            child.localPosition = Vector3.zero;
            child.localEulerAngles = Vector3.zero;
            child.localScale = Vector3.one;
        }

        ///// <summary>
        ///// 从XML配置文件中读取版本号以及模型数据
        ///// </summary>
        ///// <param name="xmlPath"></param>
        ///// <returns></returns>
        //public static UserData ReadXmlContent(string xmlPath)
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load(xmlPath);
        //    XmlNodeList nodeList = xmlDoc.SelectSingleNode("UserDataNew").ChildNodes;
        //    UserData userData = new UserData();
        //    foreach (XmlNode xn in nodeList)
        //    {
        //        switch (xn.Name)
        //        {
        //            case "versions":
        //                userData.appVersion.appVersion = xn.ChildNodes.Item(0).InnerXml;
        //                break;
        //            case "stereoCamera":
        //                XmlNodeList nls = xn.ChildNodes;
        //                foreach (XmlNode item in nls)
        //                {
        //                    switch (item.Name)
        //                    {
        //                        case "point":
        //                            userData.stereoCamera.point = item.InnerXml;
        //                            break;
        //                        case "eyeDistance":
        //                            userData.stereoCamera.eyeDistance = item.InnerXml;
        //                            break;
        //                        case "modelDistance":
        //                            userData.stereoCamera.modelDistance = item.InnerXml;
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                break;
        //            case "prefab":
        //                userData.modelPrefabName.modelPrefabName = xn.ChildNodes.Item(0).InnerXml;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    return userData;
        //}

    }
}