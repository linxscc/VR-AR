/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
namespace MoDouAR
{
    /// <summary>
    /// 记录日志
    /// </summary>
    public class SystemLog
    {
        // static public string path = Application.dataPath + "/LogFile.txt";
        
        public static bool EnableLog = true;
        public static bool isWriteLog = true;
        /// <summary>
        /// 记录点击按钮
        /// </summary>
        /// <param name="WindData">窗口信息</param>
        /// <param name="buttonName">按钮名称</param>
        public static void LogClick(Wind WindData,string buttonName)
        {

        }
        public static void Log(object message)
        {
            Log(message, null);
        }
        public static void Log(object message, Object context)
        {
            if (EnableLog)
            {
                Debug.Log(message, context);
            }
            if (isWriteLog) WriteLog(message);
        }
        public static void LogError(object message)
        {
            LogError(message, null);
        }
        public static void LogError(object message, Object context)
        {
            if (EnableLog)
            {
                Debug.LogError(message, context);
            }
            if (isWriteLog) WriteLog(message);
        }
        public static void LogWarning(object message)
        {
            LogWarning(message, null);
        }
        public static void LogWarning(object message, Object context)
        {
            if (EnableLog)
            {
                Debug.LogWarning(message, context);
            }
            if (isWriteLog) WriteLog(message);
        }

        public static void Exception(System.Exception exception)
        {

            Exception(exception, null);
        }

        public static void Exception(System.Exception exception, Object context)
        {
            Debug.LogException(exception, context);
            if (isWriteLog) WriteLog(exception);
        }

        public static void WriteLog(object str)
        {
            lock ("WriteLog")
            {
                // using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                //  {
                // byte[] data = Encoding.UTF8.GetBytes(str.ToString() + "\n");
                // fs.Write(data, 0, data.Length);
                // }
            }
        }

    }
}