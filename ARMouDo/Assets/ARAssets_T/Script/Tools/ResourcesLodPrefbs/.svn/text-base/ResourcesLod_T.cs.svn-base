using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Tools_XYRF
{
    /// <summary>
    /// Resource 预制资源加载
    /// </summary>
    public class ResourcesLod_T
    {
        /// <summary>
        /// 路径+资源[GameObject]
        /// </summary>
        /// <param name="YZJname"></param>
        /// <returns></returns>
        static public GameObject ResourcesLodObj(string YZJname)
        {
            UnityEngine.Object obj_ = Resources.Load(YZJname, typeof(GameObject));
            GameObject ga_ = UnityEngine.Object.Instantiate(obj_) as GameObject;
            ga_.name = ga_.name.Replace("(Clone)", "");
            return ga_;
        }
        /// <summary>
        /// 路径+资源[GameObject]
        /// </summary>
        /// <param name="YZJname"></param>
        /// <param name="parent">父对象</param>
        /// <returns></returns>
        static public GameObject ResourcesLodObj(string YZJname, Transform parent)
        {
            UnityEngine.Object obj_ = Resources.Load(YZJname, typeof(GameObject));
            GameObject ga_ = UnityEngine.Object.Instantiate(obj_, parent) as GameObject;
            ga_.name = ga_.name.Replace("(Clone)", "");
            return ga_;
        }

        /// <summary>
        /// 路径+资源[ParticleSystem]
        /// </summary>
        /// <param name="YZJname"></param>
        /// <returns></returns>
        static public ParticleSystem ResourcesLodPar(string YZJname)
        {
            UnityEngine.Object obj_ = Resources.Load(YZJname, typeof(ParticleSystem));
            ParticleSystem ga_ = UnityEngine.Object.Instantiate(obj_) as ParticleSystem;
            ga_.name = ga_.name.Replace("(Clone)", "");
            return ga_;
        }

        /// <summary>
        /// 加载Sprite
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">文件名</param>
        static public Sprite ResourcesLoad_SPR(string path, string name)
        {
            path = path + "/" + name;
            Sprite go = Resources.Load<Sprite>(path);
            return go;
        }















        /// <summary>
        ///  /// <summary>
        /// 路径+资源[Sprite]
        /// </summary>
        /// <param name="YZJname"></param>
        /// <returns></returns>
        static public Sprite ResourcesLodSpr(string YZJname, int width, int height)
        {
            //创建文件读取流
            FileStream fileStream;
            try
            {
                fileStream = new FileStream(YZJname, FileMode.Open, FileAccess.Read);
                fileStream.Seek(0, SeekOrigin.Begin);
                //创建文件长度缓冲区
                byte[] bytes = new byte[fileStream.Length];
                //读取文件
                fileStream.Read(bytes, 0, (int)fileStream.Length);
                //释放文件读取流
                fileStream.Close();
                fileStream.Dispose();
                fileStream = null;

                //创建Texture
                Texture2D texture = new Texture2D(width, height);
                texture.LoadImage(bytes);

                //创建Sprite
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                return sprite;
            }
            catch (Exception e)
            {
                //路径与名称未找到文件则直接返回空
                return null;
            }
        }




    }
}
