/*
 *    日期:2017/6/30
 *    作者:
 *    标题:
 *    功能:读写文件
*/
using UnityEngine;
using System.Collections;
using System.IO;

namespace PlaceAR
{
    /// <summary>
    /// 读写工具
    /// </summary>
    public static class ToolReadWrite
    {
        /// <summary>
        /// 保存模型
        /// </summary>
       public static IEnumerator SavePrefab(string url,string name)
        {
            WWW w = new WWW(url);
            yield return w;
            if (w.isDone)
            {
                byte[] model = w.bytes;
                int length = model.Length;
                //写入模型到本地
                CreateModelFile(Application.persistentDataPath, name, model, length);
            }
        }
        private static void CreateModelFile(string path, string name, byte[] info, int length)
        {
            //文件流信息
            //StreamWriter sw;
            Stream sw;
            FileInfo t = new FileInfo(path + "//" + name);
            if (!t.Exists)
            {
                //如果此文件不存在则创建
                sw = t.Create();
            }
            else
            {
                //如果此文件存在则打开
                //sw = t.Append();
                return;
            }
            //以行的形式写入信息
            //sw.WriteLine(info);
            sw.Write(info, 0, length);
            //关闭流
            sw.Close();
            //销毁流
            sw.Dispose();
        }
    }
}
