/***
 * 
 *    Title: UI框架
 *           主题:  资源加载管理器
 *    Description: 
 *           功能: 在Unity的Resources类的基础之上，增加了“缓存”的处理
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

namespace vPlace_FW
{
    public class ResourcesManager : MonoBehaviour
    {
        #region 字段

        private static ResourcesManager instance;

        /// <summary>
        /// 容器键值对集合
        /// </summary>
        private Hashtable ht = null;

        #endregion


        public static ResourcesManager GetInstance()
        {
            if (instance == null)
                instance = new GameObject("ResourcesManager").AddComponent<ResourcesManager>();
            return instance;
        }

        void Awake()
        {
            ht = new Hashtable();
        }


        #region 公有方法

        /// <summary>
        /// 调用资源（带对象缓冲技术）
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="path">路径</param>
        /// <param name="isCatch">是否缓存</param>
        /// <returns></returns>
        public T LoadResources<T>(string path, bool isCatch) where T : UnityEngine.Object
        {
            if (ht.Contains(path))
                return ht[path] as T;

            T TResource = Resources.Load<T>(path);
            if (TResource == null)
                Debug.LogError(GetType() + " / GetInstance() / TResource 提取的资源找不到，请检查。 path = " + path);
            else if (isCatch)
                ht.Add(path, TResource);

            return TResource;
        }

        /// <summary>
        /// 调用资源（带对象缓冲技术）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isCatch"></param>
        /// <returns></returns>
        public GameObject LoadAsset(string path, bool isCatch)
        {
            GameObject goObj = LoadResources<GameObject>(path, isCatch);
            GameObject goObjClone = Instantiate<GameObject>(goObj);
            if (goObjClone == null)
                Debug.LogError(GetType() + "/LoadAsset()/克隆资源不成功，请检查。 path=" + path);
            return goObjClone;
        }
        #endregion
    }
}
