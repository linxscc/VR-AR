using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; //引入供序列化Image对象使用


namespace MoDouAR
{
    /// <summary>
    /// 图片存储
    /// </summary>
    public class FileTexture
    {
        /// <summary>
        /// 区域截图
        /// </summary>
        /// <param name="rect">Rect 截图目标区域</param>
        /// <returns>Texture2D</returns>
        static public Texture2D CaptureScreen(Rect rect)
        {
            Texture2D texture = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();
            Texture2D _lastTakenScreenshot = texture;
            return _lastTakenScreenshot;
        }
        /// <summary>  
        /// 对相机截图。   
        /// </summary>  
        /// <param name="camera">Camera.要被截屏的相机</param>  
        /// <param name="rect">Rect.截屏的区域</param>  
        /// <returns>Texture2D</returns>
        static public Texture2D CaptureCamera(Camera camera, Rect rect)
        {
            // 创建一个RenderTexture对象  
            RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);
            // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
            camera.targetTexture = rt;
            camera.Render();
            //ps: --- 如果这样加上第二个相机，可以实现只截图某几个指定的相机一起看到的图像。  
            //ps: camera2.targetTexture = rt;  
            //ps: camera2.Render();  
            //ps: -------------------------------------------------------------------  

            // 激活这个rt, 并从中中读取像素。  
            RenderTexture.active = rt;
            Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
            screenShot.Apply();

            // 重置相关参数，以使用camera继续在屏幕上显示  
            camera.targetTexture = null;
            //ps: camera2.targetTexture = null;  
            RenderTexture.active = null; // JC: added to avoid errors  
            GameObject.Destroy(rt);

            return screenShot;
        }


        /// <summary>
        /// 保存图片------File.IO
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">名字</param>
        /// <param name="texture">图片</param>
        /// <param name="suffix">后缀</param>
        static public void SaveTexture(string path, string name, Texture2D texture, TextureSuffixs suffix)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                if (FileFolders.FileExist(path, name))
                    File.Delete(path + "//" + name);
            }
            path = path + "/" + name;
            List<byte> bytes = new List<byte>();
            switch (suffix)
            {
                case TextureSuffixs.JPG:
                    path += ".jpg";
                    bytes.AddRange(texture.EncodeToJPG());
                    break;
                case TextureSuffixs.PNG:
                    path += ".png";
                    bytes.AddRange(texture.EncodeToPNG());
                    break;
                case TextureSuffixs.EXR:
                    path += ".exr";
                    bytes.AddRange(texture.EncodeToEXR());
                    break;
                default:
                    break;
            }
            try
            {
                File.WriteAllBytes(path, bytes.ToArray());
                ComeBack.OnSaveLoadTextureSuccesss(FileTextureState.SaveSuccess);  //保存成功
            }
            catch (Exception e)
            {
                ComeBack.OnSaveLoadTextureLosts(FileTextureState.SaveLost, e.Message);  //保存失败
            }
        }

        /// <summary>
        /// 读取单个图片(通过指定路径)------FileStream
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">名字</param>
        /// <param name="suffix">后缀</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>Texture2D</returns>
        static public Texture2D ReadTexture(string path, string name, TextureSuffixs suffix, int width, int height)
        {
            path = path + "/" + name;
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
            float startTime = Time.time;
            try
            {
                Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
                texture.LoadImage(GetImageByte(path));

                //  Debug.Log("IO图片加载用时:" + (Time.time - startTime));
                ComeBack.OnSaveLoadTextureSuccesss(FileTextureState.LoadSucessSingle, Time.time - startTime);  //加载成功
                return texture;
            }
            catch (Exception e)
            {
                //路径与名称未找到文件则直接返回空
                ComeBack.OnSaveLoadTextureLosts(FileTextureState.LoadLostSingle, e.Message);  //加载失败
                return null;
            }
        }

        /// <summary>
        /// 读取文件夹下所有图片------FileStream
        /// </summary>
        /// <param name="pathP">目标文件夹路径</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>Texture2D[]</returns>
        static public Texture2D[] ReadTextures(string pathP, int width, int height)
        {
            List<string> filePaths = new List<string>();
            List<Texture2D> allTex2d = new List<Texture2D>();
            string imgtype = "*.BMP|*.JPG|*.GIF|*.PNG";
            string[] ImageType = imgtype.Split('|');
            try
            {
                //计时
                float startTime = Time.time;
                for (int i = 0; i < ImageType.Length; i++)
                {
                    //获取d盘中a文件夹下所有的图片路径  
                    string[] dirs = Directory.GetFiles(pathP, ImageType[i]);
                    filePaths.AddRange(dirs);
                }
                for (int i = 0; i < filePaths.Count; i++)
                {
                    Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
                    texture.LoadImage(GetImageByte(filePaths[i]));
                    allTex2d.Add(texture);
                    if (i == filePaths.Count - 1)
                    {
                        Debug.Log("IO图片加载用时:" + (Time.time - startTime));
                        ComeBack.OnSaveLoadTextureSuccesss(FileTextureState.LoadSucessAll, Time.time - startTime);  //加载成功
                        return allTex2d.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                ComeBack.OnSaveLoadTextureLosts(FileTextureState.LoadLostAll, e.Message);  //加载失败
            }
            return null;
        }
        /// <summary>
        /// Texture2D → Sprite
        /// </summary>
        /// <param name="texture"></param>
        /// <returns>Sprite</returns>
        static public Sprite SpriteFromTex2D(Texture2D texture)
        {
            if (texture != null)
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return null;
        }
        /// <summary>
        /// 读取图片 区域纹理
        /// </summary>
        /// <param name="texture">图片</param>
        /// <returns>Texture2D</returns>
        static public Texture2D ReadTexture2DTexture(Texture2D texture)
        {
            Texture2D lastTexture = texture;
            Texture2D screenShot;
            //  texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
            try
            {
                List<Color> li = new List<Color>();
                int dex = 0;
                int dey = 0;
                int readWidth = 0;
                if (texture.height >= texture.width)
                {
                    dey = (texture.height - texture.width) / 2;
                    readWidth = texture.width;
                }
                else
                {
                    dex = (texture.width - texture.height) / 2;
                    readWidth = texture.height;
                }
                screenShot = new Texture2D(readWidth, readWidth, TextureFormat.RGB24, false);
                Color[] s = texture.GetPixels(dex, dey, readWidth, readWidth);
                li.AddRange(s);
                screenShot.SetPixels(li.ToArray()); //...
                screenShot.Apply(true, true);
                ComeBack.OnSaveLoadTextureSuccesss(FileTextureState.ReadTextureSucess);  //读取成功
            }
            catch (Exception e)
            {
                ComeBack.OnSaveLoadTextureLosts(FileTextureState.ReadTextureLost, e.Message);   //读取失败
                return lastTexture;
            }
            return screenShot;
        }
        /// <summary>
        /// 缩放纹理
        /// </summary>
        /// <param name="texture">图片</param>
        /// <param name="targetWidth">目标宽度</param>
        /// <param name="targetHeight">目标高度</param>
        /// <returns></returns>
        static public Texture2D ScaleTexture(Texture2D texture, int targetWidth, int targetHeight)
        {
            Texture2D result = new Texture2D(targetWidth, targetHeight, texture.format, false);
            for (int i = 0; i < result.height; i++)
            {
                for (int c = 0; c < result.width; c++)
                {
                    Color newColor = texture.GetPixelBilinear((float)c / (float)result.width, (float)i / (float)result.height);
                    result.SetPixel(c, i, newColor);
                }
            }
            result.Apply();
            return result;
        }

        #region 私有方法
        /// <summary>  
        /// 根据图片路径返回图片的字节流byte[]  
        /// </summary>  
        /// <param name="imagePath">图片路径</param>  
        /// <returns>返回的字节流</returns>  
        public static byte[] GetImageByte(string imagePath)
        {
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            fileStream.Seek(0, SeekOrigin.Begin);
            //创建文件长度缓冲区
            byte[] bytes = new byte[fileStream.Length];
            //读取文件
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            //释放文件读取流
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;
            return bytes;
        }
        #endregion  //...
    }
    /// <summary>
    /// 图片后缀
    /// </summary>
    public enum TextureSuffixs
    {
        /// <summary>
        /// 未知类型
        /// </summary>
        None,
        JPG,
        PNG,
        EXR
    }
}
