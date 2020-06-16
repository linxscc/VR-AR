using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlaceAR;
using PlaceAR.LabelDatas;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;
using ARKit_T;
using XYRF_FingerGesture;

namespace vPlace_zpc
{
    public class ModelControl : MonoBehaviour
    {
        /// <summary>
        /// 模型数据
        /// </summary>
        public MoDouAR.ChildItemData data;
        public event EventHandler OnInitModel;
        public GameObject model = null;
        /// <summary>
        /// 模型父级
        /// </summary>
        public GameObject modelParent = null;
        /// <summary>
        /// 透明材质
        /// </summary>
        public Material FadeMat;
        /// <summary>
        /// 阴影面板
        /// </summary>
        public Transform shadowGround = null;
        /// <summary>
        /// 自身不带光照的模型所用灯光
        /// </summary>
        public Light modelLight = null;
        /// <summary>
        /// 截面模式所用需隐藏的子模型
        /// </summary>
        public List<PrefabChildControl> modelChildList = new List<PrefabChildControl>();

        private List<MaterialHandler> materials;
        private int nowLayer = 0;
        /// <summary>
        /// 是否为截面模式
        /// </summary>
        private bool isSectionMode = true;


        private static ModelControl instance = null;
        public static ModelControl GetInstance()
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("ModelControll");
                instance = obj.AddComponent<ModelControl>();
            }
            return instance;
        }

        private void Awake()
        {
            if (instance != null)
            {
                if (instance != this)
                    Destroy(this);
            }
            else
                instance = this;
        }
       
        /// <summary>
        /// 模型初始化
        /// </summary>
        /// <param name="modelObj">生成的模型</param>
        /// <param name="labelList">数据链表</param>
        public void OnInit(GameObject modelObj, LabelDataList labelList)
        {
            //home = new TransformData(transform.localPosition, Quaternion.identity, Vector3.one);

            model = modelObj;
            model.transform.SetParent(transform);
            model.transform.localEulerAngles = labelList.transform.Rotation;
            model.transform.localPosition = labelList.transform.Position;
            model.transform.localScale = labelList.transform.Scale;

            _label3Ds = model.AddComponent<PrefabControl>();
            _animate = model.AddComponent<AnimationController>();

            WholeModelInit(labelList);

            Label3Ds.OnInit(labelList.list);
            Animate.OnInit(labelList.list);

            if (OnInitModel != null)
                OnInitModel(this, EventArgs.Empty);

            BackHome();
        }

        /// <summary>
        /// 整体模型特殊处理化
        /// </summary>
        private void WholeModelInit(LabelDataList labelList)
        {
            if (labelList.controlType != 4)
            {
                materials = new List<MaterialHandler>();
                foreach (var render in model.GetComponentsInChildren<MeshRenderer>())
                {
                    var mat = render.gameObject.AddComponent<MaterialHandler>();
                    materials.Add(mat);
                }
            }
        }

        /// <summary>
        /// 截面模型特殊处理化
        /// </summary>
        private void SectionModelInit(LabelDataList labelList)
        {
            if (labelList.controlType == 6)
            {
                List<PrefabChildControl> labels = GetComponentInChildren<PrefabControl>().childData;
                foreach (var lab in labels)
                {
                    if (lab.isHide)
                        lab.gameObject.SetActive(false);
                }
            }
        }

        #region Material 各浏览模式逻辑


        /// <summary>
        /// 突出显示模型所属分组-拆解模式
        /// </summary>
        /// <param name="label">子级信息的存储</param>
        /// <param name="OnComplete">动画</param>
        public void HideOthersButOne(PrefabChildControl label, TweenCallback OnComplete)
        {
            faceToHandler.Rotate(label, OnComplete);
            if (label.gameObject.GetComponent<MaterialHandler>() != null)
                label.gameObject.GetComponent<MaterialHandler>().OnUpdate();
            foreach (var lab in Label3Ds.childData.Where(l => l != label))
            {
                if (lab.gameObject.GetComponent<MaterialHandler>() != null)
                    lab.gameObject.GetComponent<MaterialHandler>().OnUpdate(FadeMat);
            }
        }
        /// <summary>
        /// 突出显示模型所属分组-高亮模式
        /// </summary>
        /// <param name="label">子级信息的存储</param>
        public void HideOthersButOne(PrefabChildControl label)
        {
            HighlightShowModel(label);
            foreach (var lab in Label3Ds.childData.Where(l => l != label))
            {
                HighlightShowModel(lab, FadeMat);
            }
        }
        /// <summary>
        /// 突出显示所属分组的子模型
        /// </summary>
        /// <param name="label">子级信息的存储</param>
        /// <param name="label3DUI"></param>
        public void HideOthersBut(PrefabChildControl label, BrowserTypeItem label3DUI)
        {
            for (int i = 0; i < label3DUI.labelChild.Count; i++)
            {
                foreach (PrefabChildControl item in Label3Ds.childData)
                {
                    if (item.Group == label3DUI.labelChild[i].labelData.Group)
                    {
                        item.GetComponent<MaterialHandler>().OnUpdate();
                    }
                    else
                    {
                        item.GetComponent<MaterialHandler>().OnUpdate(FadeMat);
                    }
                }
            }
        }

        /// <summary>
        /// 逐层点击单个
        /// </summary>
        /// <param name="label">子级信息的存储</param>
        public void Layer(PrefabChildControl label)
        {
            List<PrefabChildControl> labels = GetComponentInChildren<PrefabControl>().childData;
            foreach (PrefabChildControl item in labels)
            {
                if (label.Layer < item.Layer)
                {
                    ByLayer(item, false);
                }
                else
                {
                    ByLayer(item, true);
                }
            }
            label.GetComponent<MaterialHandler>().OnUpdateByLayer(FadeMat);

        }
        /// <summary>
        /// 逐层
        /// </summary>
        /// <param name="label">子级信息的存储</param>
        /// <param name="isShow">是否显示</param>
        public void ByLayer(PrefabChildControl label, bool isShow)
        {
            label.GetComponent<MaterialHandler>().OnUpdateByLayer(FadeMat, isShow);
        }

        /// <summary>
        /// 剖面单个点击
        /// </summary>
        /// <param name="label">子级信息的存储</param>
        public void Profile(PrefabChildControl label)
        {
            List<PrefabChildControl> labels = GetComponentInChildren<PrefabControl>().childData;
            foreach (PrefabChildControl item in labels)
            {
                if (label.Layer < item.Layer)
                {
                    ByProfile(item, false);
                }
                else
                {
                    ByProfile(item, true);
                }
            }
            label.GetComponent<MaterialHandler>().OnUpdateByLayer(FadeMat);
        }

        /// <summary>
        /// 剖面
        /// </summary>
        /// <param name="label">子级信息的存储</param>
        /// <param name="isShow">是否显示</param>
        public void ByProfile(PrefabChildControl label, bool isShow)
        {
            label.GetComponent<MaterialHandler>().Profile(FadeMat, isShow);
        }

        #endregion Material

        #region Label3D
        PrefabControl _label3Ds;
        public PrefabControl Label3Ds
        {
            get
            {
                if (_label3Ds == null)
                    _label3Ds = GetComponent<PrefabControl>();
                if (_label3Ds == null)
                    _label3Ds = gameObject.AddComponent<PrefabControl>();
                return _label3Ds;
            }
        }

        #endregion Label3D

        #region Face to camera

        FaceToCameraHandler _faceToHandler;
        FaceToCameraHandler faceToHandler
        {
            get
            {
                if (_faceToHandler == null)
                    _faceToHandler = GetComponent<FaceToCameraHandler>();
                if (_faceToHandler == null)
                    _faceToHandler = gameObject.AddComponent<FaceToCameraHandler>();
                return _faceToHandler;
            }
        }

        #endregion

        #region Animate 各浏览模式逻辑
        AnimationController _animate;
        AnimationController Animate
        {
            get
            {
                if (_animate == null)
                    _animate = GetComponent<AnimationController>();
                if (_animate == null)
                    _animate = gameObject.AddComponent<AnimationController>();
                return _animate;
            }
        }

        /// <summary>
        /// 合
        /// </summary>
        public void OnAssemble()
        {
            Animate.OnAssemble();
            //abel3Ds.FadeOut ( );
        }
        /// <summary>
        /// 拆
        /// </summary>
        public void OnDisassemble()
        {
            Animate.OnDisassemble();
            //Label3Ds.FadeIn ( );
        }
        /// <summary>
        /// 逐层显示
        /// </summary>
        public void Layer()
        {
            foreach (Transform child in model.transform)
            {
                if (nowLayer == child.GetComponent<PrefabChildControl>().Layer)
                {

                    ByLayer(child.GetComponent<PrefabChildControl>(), true);
                    //ShowInformation.showInformation.SetValue(child.GetComponent<PrefabChildControl>().Title + "\n" + child.GetComponent<Label3DHandler>().Description);
                }
                if (nowLayer == model.transform.childCount - 1)
                {
                    //ShowInformation.showInformation.Close();
                    ByLayer(child.GetComponent<PrefabChildControl>(), false);
                }
            }
            nowLayer++;
            if (nowLayer == model.transform.childCount)
                nowLayer = 0;

        }
        /// <summary>
        /// 剖面
        /// </summary>
        public void Profile()
        {
            foreach (Transform child in model.transform)
            {
                if (nowLayer == child.GetComponent<PrefabChildControl>().Layer)
                {

                    ByProfile(child.GetComponent<PrefabChildControl>(), true);
                    //ShowInformation.showInformation.SetValue(child.GetComponent<Label3DHandler>().Title + "\n" + child.GetComponent<Label3DHandler>().Description);
                }
                if (nowLayer == model.transform.childCount - 1)
                {
                    //ShowInformation.showInformation.Close();
                    ByProfile(child.GetComponent<PrefabChildControl>(), false);
                }
            }

            nowLayer++;
            if (nowLayer == model.transform.childCount)
                nowLayer = 0;
        }
        /// <summary>
        /// 截面模式
        /// </summary>
        public void Section()
        {
            //if (modelChildList != null)
            //    modelChildList.Clear();
            if (modelChildList.Count == 0)
                modelChildList = GetComponentInChildren<PrefabControl>().childData;

            if (isSectionMode)
            {
                model.transform.GetChild(model.transform.childCount - 1).gameObject.SetActive(false);
                foreach (var lab in modelChildList)
                {
                    if (lab.isHide)
                        lab.gameObject.SetActive(true);
                }
            }
            else
            {
                model.transform.GetChild(model.transform.childCount - 1).gameObject.SetActive(true);
                foreach (var lab in modelChildList)
                {
                    if (!lab.isHide)
                        lab.gameObject.SetActive(true);
                    else
                        lab.gameObject.SetActive(false);
                }
            }
            isSectionMode = !isSectionMode;
        }
        #endregion Animate

        #region Home 复位逻辑
        TransformData _home;
        TransformData home
        {
            get
            {
                if (_home == null)
                {
                    _home = TransformData.identity;
                }
                return _home;
            }
            set
            {
                _home = value;
            }
        }

        float _duration = 0.5f;

        /// <summary>
        /// 还原复位
        /// </summary>
        public void BackHome()
        {
            CameraReset();
            nowLayer = 0;
            //model.transform.parent = transform;
            model.transform.DOLocalMove(ProjectConstDefine.labelDataList.transform.Position, .5f).SetEase(Ease.OutQuart);
            model.transform.DORotate(ProjectConstDefine.labelDataList.transform.Rotation, .5f).SetEase(Ease.OutQuart);
            model.transform.DOScale(ProjectConstDefine.labelDataList.transform.Scale, .5f);
            //模型父级归零，重置
            transform.DOLocalMove(Vector3.zero, .5f);
            //StartCoroutine(HandleOnHome());
            MaterialReset();
            SectionModeBackHome();
        }

        /// <summary>
        /// 透明材质还原
        /// </summary>
        public void MaterialReset()
        {
            foreach (var lab in Label3Ds.childData)
            {
                HighlightShowModel(lab);
            }
        }

        /// <summary>
        /// 界面模式的特殊复位
        /// </summary>
        public void SectionModeBackHome()
        {
            SectionModelInit(ProjectConstDefine.labelDataList);
            if (ProjectConstDefine.labelDataList.controlType == 6)
                model.transform.GetChild(model.transform.childCount - 1).gameObject.SetActive(true);
            //List<GameObject> hideObjList = BrowserTypeViewContol.Instance.browserTypeView.labelUIs.hideObjList;
            //for (int i = 0; i < hideObjList.Count; ++i)
            //{
            //    hideObjList[i].SetActive(false);
            //}
            //isSectionMode = true;
        }

        IEnumerator HandleOnHome()
        {
            float lastTime = 0f;
            TransformData from = transform.SaveLocal();

            while (lastTime < _duration)
            {
                lastTime += Time.deltaTime;

                var factor = lastTime / _duration;
                if (factor > 1.0F) factor = 1.0F;
                var current = TransformData.Lerp(from, home, factor);
                transform.LoadLocal(current);

                yield return new WaitForFixedUpdate();

            }
            yield return null;
        }
        #endregion Home

        /// <summary>
        /// 载入模型
        /// </summary>
        /// <param name="obj">模型克隆件</param>
        /// <param name="name">模型名称</param>
        public void LoadModel(GameObject obj, LabelDataList labelDataList, MoDouAR.ChildItemData data)
        {
            this.data = data;
            try
            {
                if (model != null && !obj.Equals(model))
                    Destroy(model);
                modelChildList.Clear();

                CameraReset();

                if (ProjectConstDefine.hasConfig)
                    OnInit(obj, labelDataList);
                else
                    obj.transform.SetParent(transform);

                modelParent.transform.localPosition = model.transform.localPosition;

                if (!obj.GetComponent<ARKit_Input>())
                    obj.AddComponent<ARKit_Input>().UpdataData();
                else
                    obj.GetComponent<ARKit_Input>().UpdataData();

                if (!obj.GetComponent<OnMouseEvent_T>())
                    obj.AddComponent<OnMouseEvent_T>();

                if (!Global.currentSelectObjectFather)
                    Global.currentSelectObjectFather = obj;

                LightJudge(obj);

                SetShadowGround(obj, labelDataList);
            }
            catch (System.Exception error)
            {
                throw error;
            }
        }

        void HandleOnInitModel(GameObject modelObj, LabelDataList labels)
        {
            if (!this)
                Debug.Log("Model Controller is null!");
            OnInit(modelObj, labels);
        }

        /// <summary>
        /// 清空模型只保留最新生成的
        /// </summary>
        public void ClearChildCount()
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// 模型浏览进入AR界面
        /// </summary>
        public void EnterAR()
        {
            if (model != null)
                ARKit_DetectionPanel.Instance.ARKit_ModelBrowserToARScene(model.transform);
            shadowGround.gameObject.SetActive(false);
        }

        /// <summary>
        /// 高亮显示及隐藏
        /// </summary>
        /// <param name="lab">子级信息的存储</param>
        /// <param name="mat">透明材质</param>
        private void HighlightShowModel(PrefabChildControl lab, Material mat = null)
        {
            if (lab.gameObject.GetComponent<MaterialHandler>() != null)
                lab.gameObject.GetComponent<MaterialHandler>().OnUpdate(mat);
            for (int i = 0; i < lab.transform.childCount; ++i)
            {
                if (lab.transform.GetChild(i).GetComponent<MaterialHandler>() != null)
                    lab.transform.GetChild(i).GetComponent<MaterialHandler>().OnUpdate(mat);
            }
        }

        /// <summary>
        /// 灯光判断
        /// </summary>
        /// <param name="obj">当前载入模型</param>
        public void LightJudge(GameObject obj)
        {
            if (obj.layer != 9)
                modelLight.color = Color.white;
            else
                modelLight.color = Color.black;
        }

        /// <summary>
        /// 阴影面板设置
        /// </summary>
        /// <param name="obj">父级</param>
        public void SetShadowGround(GameObject obj, LabelDataList labelDataList)
        {
            if (!shadowGround)
            {
                shadowGround = Resources.Load<GameObject>(Global.planeShadowYZJname).transform;
                shadowGround = Instantiate(shadowGround);
            }
            else
            {
                if (labelDataList.controlType == 5)
                    shadowGround.gameObject.SetActive(false);
                else
                    shadowGround.gameObject.SetActive(true);
            }

            shadowGround.SetParent(obj.transform);
            shadowGround.SetAsFirstSibling();
            shadowGround.localScale = new Vector3(40, 1, 40);
            shadowGround.localPosition = Vector3.zero - new Vector3(0, (obj.transform.localScale.x / 100) * 1, 0);

            //从AR退回浏览界面，相机重置
            CameraReset();
        }

        /// <summary>
        /// 每次重新载入模型时-相机重置
        /// </summary>
        public void CameraReset()
        {
            if (GetComponentInChildren<ARKit_Input>())
            {
                Sequence sequence = DOTween.Sequence();
                Tweener tweener1 = Global.camera.transform.DOLocalMove(Vector3.zero, .15f);
                Tweener tweener2 = Global.camera.transform.DOLocalRotateQuaternion(Quaternion.identity, .15f);

                sequence.Append(tweener2);
                sequence.Join(tweener1);
                //tweener.OnComplete(() => GetComponentInChildren<ARKit_Input>().Init());
                sequence.OnComplete(() => GetComponentInChildren<ARKit_Input>().Init());
                //GetComponentInChildren<ARKit_Input>().Init();//相机初始化
            }
        }

        /// <summary>
        /// 摧毁当前载入模型
        /// </summary>
        public void DestroyModel()
        {
            Destroy(model);
        }
    }
}