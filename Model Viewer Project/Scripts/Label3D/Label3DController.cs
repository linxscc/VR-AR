using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

namespace ModelViewerProject.Label3D
{
    public class Label3DController : MonoBehaviour
    {
        public string myName;
        public AnimaiomType animaiomType;
        public ControlType controlType;
        public List<Label3DHandler> labels { set; get; }
      
        public void OnInit(List<LabelData> datas)
        {
            if (labels == null)
                labels = new List<Label3DHandler>();
            else
                labels.Clear();
            
            var children = transform.GetComponentsInChildren<Transform>();

            foreach (var data in datas)
            {
                var child = children.FirstOrDefault(t => t.name == data.name);
                if (child)
                {

                    var label = child.GetComponent<Label3DHandler>();
                    if (label == null)
                        label = child.gameObject.AddComponent<Label3DHandler>();

                    label.OnInit(data);

                    labels.Add(label);
                }
            }
        }




        #region Fade In and Out

        bool isAllFadeIn = false;

        public void FadeIn()
        {
            foreach (var label in labels)
            {
                label.FadeIn();
            }


            isAllFadeIn = true;


        }

        public void FadeOut()
        {
            foreach (var label in labels)
            {
                label.FadeOut();
            }

            isAllFadeIn = false;
        }

        public void HideOthersBut(Label3DHandler label3D)
        {

            var needsFadeOut = labels.Where(l => l != label3D);
            foreach (var label in needsFadeOut)
            {
                label.FadeOut();
            }
            label3D.FadeIn();
        }

        public void ResetAll()
        {
            if (isAllFadeIn)
                FadeIn();
            else
                FadeOut();

        }

        #endregion Fade In and Out

        #region Update Position

        bool isStatic = true;

        public void StartUpdatePosition()
        {
            StartCoroutine(HandleOnUpdatePosition());
        }
        public void UpdatePosition()
        {
            foreach (var label in labels)
            {
                label.UpdatePosition();
            }
        }
        public void StopUpdatePosition()
        {
            isStatic = true;
        }

        IEnumerator HandleOnUpdatePosition()
        {
            isStatic = false;

            while (!isStatic)
            {
                foreach (var label in labels)
                {
                    label.UpdatePosition();
                }
                yield return new WaitForFixedUpdate();
            }

            yield return null;

        }
        #endregion Update Position

        #region Editor
        public LabelDataList DataList
        {
            get
            {
                var datas = labels.Select(l => new LabelData()
                {
                    title = l.Title,
                    description = l.Description,
                    name = l.Name,
                    localPosition = l.LocalPosition,
                    initialPosition = l.InitialPosition,
                    group = l.Group
                }).ToList();
                return new LabelDataList() { transform = transform.Serialization(myName), animationType = (int)animaiomType,controlType=(int)controlType, list = datas };
            }
        }

        //Init by Editor Window
        public void OnInit(Transform modRoot, string myName,AnimaiomType type,ControlType conType)
        {
            this.myName = myName;
            animaiomType = type;
            controlType = conType;
            if (labels == null)
                labels = new List<Label3DHandler>();
            else
                labels.Clear();

            foreach (var child in modRoot.GetComponentsInChildren<MeshFilter>())
            {
                var position = child.sharedMesh.vertices.Bounds().center;

                //var label = LabelPrefab.Spawn(position);
                var label = child.GetComponent<Label3DHandler>();
                if (label == null)
                    label = child.gameObject.AddComponent<Label3DHandler>();

                label.OnInit(child);

                labels.Add(label);

            }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="modRoot"></param>
        /// <param name="myName"></param>
        public void MyOnInit(Transform modRoot, string myName)
        {
            this.myName = myName;
            if (labels == null)
                labels = new List<Label3DHandler>();
            else
                labels.Clear();

            foreach (Transform child in modRoot)
            {
                //var position = child.sharedMesh.vertices.Bounds().center;

                //var label = LabelPrefab.Spawn(position);
                var label = child.GetComponent<Label3DHandler>();
                if (label == null)
                    label = child.gameObject.AddComponent<Label3DHandler>();

                label.OnInitLater(child);

                labels.Add(label);

            }
        }
        public void OnUpdate(List<LabelData> datas)
        {

            foreach (var label in labels)
            {
                var data = datas.FirstOrDefault(d => d.name == label.Name);
                if (data != null)
                    label.OnInit(data, 1f);
            }


        }
        /// <summary>
        /// 编辑下加载已经有的数据
        /// </summary>
        /// <param name="datas"></param>
        public void OnUpdate(List<LabelData> datas, bool isClose)
        {

            foreach (var label in labels)
            {
                var data = datas.FirstOrDefault(d => d.name == label.Name);
                if (data != null)
                    label.OnInit(data, isClose, 1f);
            }


        }
        #endregion Editor

    }
}
