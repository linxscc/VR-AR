/*
 *    日期:2017,7,27
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace PlaceAR
{
    /// <summary>
    /// 本地文件操作
    /// </summary>
	public class FlieTools  
	{

        /// <summary>
        /// 文本写入本地
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="info"></param>
        public static void CreateFile(string path, string name, string info)
        {
            //文件流信息
            StreamWriter sw;
            FileInfo t = new FileInfo(path + "//" + name);
            File.Delete(path + "//" + name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!t.Exists)
            {
                //如果此文件不存在则创建
                sw = t.CreateText();
            }
            else
            {
                sw = t.AppendText();
                // t.Delete();
            }

            //以行的形式写入信息
            sw.WriteLine(info);
            sw.Close();
            sw.Dispose();
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        public static void Delete(string url)
        {

        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool FileExist(string path, string name)
        {
            FileInfo t = new FileInfo(path + "//" + name);
            if (!t.Exists)
            {
                return false;
            }
            else
            {
                return true;
                // t.Delete();
            }
        }
        /// <summary>
        /// 读取文本
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public static T ReadText<T>(string url)
        {
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(url);
            }
            catch (Exception e)
            {
                //路径与名称未找到文件则直接返回空
                return default(T);
            }
            string line = sr.ReadLine();
            T config = JsonFx.Json.JsonReader.Deserialize<T>(line);
            return config;
        }
        /// <summary>
        /// 读取图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Texture2D ReadTexture(string path, string name,int width=132,int height=132)
        {

            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(path + "//" + name, FileMode.Open, FileAccess.Read);
                fileStream.Seek(0, SeekOrigin.Begin);
                //创建文件长度缓冲区
                byte[] bytes = new byte[fileStream.Length];
                //读取文件
                fileStream.Read(bytes, 0, (int)fileStream.Length);
                //释放文件读取流
                fileStream.Close();
                fileStream.Dispose();
                fileStream = null;
                Texture2D texture=new Texture2D(width, height);
                texture.LoadImage(bytes);
                return texture;

            }
            catch (Exception e)
            {
                //路径与名称未找到文件则直接返回空
                return null;
            }

        }
        /// <summary>
        /// 资源写入本地
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="info"></param>
        /// <param name="length"></param>
        public static void CreateModelFile(string path, string name, byte[] info, int length)
        {
            //文件流信息
            //StreamWriter sw;
            Stream sw;
            FileInfo t = new FileInfo(path + "//" + name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            // Debug.Log(path + "//" + name);
            if (!t.Exists)
            {
                //如果此文件不存在则创建
                sw = t.Create();
            }
            else
            {
                //如果此文件存在则打开
                // sw = t.AppendText();
                return;
            }
            //以行的形式写入信息
            sw.Write(info, 0, length);
            sw.Close();
            sw.Dispose();

        }
    }
}
