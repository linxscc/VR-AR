/***
 * 
 *    Title: "SUIFW" UI框架项目
 *           主题： 语言国际化 
 *    Description: 
 *           功能： 使得我们发布的游戏，可以根据不同的国家，显示不同的语言信息。
 *                  
 *    Date: 2017
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vPlace_FW
{
	public class LauguageMgr {
        //本类实例
	    public static LauguageMgr instance;
        //语言翻译的缓存集合
	    private Dictionary<string, string> dicLauguageCache;



	    private LauguageMgr()
	    {
	         dicLauguageCache=new Dictionary<string, string>();
             //初始化语言缓存集合
	         InitLauguageCache();
	    }

        /// <summary>
        /// 得到本类实例
        /// </summary>
        /// <returns></returns>
	    public static LauguageMgr GetInstance()
	    {
	        if(instance==null)
            {
                instance=new LauguageMgr();
            }
	        return instance;
	    }

        /// <summary>
        /// 显示文本信息
        /// </summary>
        /// <param name="lauguageID">语言的ID</param>
        /// <returns></returns>
	    public string ShowText(string lauguageID)
	    {
	        string strQueryResult = string.Empty;           //查询结果

            //参数检查
	        if (string.IsNullOrEmpty(lauguageID)) return null;

            //查询处理
            if (dicLauguageCache != null && dicLauguageCache.Count>=1)
            {
                dicLauguageCache.TryGetValue(lauguageID, out strQueryResult);
                if (!string.IsNullOrEmpty(strQueryResult))
                {
                    return strQueryResult;
                }
            }

            Debug.Log(GetType() + "/ShowText()/ Query is Null!  Parameter lauguageID: " + lauguageID);
	        return null;
	    }

	    /// <summary>
        /// 初始化语言缓存集合
        /// </summary>
	    private void InitLauguageCache()
	    {
            IConfigManager config = new ConfigManagerByJson("LauguageJSONConfig");
	        if (config!=null)
	        {
	            dicLauguageCache = config.AppSetting;
	        }
	    }


	}
}