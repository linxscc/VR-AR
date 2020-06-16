/***
 * 
 *    Title: UI框架
 *           主题: Json解析异常 
 *    Description: 
 *           功能: 专门负责对于Json由于路径错误，或者Json格式错误造成的异常，进行捕获
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
using System;

namespace vPlace_FW
{
    public class JsonAnalysisIsException : Exception
    {
        public JsonAnalysisIsException() : base() { }

        public JsonAnalysisIsException(string exceptionMessage) : base(exceptionMessage) { }
    }
}
