using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MoDouAR
{
    public class ComeBackResult { }


    /// <summary>
    /// 滑动回调结果
    /// </summary>
    public class PanelGridResult
    {
        protected int pageNumber = 0;
        protected TonStart toStart = 0;
        public PanelGridResult(int PageNumber, TonStart toStarts)
        {
            pageNumber = PageNumber;
            toStart = toStarts;
        }

        /// <summary>
        /// 得到当前页码
        /// </summary>
        public int GetPageNumber
        {
            get
            {
                return pageNumber;
            }
        }
        /// <summary>
        /// 得到回调时间
        /// </summary>
        public TonStart GetTonStart
        {
            get
            {
                return toStart;
            }
        }
    }



    /// <summary>
    /// 图片保存/读取 回调
    /// </summary>
    public class FileTextureResult
    {
        protected FileTextureState state = FileTextureState.None;
        protected string message = "";
        protected double startTime;
        public FileTextureResult(FileTextureState state)
        {
            this.state = state;
        }
        public FileTextureResult(FileTextureState state, double startTime)
        {
            this.state = state;
            this.startTime = startTime;
        }
        public FileTextureResult(FileTextureState state, string message)
        {
            this.state = state;
            this.message = message;
        }

        /// <summary>
        /// 保存/读取/? 状态
        /// </summary>
        public FileTextureState GetState
        {
            get
            {
                return state;
            }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string GetMessage
        {
            get
            {
                return message;
            }
        }
        /// <summary>
        /// 加载用时
        /// </summary>
        public double GetLoadTime
        {
            get
            {
                return startTime;
            }
        }
    }
    /// <summary>
    /// 保存/读取图片 状态
    /// </summary>
    public enum FileTextureState
    {
        /// <summary>
        /// 无任何操作
        /// </summary>
        None,
        /// <summary>
        /// 保存成功
        /// </summary>
        SaveSuccess,
        /// <summary>
        /// 保存失败
        /// </summary>
        SaveLost,
        /// <summary>
        /// 加载成功 单个
        /// </summary>
        LoadSucessSingle,
        /// <summary>
        /// 加载失败 单个
        /// </summary>
        LoadLostSingle,
        /// <summary>
        /// 加载成功 ALL
        /// </summary>
        LoadSucessAll,
        /// <summary>
        /// 加载失败 ALL
        /// </summary>
        LoadLostAll,
        /// <summary>
        /// 读取成功 图片纹理
        /// </summary>
        ReadTextureSucess,
        /// <summary>
        /// 读取失败 图片纹理
        /// </summary>
        ReadTextureLost
    }



    /// <summary>
    /// 点击相片/Video回调
    /// </summary>
    public class PanelShowPhotoOrVideoResult
    {
        protected GameObject buttonGameObject;
        public PanelShowPhotoOrVideoResult(GameObject buttonGameObject)
        {
            this.buttonGameObject = buttonGameObject;
        }
        public GameObject GetButtonGameObject
        {
            get
            {
                return buttonGameObject;
            }
        }
    }


    #region 公共
    /// <summary>
    /// 回调Time
    /// </summary>
    public enum TonStart
    {
        /// <summary>
        /// 开始时
        /// </summary>
        Start,
        /// <summary>
        /// 进行时
        /// </summary>
        Centering,
        /// <summary>
        /// 结束时
        /// </summary>
        End
    }
    #endregion //结束...
}
