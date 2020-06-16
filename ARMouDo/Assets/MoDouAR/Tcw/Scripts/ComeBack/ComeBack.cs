using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MoDouAR
{
    public class ComeBack
    {
        #region PanelGrid回调
        /// <summary>
        /// 页面改变回调
        /// </summary>
        static public event Action<PanelGridResult> OnEnablGridChange = delegate { };
        static public void OnEnablGridChanges(int 页码, TonStart tonStart)
        {
            PanelGridResult panelGridResult = new PanelGridResult(页码, tonStart);
            OnEnablGridChange(panelGridResult);
        }

        #endregion  //...

        #region FileTexture回调
        static public event Action<FileTextureResult> OnSaveLoadTextureSuccess = delegate { };
        static public event Action<FileTextureResult> OnSaveLoadTextureLost = delegate { };
        static public void OnSaveLoadTextureSuccesss(FileTextureState state)
        {
            FileTextureResult fileTextureResult = new FileTextureResult(state);
            OnSaveLoadTextureSuccess(fileTextureResult);
        }
        static public void OnSaveLoadTextureSuccesss(FileTextureState state, double startTime)
        {
            FileTextureResult fileTextureResult = new FileTextureResult(state, startTime);
            OnSaveLoadTextureSuccess(fileTextureResult);
        }
        static public void OnSaveLoadTextureLosts(FileTextureState state, string message)
        {
            FileTextureResult fileTextureResult = new FileTextureResult(state, message);
            OnSaveLoadTextureLost(fileTextureResult);
        }


        #endregion  //...

        #region PanelShowPhoto
        /// <summary>
        /// 点击相片/Video
        /// </summary>
        static public event Action<PanelShowPhotoOrVideoResult> OnButtonPhotoOrVideo = delegate { };
        static public void OnButtonPhotoOrVideos(GameObject buttonGameObject)
        {
            PanelShowPhotoOrVideoResult panelShowPhotoOrVideoResult = new PanelShowPhotoOrVideoResult(buttonGameObject);
            OnButtonPhotoOrVideo(panelShowPhotoOrVideoResult);
        }
        #endregion  //...
    }
}
