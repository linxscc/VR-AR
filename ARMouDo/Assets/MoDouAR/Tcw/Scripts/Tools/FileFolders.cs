using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;



/// <summary>
/// 本地文件操作
/// </summary>
public class FileFolders
{

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path">路径 </param>
    /// <param name="name">资源名</param>
    public static void Delete(string path, string name)
    {
        FileInfo t = new FileInfo(path + "//" + name);
        if (t.Exists)
            File.Delete(path + "//" + name);
    }


    /// <summary>
    /// 删除文件夹
    /// </summary>
    /// <param name="path">路径</param>
    public static void DeleteFolder(string path)
    {
        // FileInfo t = new FileInfo(path );
        if (Directory.Exists(path))
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            dir.Delete(true);
        }
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
    /// 判断文件是否存在
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns></returns>
    public static bool FileExist(string path)
    {
        FileInfo t = new FileInfo(path);
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
    /// 获取文件扩展名  
    /// </summary>  
    /// <param name="filePath">路径</param>  
    /// <returns>返回文件的扩展名 / Null</returns>  
    static public string GetFileExtension(string filePath)
    {
        string filePathExtension = "";
        try
        {
            if (Directory.Exists(filePath))
            {
                filePathExtension = filePath.Substring(filePath.LastIndexOf(".") + 1);
            }
        }
        catch (Exception ex)
        {
            //    MessageBox.Show(ex.Message);
            //打印 错误信息
            Debug.Log(ex.Message);
            return "";
        }
        return filePathExtension;
    }

}
