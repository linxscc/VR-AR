using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ModelViewerProject.UI.Label
{

    using System.Linq;

    using Label3D;

    public class Label2DUIController : MonoBehaviour
    {

        public Label2DUI LabelPrefab;
        public Dictionary<string, Label2DUI> group = new Dictionary<string, Label2DUI>();
        public void OnInit(List<Label3DHandler> label3Ds)
        {
            var children = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();
            GetComponent<RectTransform>().offsetMin = new Vector2(GetComponent<RectTransform>().offsetMin.x, -label3Ds.Count * (0.018f));
            // Transform[] child = childTransform.get;
            for (int i = 0; i < children.Count(); i++)
            {
                Destroy(children[i].gameObject);
            }
            for (int i = 0; i < label3Ds.Count; i++)
            {
                if (label3Ds[i].Group == null || label3Ds[i].Group == "分组情况")
                {
                    CreatLabel(label3Ds[i]);
                }
                else
                {
                    if (!group.ContainsKey(label3Ds[i].Group))
                    {
                        Label2DUI labelUI = LabelPrefab.Spawn();

                        labelUI.transform.parent = transform.parent.Find("GridGroud");
                        // labelUI.transform.localPosition = Vector3.zero;
                        labelUI.transform.localPosition = new Vector3(labelUI.transform.localPosition.x, labelUI.transform.localPosition.y, 0); 
                        labelUI.OnInit(label3Ds[i], LabelPrefab);

                       // Label2DUI labelUI = CreatLabel(label3Ds[i]);
                        // labelUI.transform.parent = transform.parent;
                        group.Add(label3Ds[i].Group, labelUI);
                        labelUI.labelChild.Add(label3Ds[i]);
                        //Label2DUI labelUIchild = LabelPrefab.Spawn();
                        //labelUIchild.transform.parent = labelUI.transform.FindChild("ChildMenu");
                        //labelUIchild.transform.localPosition = new Vector3(labelUI.transform.localPosition.x, labelUI.transform.localPosition.y, 0);
                        //labelUIchild.OnInit(label3Ds[i], LabelPrefab);
                    }
                    else
                    {
                        group[label3Ds[i].Group].labelChild.Add(label3Ds[i]);
                    }

                }
            }
            //foreach (Label3DHandler label3D in label3Ds)
            //{
            //    if (label3D.Group == null || label3D.Group == "分组情况")
            //    {
            //        CreatLabel(label3D);
            //    }
            //    else
            //    {
            //        if (!group.ContainsKey(label3D.Group))
            //        {
            //            Label2DUI labelUI=CreatLabel(label3D);
            //           // labelUI.transform.parent = transform.parent;
            //            group.Add(label3D.Group, labelUI);
            //        }
            //        else
            //        {
            //            group[label3D.Group].labelChild.Add(label3D);
            //        }

            //    }
            //foreach (var label3D in label3Ds)
            //{
            //    var labelUI = LabelPrefab.Spawn();

            //    labelUI.transform.parent = transform;
            //    // labelUI.transform.localPosition = Vector3.zero;
            //    labelUI.transform.localPosition = new Vector3(labelUI.transform.localPosition.x, labelUI.transform.localPosition.y, 0); ;
            //    labelUI.OnInit(label3D);
            //}
            //}
        }
        private Label2DUI CreatLabel(Label3DHandler label)
        {
            Label2DUI labelUI = LabelPrefab.Spawn();

            labelUI.transform.parent = transform;
            // labelUI.transform.localPosition = Vector3.zero;
            labelUI.transform.localPosition = new Vector3(labelUI.transform.localPosition.x, labelUI.transform.localPosition.y, 0); ;
            labelUI.OnInit(label, LabelPrefab);
            return labelUI;
        }
        void Awake()
        {
            LabelPrefab.CreatePool();
        }
        // Use this for initialization
        void Start()
        {

            //var rectTrans = transform as RectTransform;
            // var rect = rectTrans.rect;
        }

    }
}
