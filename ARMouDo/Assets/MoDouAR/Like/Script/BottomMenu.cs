/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using PlaceAR.LabelDatas;
using UnityEngine.UI;
using DG.Tweening;
using vPlace_zpc;
using ARKit_T;

namespace MoDouAR
{
    /// <summary>
    /// 下方选择模型菜单
    /// </summary>
    public class BottomMenu : Window<BottomMenu>
    {
        public override int ID
        {
            get
            {
                return 1;
            }
        }

        public override string Name
        {
            get
            {
                return "BottomMenu";
            }
        }

        public override string Path
        {
            get
            {
                return "UI/BottomMenu";
            }
        }
        public Button takePhoto;
        public GridAndLoop grid;
        /// <summary>
        /// 所有下载的数据
        /// </summary>
        private List<ChildItemData> childItem = new List<ChildItemData>();
        /// <summary>
        /// 所有模型选择的按钮
        /// </summary>
        private List<MainMenuButton> allButton = new List<MainMenuButton>();
        /// <summary>
        /// 当前模型图标
        /// </summary>
        private ChildItemData curModelData = null;

        private GameObject insObj;
        /// <summary>
        /// 模型
        /// </summary>
        public GameObject InsObj
        {
            get { return insObj; }
            set
            {
                insObj = value;
                if (insObj == null)
                    MoveDown();
                else
                    MoveUp();
            }
        }
        public override void Start()
        {
            base.Start();
            grid.onInitializeItem = UpdateItem;
            takePhoto.onClick.AddListener(TakePhotoP);
        }
        public override void Open()
        {
            childItem = LoadData.Instance.childItem;
            if (grid != null)
                grid.OnInit(childItem.Count);
            base.Open();
            if (InsObj)
            {
                BottomMenu_ModelWindow.Instance.Open();
            }
        }
        public void Close(bool c = false)
        {
            base.Close();
            if (InsObj && !c)
            {
                BottomMenu_ModelWindow.Instance.Close();
            }
        }
        /// <summary>
        /// 创建模型
        /// </summary>
        public void CreatModel(ChildItemData data)
        {
            curModelData = data;
            if (InsObj != null)
                Destroy(InsObj);
            AssetBundle.UnloadAllAssetBundles(true);
            string url = string.Format("{0}{1}/{2}/", Global.LocalUrl, data.item.catId, data.item.id);
            AssetBundle assetBundle = AssetBundle.LoadFromFile(url + FileTools.ReturnNmae(data.item.fileUrl));
            UnityEngine.Object[] obj = assetBundle.LoadAllAssets();
            InsObj = Instantiate(obj[0] as GameObject);
            AddView(InsObj.transform);
            InsObj.transform.position = ARKitControl.Instance.frame.position;
            LabelDataList d = null;
            if (FileTools.FileExist(url, FileTools.ReturnNmae(data.item.configFileUrl)))
            {
                d = FileTools.ReadText<LabelDataList>(url + FileTools.ReturnNmae(data.item.configFileUrl));
                InsObj.transform.eulerAngles = d.transform.Rotation;
                InsObj.transform.localScale = d.transform.Scale;
                Vector3 v = GetBetweenPoint(Global.camera.transform.position, ARKitControl.Instance.frame.position, d.transform.Position.z);
                InsObj.transform.position = v;
                //  InsObj.transform.position = new Vector3(ARKitControl.Instance.frame.position.x + d.transform.Position.x,
                // ARKitControl.Instance.frame.position.y + d.transform.Position.y,
                // ARKitControl.Instance.frame.position.z + d.transform.Position.z);
                ProjectConstDefine.labelDataList = d;
                ProjectConstDefine.hasConfig = true;
                if (!ModelIntroWindow.instance)
                    ModelIntroWindow.Instance.CreatWindow();
                ModelIntroWindow.Instance.ModelInfoUpdate(data.item.name, data.item.info);
            }
            else
            {
                Debug.Log("配置文件不存在: " + url + FileTools.ReturnNmae(data.item.configFileUrl));
                ProjectConstDefine.hasConfig = false;
            }
            if (Global.OperatorModel == OperatorMode.ARMode)
                ARKit_DetectionPanel.Instance.ARKit_ExempleMode(InsObj.transform);
            else
                ModelControl.GetInstance().LoadModel(InsObj, d, data);
            //ModelControl.GetInstance().OnInit(InsObj, d);
            //  if (Global.OperatorModel == OperatorMode.BrowserMode)
            //ModelControl.GetInstance().LoadModel(InsObj, d, data);
        }
        private Vector3 GetBetweenPoint(Vector3 start, Vector3 end, float distance)
        {
            Vector3 normal = (end - start).normalized;
            return normal * distance + end;
        }
        /// <summary>
        /// 添加检测是否在视野内的脚本
        /// </summary>
        /// <param name="tran"></param>
        private void AddView(Transform tran)
        {
            foreach (Transform item in tran)
            {
                if (item.GetComponent<MeshRenderer>() || item.GetComponent<SkinnedMeshRenderer>())
                {
                    item.gameObject.AddComponent<AtView>();

                }

                AddView(item);
            }
        }
        /// <summary>
        /// 滑动更新
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <param name="realIndex"></param>
        private void UpdateItem(GameObject item, int index, int realIndex)
        {
            item.GetComponent<MainMenuButton>().OnInit(childItem[realIndex]);
            allButton.Add(item.GetComponent<MainMenuButton>());
            //print(item.name);
        }
        /// <summary>
        /// 上移
        /// </summary>
        private void MoveUp()
        {
            Tweener tw = transform.DOLocalMoveY(88, 0.2f);
            tw.OnComplete(CompleteUp);
        }
        /// <summary>
        /// 还原
        /// </summary>
        public void MoveDown()
        {
            Tweener tw = transform.DOLocalMoveY(0, 0.2f);
            if (BottomMenu_ModelWindow.Instance != null)
                BottomMenu_ModelWindow.Instance.Close();
        }
        /// <summary>
        /// 上移完成回调
        /// </summary>
        private void CompleteUp()
        {
            BottomMenu_ModelWindow.Instance.CreatWindow();
            BottomMenu_ModelWindow.Instance.Open();
            BottomMenu_ModelWindow.Instance.ChangeTheCurModelImage(curModelData);
        }
        public void CloseAll()
        {
            for (int i = 0; i < allButton.Count; i++)
            {
                allButton[i].Close();
            }
            for (int i = 0; i < childItem.Count; i++)
            {
                childItem[i].option = false;
            }
        }
        /// <summary>
        /// 打开拍照
        /// </summary>
        private void TakePhotoP()
        {
            TakePhoto.Instance.CreatWindow();
            TakePhoto.Instance.Open();
            //Close();

        }
        /// <summary>
        /// 打开模型库按钮
        /// </summary>
        public void ModelLibraryButton()
        {
            ModelLibrary.Instance.CreatWindow();
            ModelLibrary.Instance.Open();
        }
    }
}