using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ModelViewerProject.Model
{
    using Label3D;
    using Animate;
    using System.Linq;
    using UI.Label;
    using DG.Tweening;
    using HTCVIVE;

    [RequireComponent(typeof(RotateHandler), typeof(ScaleHandler))]
    public class ModelController : MonoBehaviour
    {

        public event EventHandler OnInitModel;
        //public event EventHandler OnHome;

        public string textAssetName = "Labels";
        public string modelAssetName = "Skull_Mod";
        public GameObject model;
        private int nowLayer = 0;

        public void OnInit()
        {

            _home = transform.SaveLocal();

            var datas = Resources.Load<TextAsset>(textAssetName);
            var dataList = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(datas.text);

            var model = Resources.Load<GameObject>(modelAssetName);
            //www.assetBundle.Unload(false);
            var modelObj = GameObject.Instantiate(Resources.Load<GameObject>(modelAssetName));

            OnInit(modelObj, dataList);

        }

        public void OnInit(GameObject modelObj, LabelDataList labels)
        {

            home = new TransformData(transform.localPosition, Quaternion.identity, Vector3.one);
            var children = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();

            for (int i = 0; i < children.Count(); i++)
            {
                //Destroy ( children [ i ].gameObject );
            }
            model = modelObj;
           
            modelObj.transform.parent = transform;
            modelObj.transform.localEulerAngles = labels.transform.Rotation;
            modelObj.transform.localPosition = Vector3.zero;
            modelObj.transform.localScale = labels.transform.Scale;
            //modelObj.transform.localEulerAngles = new Vector3 ( 0, 180, 0 );
            transform.parent.GetComponent<VRTriggerControl>().parentPrefab = modelObj.transform;
            _label3Ds = modelObj.AddComponent<Label3DController>();
            _animate = modelObj.AddComponent<AnimationController>();

            materials = new List<MaterialHandler>();
            foreach (var render in modelObj.GetComponentsInChildren<MeshRenderer>())
            {
                var mat = render.gameObject.AddComponent<MaterialHandler>();
                materials.Add(mat);
            }

            Label3Ds.OnInit(labels.list);
            Animate.OnInit(labels.list);

            if (OnInitModel != null)
                OnInitModel(this, EventArgs.Empty);

            BackHome();
        }

        #region Material

        List<MaterialHandler> materials;

        public Material FadeMat;

        public void HideOthersButOne(Label3DHandler label, TweenCallback OnComplete)
        {
            faceToHandler.Rotate(label, OnComplete);

            Label3Ds.StartUpdatePosition();

            Label3Ds.HideOthersBut(label);

            label.gameObject.GetComponent<MaterialHandler>().OnUpdate();
            foreach (var lab in Label3Ds.labels.Where(l => l != label))
            {
                lab.gameObject.GetComponent<MaterialHandler>().OnUpdate(FadeMat);
            }

        }
        /// <summary>
        /// 逐层
        /// </summary>
        /// <param name="label"></param>
        /// <param name="isShow"></param>
        public void ByLayer(Label3DHandler label, bool isShow)
        {

            label.GetComponent<MaterialHandler>().OnUpdateByLayer(FadeMat, isShow);

        }
        /// <summary>
        /// 剖面模式
        /// </summary>
        public void ByProfile(Label3DHandler label, bool isShow)
        {
            label.GetComponent<MaterialHandler>().Profile(FadeMat, isShow);
        }
        /// <summary>
        /// 逐层点击单个
        /// </summary>
        /// <param name="label"></param>
        public void ByLayer(Label3DHandler label)
        {
            List<Label3DHandler> labels = GetComponentInChildren<Label3DController>().labels;
            foreach (Label3DHandler item in labels)
            {
                if (label.Layer < item.Layer)
                {

                    ByLayer(item, false);
                }
                else
                {
                    //item.GetComponent<MaterialHandler>().OnUpdateByLayer(FadeMat, true);
                    ByLayer(item, true);
                }
            }
            label.GetComponent<MaterialHandler>().OnUpdateByLayer(FadeMat);

        }
        /// <summary>
        /// 逐层单个点击
        /// </summary>
        /// <param name="label"></param>
        public void Profile(Label3DHandler label)
        {
            List<Label3DHandler> labels = GetComponentInChildren<Label3DController>().labels;
            foreach (Label3DHandler item in labels)
            {
                if (label.Layer < item.Layer)
                {

                    ByProfile(item, false);
                }
                else
                {
                    //item.GetComponent<MaterialHandler>().OnUpdateByLayer(FadeMat, true);
                    ByProfile(item, true);
                }
            }
            label.GetComponent<MaterialHandler>().OnUpdateByLayer(FadeMat);
        }
        /// <summary>
        /// 突出显示所属分组的子模型
        /// </summary>
        /// <param name="label"></param>
        /// <param name="label3DUI"></param>
        public void HideOthersBut(Label3DHandler label, GridItem label3DUI)
        {
            for (int i = 0; i < label3DUI.labelChild.Count; i++)
            {
                foreach (Label3DHandler item in Label3Ds.labels)
                {
                    if (item.Group == label3DUI.labelChild[i].label3D.Group)
                    {
                        item.GetComponent<MaterialHandler>().OnUpdate();
                    }
                    else
                    {
                        item.GetComponent<MaterialHandler>().OnUpdate(FadeMat);
                    }
                }
                //  label3DUI.labelChild[i].gameObject.GetComponent<MaterialHandler>().OnUpdate();
                //  print(label3DUI.labelChild[i]);
                //  foreach (var lab in Label3Ds.labels.Where(l => l != label3DUI.labelChild[i]))
                //  {

                //  lab.gameObject.GetComponent<MaterialHandler>().OnUpdate(FadeMat);
                //  }
            }
        }
        #endregion Material

        #region Label3D
        Label3DController _label3Ds;
        public Label3DController Label3Ds
        {
            get
            {
                if (_label3Ds == null)
                    _label3Ds = GetComponent<Label3DController>();
                if (_label3Ds == null)
                    _label3Ds = gameObject.AddComponent<Label3DController>();
                return _label3Ds;
            }
        }

        #endregion Label3D

        #region Fact to camera

        FaceToCameraHandler _faceToHandler;
        FaceToCameraHandler faceToHandler
        {
            get
            {
                if (_faceToHandler == null)
                    _faceToHandler = GetComponent<FaceToCameraHandler>();
                if (_faceToHandler == null)
                    _faceToHandler = gameObject.AddComponent<FaceToCameraHandler>();
                _faceToHandler.EndRotate += _faceToHandler_EndRotate;
                return _faceToHandler;
            }
        }

        private void _faceToHandler_EndRotate(object sender, EventArgs e)
        {
            Label3Ds.StopUpdatePosition();
        }

        public void FaceTo(Vector3 target)
        {
            // faceToHandler.Rotate ( target,null );
        }

        #endregion

        #region Animate

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
        /// 预设拆解/逐层显示
        /// </summary>
        public void MenuButtonDefault()
        {

            foreach (Transform child in model.transform)
            {
                if (nowLayer == child.GetComponent<Label3DHandler>().Layer)
                {
                    
                    ByLayer(child.GetComponent<Label3DHandler>(), true);
                    ShowInformation.showInformation.SetValue(child.GetComponent<Label3DHandler>().Title+"\n"+child.GetComponent<Label3DHandler>().Description);
                }
                if (nowLayer == model.transform.GetChildCount() - 1)
                {
                    ShowInformation.showInformation.Close();
                    ByLayer(child.GetComponent<Label3DHandler>(), false);
                }
            }

            nowLayer++;
            if (nowLayer == model.transform.GetChildCount())
                nowLayer = 0;

        }
        /// <summary>
        /// 剖面
        /// </summary>
        public void Profile()
        {
            foreach (Transform child in model.transform)
            {
                if (nowLayer == child.GetComponent<Label3DHandler>().Layer)
                {

                    ByProfile(child.GetComponent<Label3DHandler>(), true);
                    ShowInformation.showInformation.SetValue(child.GetComponent<Label3DHandler>().Title + "\n" + child.GetComponent<Label3DHandler>().Description);
                }
                if (nowLayer == model.transform.GetChildCount() - 1)
                {
                    ShowInformation.showInformation.Close();
                    ByProfile(child.GetComponent<Label3DHandler>(), false);
                }
            }

            nowLayer++;
            if (nowLayer == model.transform.GetChildCount())
                nowLayer = 0;
        }
        #endregion Animate

        #region Scale

        ScaleHandler _scaleHandler;
        ScaleHandler scaleHandler
        {
            get
            {
                if (_scaleHandler == null)
                    _scaleHandler = GetComponent<ScaleHandler>();
                if (_scaleHandler == null)
                    _scaleHandler = gameObject.AddComponent<ScaleHandler>();
                return _scaleHandler;
            }
        }

        public void StartScale(int dir)
        {
            scaleHandler.StartScale(dir);
        }

        public void StopScale()
        {
            scaleHandler.StopScale();
        }

        #endregion Scale

        #region Rotate

        RotateHandler _rotateHandler;
        RotateHandler rotateHandler
        {
            get
            {
                if (_rotateHandler == null)
                    _rotateHandler = GetComponent<RotateHandler>();
                if (_rotateHandler == null)
                    _rotateHandler = gameObject.AddComponent<RotateHandler>();
                return _rotateHandler;
            }
        }

        public void StartRotate(Vector2 dir, Transform tran)
        {
           // rotateHandler.StartRotate(dir, tran);
            rotateHandler.VrControlRotation(dir, tran);

           // Label3Ds.StartUpdatePosition();
            //Label3Ds.ResetAll ( );
        }
        public void StartTouchRotate(Vector2 dir)
        {
            rotateHandler.PrefabRotate(dir);

            Label3Ds.UpdatePosition();
            //Label3Ds.ResetAll ( );
        }
        public void StopRotate()
        {
            rotateHandler.StopRotate();

            Label3Ds.StopUpdatePosition();
        }

        #endregion Rotate



        #region Home
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
        /// 还原
        /// </summary>
        public void BackHome()
        {
            nowLayer = 0;
            model.transform.parent = transform;
            model.transform.DOLocalMove(Vector3.zero, 0.2f);
            model.transform.DORotate(Global.labelDataList.transform.Rotation, 0.2f);
            // model.transform.localPosition = Vector3.zero;
            //  model.transform.eulerAngles = Global.labelDataList.transform.Rotation;
            //transform.LoadLocal ( homeTransform );
            StartCoroutine(HandleOnHome());
            Label3Ds.ResetAll();
            Label3Ds.StartUpdatePosition();

            foreach (var lab in Label3Ds.labels)
            {
                lab.gameObject.GetComponent<MaterialHandler>().OnUpdate();
            }
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

            //if ( OnHome != null )
            //    OnHome ( this, EventArgs.Empty );

            Label3Ds.StopUpdatePosition();

            yield return null;
        }
        #endregion Home
    }
}
