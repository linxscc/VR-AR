using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ModelViewerProject.Label3D
{
    [RequireComponent(typeof(MeshCollider))]
    public class Label3DHandler : MonoBehaviour
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// 详细信息
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 逐层模式中所属的层级
        /// </summary>
        public float Layer { set; get; }
        /// <summary>
        /// 预设分解位置
        /// </summary>

        public Vector3 LocalPosition { set; get; }
        /// <summary>
        /// 初始位置
        /// </summary>
        public Vector3 InitialPosition { set; get; }
        /// <summary>
        /// 所属组
        /// </summary>
        public string Group { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 模型名称
        /// </summary>
        public string LocalName { set; get; }
        public TextMesh textMesh;

        // public BoxCollider boxCollider;
        // public List<Label3DHandler> labels { set; get; }
        public Vector3 Position
        {
            get
            {

                return transform.TransformPoint(LocalPosition);
            }
        }

        public void RenderText()
        {
            if (textMesh == null)
            {
                textMesh = GetComponentInChildren<TextMesh>();
                if (textMesh == null)
                {

                    var obj = new GameObject();
                    obj.transform.parent = transform;
                    obj.transform.localPosition = LocalPosition;
                    obj.transform.localScale = new Vector3(0.0025f, 0.0025f, 1f);
                    textMesh = obj.AddComponent<TextMesh>();
                    textMesh.transform.localPosition = Vector3.zero;
                    if (transform.parent.name == "001_IRONMAN")
                        textMesh.characterSize = 1f;
                    else if (transform.parent.name == "Skull_Mod")
                        textMesh.characterSize = 17f;
                    else if (transform.parent.name == "AIRCRAFT")
                        textMesh.characterSize = 43;
                    else if (transform.parent.name == "vPlace_model_001_MF")
                        textMesh.characterSize = 450;

                }
            }
            //textMesh.text = string.Format("<size=32><b>{0}</b></size>\n<size=24>{1}</size>", Title, Description);
            textMesh.text = string.Format("<size=32><b>{0}</b></size>", Title);
        }
        public void OnInit(LabelData data, bool isClose, float alpha = 0)
        {
            gameObject.layer = LayerMask.NameToLayer("model");
            transform.tag = Tag.prefab;
            //transform.localPosition = data.localPosition;
            LocalPosition = data.localPosition;

            InitialPosition = data.initialPosition;
            if (isClose)
                transform.localPosition = LocalPosition;
            else
                transform.localPosition = InitialPosition;
            //  print(LocalPosition+"=="+ InitialPosition);
            Name = data.name;
            Title = data.title;
            Description = data.description;
            Layer = data.layer;
            Group = data.group;
            RenderText();

            textMesh.color = new Color(1, 1, 1, alpha);


            //boxCollider = gameObject.AddComponent<BoxCollider> ( );

        }
        public void OnInit(MeshFilter child)
        {
            //transform.parent = child.transform;
            LocalPosition = transform.localPosition;
            //Name = child.name;
            //Title = child.name;
            //Description = "详细说明";

            //RenderText ( );
            //textMesh.color = new Color ( 1, 1, 1, 1 );


        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="child"></param>
        public void OnInitLater(Transform modRoot)
        {
            InitialPosition = transform.localPosition;
            // LocalPosition = transform.position;
            Name = modRoot.name;
            Title = modRoot.name;
            Description = "详细说明";
            Group = "分组情况";
            //if (transform.childCount == 0) return;
            //if (labels == null)
            //    labels = new List<Label3DHandler>();
            //else
            //    labels.Clear();
            //foreach (Transform child in transform)
            //{
            //    Label3DHandler label = child.GetComponent<Label3DHandler>();
            //    if (label == null)
            //        label = child.gameObject.AddComponent<Label3DHandler>();
            //    label.OnInitLater(child);
            //    labels.Add(label);
            //}

            // RenderText();
            //textMesh.color = new Color(1, 1, 1, 1);
        }
        public void OnInit(LabelData data, float alpha = 0)
        {
            gameObject.layer = LayerMask.NameToLayer("model");
            transform.tag = Tag.prefab;
            //transform.localPosition = data.localPosition;
            LocalPosition = data.localPosition;

            InitialPosition = data.initialPosition;
            //  print(LocalPosition+"=="+ InitialPosition);
            Name = data.name;
           // LocalName=data.
            Title = data.title;
            Description = data.description;
            Layer = data.layer;
            Group = data.group;
            RenderText();

            textMesh.color = new Color(1, 1, 1, alpha);


            //boxCollider = gameObject.AddComponent<BoxCollider> ( );

        }

        public void UpdatePosition()
        {
            textMesh.transform.rotation = Quaternion.identity;

        }

        //void Update ( )
        //{
        //    textMesh.transform.rotation = Quaternion.identity;
        //}

        #region Fade In and Out

        float duration = 0.5f;

        bool isHide = true;

        public void Hide()
        {
            textMesh.color = new Color(1, 1, 1, 0);
            isHide = true;
        }

        public void FadeIn()
        {
            if (!isHide) return;
            StartCoroutine(HandleOnFade(false));
        }

        public void FadeOut()
        {
            if (isHide) return;
            StartCoroutine(HandleOnFade(true));
        }

        IEnumerator HandleOnFade(bool needsHide)
        {
            float lastTime = 0f;
            float from = textMesh.color.a;
            float to = needsHide ? 0f : 1f;

            isHide = needsHide;

            while (lastTime < duration)
            {
                lastTime += Time.deltaTime;
                var factor = lastTime / duration;
                if (factor > 1f) factor = 1f;

                var a = Mathf.Lerp(from, to, factor);

                textMesh.color = new Color(1, 1, 1, a);
                yield return new WaitForFixedUpdate();

            }



            yield return null;
        }
        #endregion
    }
}
