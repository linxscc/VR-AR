using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MoDouAR
{
    public class WWWAssets
    {
        /// <summary>
        /// 读取单个图片(通过指定路径)------WWW
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">名字</param>
        /// <param name="suffix">后缀</param>
        /// <returns></returns>
        static public IEnumerator LoadLocalTexture(string path, string name, TextureSuffixs suffix)
        {
            path = "file:///" + path + "/" + name;
            switch (suffix)
            {
                case TextureSuffixs.JPG:
                    path += ".jpg";
                    break;
                case TextureSuffixs.PNG:
                    path += ".png";
                    break;
                case TextureSuffixs.EXR:
                    path += ".exr";
                    break;
                default:
                    break;
            }
            WWW www = new WWW(path);
            yield return www;
        }

    }
}
