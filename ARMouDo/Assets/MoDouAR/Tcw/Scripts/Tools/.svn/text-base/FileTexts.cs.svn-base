using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;



/// <summary>
/// 本地文本操作
/// </summary>
public class FileTexts
{

    /// <summary>
    /// 文本写入本地 [StreamWriter]
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="name">文本名</param>
    /// <param name="info">待写入 信息</param>
    public static void CreateFile(string path, string name, string info)
    {
        //文件流信息
        StreamWriter sw;
        FileInfo t = new FileInfo(path + "//" + name);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        else
        {
            if (FileFolders.FileExist(path, name))
                File.Delete(path + "//" + name);
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
    /// 文本写入本地 [FileStream]
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="name">文本名</param>
    /// <param name="info">待写入 信息</param>
    static public void FileStreamWrite(string path, string name, string info)
    {
        byte[] byData;
        char[] charData;
        try
        {
            FileStream files = new FileStream(path + "//" + name, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            //获得字符数组
            charData = info.ToCharArray();
            //初始化字节数组
            byData = new byte[charData.Length];
            //将字符数组转换为正确的字节格式
            Encoder enc = Encoding.UTF8.GetEncoder();
            enc.GetBytes(charData, 0, charData.Length, byData, 0, true);
            files.Seek(0, SeekOrigin.Begin);
            files.Write(byData, 0, byData.Length);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// 读取文本 → Json结构读取 [StreamReader]
    /// </summary>
    /// <param name="url">地址</param>
    /// <returns>return 结构数据</returns>
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
        sr.Close();
        sr.Dispose();
        return config;
    }


    /// <summary>
    /// 读取文本 → Json结构读取 [FileStream]
    /// </summary>
    /// <param name="url">地址</param>
    /// <returns>return 结构数据</returns>
    static public T FileStreamRead<T>(string url)
    {
        byte[] data = new byte[100];
        char[] charData = new char[100];
        try
        {
            FileStream file = new FileStream(url, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete | FileShare.ReadWrite);
            //文件指针指向0位置
            file.Seek(0, SeekOrigin.Begin);
            //读入两百个字节
            file.Read(data, 0, 200);
            //提取字节数组
            Decoder dec = Encoding.UTF8.GetDecoder();
            dec.GetChars(data, 0, data.Length, charData, 0);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        T config = JsonFx.Json.JsonReader.Deserialize<T>(Convert.ToString(charData));
        return config;
    }


    /// <summary>
    /// 读取文本 → 读取全部 [FileStream]
    /// </summary>
    /// <param name="url">地址</param>
    /// <returns>return string数据</returns>
    static public string FileStreamRead(string url)
    {
        byte[] data = new byte[100];
        char[] charData = new char[100];
        try
        {
            FileStream file = new FileStream(url, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete | FileShare.ReadWrite);
            //文件指针指向0位置
            file.Seek(0, SeekOrigin.Begin);
            //读入两百个字节
            file.Read(data, 0, 200);
            //提取字节数组
            Decoder dec = Encoding.UTF8.GetDecoder();
            dec.GetChars(data, 0, data.Length, charData, 0);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Convert.ToString(charData);
    }


    /// <summary>
    /// 读取文本 → 行读取 [File]
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>return 文本行字符 数组</returns>
    internal static string[] ReadTextLine(string path)
    {
        if (!File.Exists(path))
        {
            //路径与名称未找到文件则直接返回空
            return null;
        }
        else
        {
            return File.ReadAllLines(path);
        }
    }


    /// <summary>
    /// 读取文本 → 读取全部  [File]
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>return 文本所有字符</returns>
    internal static string ReadTextAll(string path)
    {
        if (!File.Exists(path))
        {
            //路径与名称未找到文件则直接返回空
            return null;
        }
        else
        {
            return File.ReadAllText(path);
        }
    }


    /// <summary>
    /// 资源写入本地
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <param name="info"></param>
    /// <param name="length"></param>
    public static void CreateAssetFile(string path, string name, byte[] info, int length)
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



    //    public static IEnumerator LoadObj(string url, CallBack<WWW, string> callback, string name)
    //    {
    //        WWW www = new WWW(url);
    //        yield return www;
    //        if (www.error == "")
    //            callback(www, name);
    //        else
    //        {
    //#if UNITY_IPHONE

    //#elif UNITY_ANDROID
    //                    PrintMenuControl.Singleton.Open("加载失败!");
    //#endif

    //        }

    //    }



    /// <summary>
    /// 返回最后一个斜杠后的字符
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string ReturnNmae(string url)
    {
        string[] str = url.Split('/');
        return str[str.Length - 1];
    }

    /// <summary>
    /// 替换指定字符 / 插入指定位置
    /// </summary>
    /// <param name="oldStr">原字符串</param>
    /// <param name="replaceStr">待替换的字符串</param>
    /// <param name="newStr">替换后的字符</param>
    static public string Replace(string oldStr, string replaceStr, string newStr)
    {
        if (oldStr.Contains(replaceStr))
        {
            oldStr = oldStr.Replace(replaceStr, newStr);
        }
        return oldStr;
    }

}


