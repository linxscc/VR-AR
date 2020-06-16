using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;


namespace MoDouAR
{
    public class ScreenShotFun
    {
        /// <summary>
        /// 截图  渲染→ Texture
        /// </summary>
        /// <param name="rect">区域 Rect(0, 0, Screen.width, Screen.height)</param>
        /// <param name="image">待渲染的图片</param>
        /// <param name="success">成功→回调</param>
        /// <param name="lost">失败→回调</param>
        /// <returns></returns>
        public void ScreenShotCapture(Rect rect, Image image, OnSuccessThandle success, OnLostThandle lost)
        {
            TCoroutine.Instance.TStartCoroutine(Capture(rect, image, success, lost));
        }
        /// <summary>
        /// 保存图片 → 到本地 并写入数据
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="success">成功→回调</param>
        /// <param name="lost">失败→回调</param>
        public void ScreenShotSave(OnSuccessThandle success = null, OnLostThandle lost = null)
        {
            try
            {
                ScreenShotTextureData textureData = new ScreenShotTextureData();
                string dateTime = DateTime.Now.ToString();
                dateTime = FileTexts.Replace(dateTime, "/", "d");
                dateTime = FileTexts.Replace(dateTime, ":", "t");
                string[] arr = dateTime.Split(' ');
                dateTime = arr[0] + "time" + arr[1];
                //判断时间是否已经存在 保证 只能保存一张图/秒
                if (ScreenShotCacheData.shotCachData != null && ScreenShotCacheData.shotCachData.dataPhoto != null && ScreenShotCacheData.shotCachData.dataPhoto.Count > 0)
                {
                    for (int i = 0; i < ScreenShotCacheData.shotCachData.dataPhoto.Count; i++)
                    {
                        string[] dayTime = ScreenShotCacheData.shotCachData.dataPhoto[i].textureName.Split('T');
                        if (dateTime == dayTime[1])
                        {
                            lost("一秒内多次保存,失败!");
                            return;
                        }
                    }
                }
                textureData.textureName = ScreenShotCacheData.ScreenShotName + "T" + dateTime;
                textureData.textureID = ScreenShotCacheData.ScreenShotID;
                textureData.texturePath = ScreenShotCacheData.GetScreenShotPath;
                textureData.textureSuffixs = TextureSuffixs.JPG;
                //读取历史数据 → 添加数据
                ScreenShotCacheData.shotCachData = ReadPhotoData();
                if (ScreenShotCacheData.shotCachData == null)
                    ScreenShotCacheData.shotCachData = new TextureData();
                ScreenShotCacheData.shotCachData.dataPhoto.Add(textureData);

                ComeBack.OnSaveLoadTextureSuccess += delegate
                 {
                     FileTexts.CreateFile(ScreenShotCacheData.GetScreenShotConfigPath, ScreenShotCacheData.GetScreenShotConfigName,
        JsonFx.Json.JsonWriter.Serialize(ScreenShotCacheData.shotCachData));
                     ComeBack.OnSaveLoadTextureSuccess -= delegate { };
                 };
                FileTexture.SaveTexture(ScreenShotCacheData.GetScreenShotPath, textureData.textureName
, ScreenShotCacheData.lastTexture, TextureSuffixs.JPG);
                success();
            }
            catch (Exception e)
            {
                lost(e.Message);
            }
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="id">图片id</param>
        /// <param name="suffixs">后缀</param>
        /// <param name="success">成功→回调</param>
        /// <param name="lost">失败→回调</param>
        public void DeletePhoto(int id, TextureSuffixs suffixs, OnSuccessThandle success, OnLostThandle lost)
        {
            TCoroutine.Instance.TStartCoroutine(Delete(id, suffixs, success, lost));
        }
        /// <summary>
        /// 刷新 相册
        /// </summary>
        /// <param name="faTra">父对象</param>
        /// <param name="success">成功→回调</param>
        /// <param name="lost">失败→回调</param>
        public void RefreshPhoto(Transform faTra, OnSuccessThandle success, OnLostThandle lost)
        {
            ScreenShotCacheData.shotCachData = ReadPhotoData();
            try
            {
                int maxCount = ScreenShotCacheData.shotCachData.dataPhoto.Count;
                TCoroutine.Instance.TStartCoroutine(CacheTexture(0, ScreenShotCacheData.cacheMaxCount));
                if (maxCount <= 0)
                {
                    lost("错误:   " + "没有图片信息! ");
                    lost("没有图片信息");
                }
                else
                {
                    SwitchGridManager switchGridManager = faTra.GetComponent<SwitchGridManager>();
                    switchGridManager.SetAmount(maxCount);
                    switchGridManager.touchDirection = TouchDirections;
                    switchGridManager.updateGridHandle = UpdateGrids;
                    success();
                }
            }
            catch (Exception e)
            {
                lost(e.Message);
            }
        }


        #region 私有方法
        private IEnumerator Capture(Rect rect, Image image, OnSuccessThandle success, OnLostThandle lost)
        {
            yield return new WaitForEndOfFrame();
            try
            {
                Texture2D texture = FileTexture.CaptureScreen(rect);
                image.sprite = texture.ToSprite();
                ScreenShotCacheData.lastTexture = texture;
                success();
            }
            catch (Exception e)
            {
                lost(e.Message);
            }
        }
        /// <summary>
        /// 读取相册数据
        /// </summary>
        /// <returns></returns>
        private TextureData ReadPhotoData()
        {
            TextureData data = new TextureData();
            data = FileTexts.ReadText<TextureData>
               (ScreenShotCacheData.GetScreenShotConfigPath + ScreenShotCacheData.GetScreenShotConfigName);
            return data;
        }
        /// <summary>
        /// 读取指定的ID范围 缓存图片
        /// </summary>
        /// <param name="minID">最小ID</param>
        /// <param name="maxID">最大ID</param>
        /// <returns></returns>
        private IEnumerator CacheTexture(int minID, int maxID)
        {
            if (ScreenShotCacheData.shotCachData != null)
            {
                bool go = true;
                if (minID < 0)
                    minID = 0;
                if (maxID > ScreenShotCacheData.shotCachData.dataPhoto.Count)
                    maxID = ScreenShotCacheData.shotCachData.dataPhoto.Count;
                int stID = minID;
                while (go)
                {
                    yield return new WaitForEndOfFrame();
                    if (minID < maxID)
                    {
                        if (!ScreenShotCacheData.shotCachTexture.ContainsKey(minID))
                        {
                            Texture2D texture = FileTexture.ReadTexture(ScreenShotCacheData.GetScreenShotPath,
                                ScreenShotCacheData.shotCachData.dataPhoto[minID].textureName, TextureSuffixs.JPG, Screen.width, Screen.height);
                            ScreenShotCacheData.shotCachTexture.Add(minID, texture);
                            //转换
                            //  ScreenShotCacheData.shotCachSprite.Add(minID, FileTexture.SpriteFromTex2D(texture));
                            //读取2   
                            Texture2D screenShot = FileTexture.ReadTexture2DTexture(texture);
                            if (!ScreenShotCacheData.shotCachThumbnails.ContainsKey(minID))
                                ScreenShotCacheData.shotCachThumbnails.Add(minID, screenShot);
                        }
                        minID += 1;
                    }
                    else
                    {
                        go = false;
                        //    RemoveCacheTexture(stID, maxID);
                    }
                }
            }
            else
            {
                Debug.Log("无图片缓存");
            }
        }
        /// <summary>
        /// 移除指定ID范围外 缓存图片
        /// </summary>
        /// <param name="minID">最小ID</param>
        /// <param name="MaxID">最大ID</param>
        /// <returns></returns>
        private void RemoveCacheTexture(int minID, int maxID)
        {
            if (ScreenShotCacheData.shotCachData != null)
            {
                for (int i = 0; i < ScreenShotCacheData.shotCachData.dataPhoto.Count; i++)
                {
                    if (ScreenShotCacheData.shotCachTexture.Count < ScreenShotCacheData.cacheMaxCount)
                        return;
                    int id = ScreenShotCacheData.shotCachData.dataPhoto[i].textureID;
                    if (id < minID || id > maxID)
                    {
                        if (ScreenShotCacheData.shotCachTexture.ContainsKey(id))
                            ScreenShotCacheData.shotCachTexture.Remove(id);
                        if (ScreenShotCacheData.shotCachThumbnails.ContainsKey(id))
                            ScreenShotCacheData.shotCachThumbnails.Remove(id);
                    }
                }
            }
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="id">图片id</param>
        /// <param name="suffixs">后缀</param>
        /// <param name="success">成功→回调</param>
        /// <param name="lost">失败→回调</param>
        private IEnumerator Delete(int id, TextureSuffixs suffixs, OnSuccessThandle success, OnLostThandle lost)
        {
            yield return new WaitForEndOfFrame();
            try
            {
                ScreenShotTextureData data = ScreenShotCacheData.shotCachData.dataPhoto[id];
                FileFolders.Delete(data.texturePath, data.textureName + "." + suffixs.ToString());

                for (int i = 0; i < ScreenShotCacheData.shotCachData.dataPhoto.Count; i++)
                {
                    if (i > id)
                    {
                        ScreenShotCacheData.shotCachData.dataPhoto[i - 1].textureName = ScreenShotCacheData.shotCachData.dataPhoto[i].textureName;
                    }
                    if (i == ScreenShotCacheData.shotCachData.dataPhoto.Count - 1)
                    {
                        ScreenShotCacheData.shotCachData.dataPhoto.Remove(ScreenShotCacheData.shotCachData.dataPhoto[i]);

                        FileTexts.CreateFile(ScreenShotCacheData.GetScreenShotConfigPath, ScreenShotCacheData.GetScreenShotConfigName,
                         JsonFx.Json.JsonWriter.Serialize(ScreenShotCacheData.shotCachData));
                        success();
                    }
                }
                //ScreenShotTextureData data = ScreenShotCacheData.shotCachData.dataPhoto[id];
                //FileFolders.Delete(data.texturePath, data.textureName + "." + suffixs.ToString());

                //int maxCount = ScreenShotCacheData.shotCachData.dataPhoto.Count;
                //ScreenShotCacheData.shotCachData.dataPhoto[id].textureName = ScreenShotCacheData.shotCachData.dataPhoto[maxCount - 1].textureName;
                //ScreenShotCacheData.shotCachData.dataPhoto.Remove(ScreenShotCacheData.shotCachData.dataPhoto[maxCount - 1]);
                //FileTexts.CreateFile(ScreenShotCacheData.GetScreenShotConfigPath, ScreenShotCacheData.GetScreenShotConfigName,
                //     JsonFx.Json.JsonWriter.Serialize(ScreenShotCacheData.shotCachData));
                //success();
                //return;
            }
            catch (Exception e)
            {
                lost(e.Message);
            }
        }


        // 回调
        /// <summary>
        /// 滑动方向
        /// </summary>
        private DragDirection dragDirection = DragDirection.None;
        private void TouchDirections(DragDirection drag, int currentMaxID)
        {
            dragDirection = drag;
        }
        private void UpdateGrids(int index, int currentMaxID, Transform trans)
        {
            ButtonPhotoOrVideo button = trans.GetComponent<ButtonPhotoOrVideo>();
            TCoroutine.Instance.TStartCoroutine(WaitUpdateTextureData(index, currentMaxID, button));
        }
        private IEnumerator WaitUpdateTextureData(int index, int currentMaxID, ButtonPhotoOrVideo button)
        {
            int ttID = ScreenShotCacheData.shotCachThumbnails.Values.Count;
            if (currentMaxID == ttID)
            {
                ////刷新缓存
                switch (dragDirection)
                {
                    case DragDirection.上:
                        TCoroutine.Instance.TStartCoroutine(CacheTexture(ttID, ttID + (ScreenShotCacheData.cacheMaxCount - 18)));
                        break;
                    case DragDirection.下:
                        TCoroutine.Instance.TStartCoroutine(CacheTexture(ttID - 18 - (ScreenShotCacheData.cacheMaxCount - 18), ttID - 18));
                        break;
                }
            }
            if (!ScreenShotCacheData.shotCachThumbnails.ContainsKey(index))
            {
                yield return new WaitUntil(() => ScreenShotCacheData.shotCachThumbnails.ContainsKey(index));
            }
            button.imagePhoto.texture = ScreenShotCacheData.shotCachThumbnails[index];
            button.textureId = index;
            button.transform.GetComponent<Button>().onClick.AddListener(delegate
            {
                ComeBack.OnButtonPhotoOrVideos(button.gameObject);
            });
        }
        #endregion  //...
    }
}
