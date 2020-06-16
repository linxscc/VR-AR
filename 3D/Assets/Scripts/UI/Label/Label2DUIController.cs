using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ModelViewerProject.UI.Label
{

    using System.Linq;

    using Label3D;
    using UnityEngine.UI;

    public class Label2DUIController : MonoBehaviour
    {
        private GameObject itemPrefab;
        private GameObject itemChild;
        /// <summary>
        /// 所有按钮
        /// </summary>
        public Dictionary<string, List<GridItem>> group = new Dictionary<string, List<GridItem>>();
        /// <summary>
        /// 一组的按钮
        /// </summary>
        public Dictionary<string, GridItem> gridItem = new Dictionary<string, GridItem>();
        public void OnInit(List<Label3DHandler> label3Ds, UIController uiController)
        {
            itemPrefab = Resources.Load<GameObject>(Global.itemfoUrl);
            itemChild = Resources.Load<GameObject>(Global.buttonChildUrl);
            Transform[] children = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();
            for (int i = 0; i < children.Count(); i++)
            {
                Destroy(children[i].gameObject);
            }
            for (int i = 0; i < label3Ds.Count; i++)
            {
                if (label3Ds[i].Group == null || label3Ds[i].Group == "分组情况")
                {
                    GameObject o = Instantiate(itemPrefab);
                    o.transform.parent = transform;
                    o.name = label3Ds[i].Title;
                    o.transform.localScale = Vector3.one;
                    o.transform.localPosition = Vector3.zero;
                    GridItem item = o.GetComponent<GridItem>();
                    o.transform.FindChild("Title/ItemButton").GetComponentInChildren<Text>().text = label3Ds[i].Title;
                    o.transform.FindChild("Title/Button").GetComponentInChildren<Text>().text = label3Ds[i].Title;
                    item.OnInit(label3Ds[i]);

                    item.onClick += uiController.Label2DUI_OnClick;
                    List<GridItem> g = new List<GridItem>();
                    g.Add(item);
                    group.Add(label3Ds[i].Title, g);

                }
                else
                {

                    if (!group.ContainsKey(label3Ds[i].Group))
                    {
                        GameObject itemO = Instantiate(itemPrefab);
                        itemO.transform.parent = transform;
                        itemO.name = label3Ds[i].Group;
                        itemO.transform.localScale = Vector3.one;
                        itemO.transform.localPosition = Vector3.zero;
                        itemO.transform.FindChild("Title/ItemButton").GetComponentInChildren<Text>().text = label3Ds[i].Group;
                        
                        itemO.transform.FindChild("Title/Button").GetComponentInChildren<Text>().text = label3Ds[i].Group;
                        GridItem itemP = itemO.GetComponent<GridItem>();
                        gridItem.Add(label3Ds[i].Group, itemP);
                        itemP.OnInit(label3Ds[i]);
                        itemP.onClick += uiController.Label2DUI_OnClick;
                        List<GridItem> g = new List<GridItem>();
                        group.Add(label3Ds[i].Group, g);
                    }
                    GameObject o = Instantiate(itemChild);
                    GridItem item = o.GetComponent<GridItem>();
                    item.OnInit(label3Ds[i]);
                    item.onClick += uiController.Label2DUI_OnClick;
                    group[label3Ds[i].Group].Add(item);
                    gridItem[label3Ds[i].Group].labelChild.Add(item);
                    //group[label3Ds[i].Group].labelChild.Add(label3Ds[i]);
                    o.transform.parent = transform.FindChild(label3Ds[i].Group + "/" + "ItemChild");
                    o.GetComponentInChildren<Text>().text = label3Ds[i].Description;
                    o.transform.localScale = Vector3.one;
                    o.transform.localPosition = Vector3.zero;
                    // group.Add(label3Ds[i].Group, label3Ds[i]);
                }
            }
            foreach (KeyValuePair<string,List<GridItem>> item in group)
            {

            }
        }
        //public void OnInit(List<Label3DHandler> label3Ds)
        //{
        //    var children = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();
        //    GetComponent<RectTransform>().offsetMin = new Vector2(GetComponent<RectTransform>().offsetMin.x, -label3Ds.Count * (0.018f));
        //    // Transform[] child = childTransform.get;
        //    for (int i = 0; i < children.Count(); i++)
        //    {
        //        Destroy(children[i].gameObject);
        //    }
        //    for (int i = 0; i < label3Ds.Count; i++)
        //    {
        //        if (label3Ds[i].Group == null || label3Ds[i].Group == "分组情况")
        //        {
        //            CreatLabel(label3Ds[i]);
        //        }
        //        else
        //        {
        //            if (!group.ContainsKey(label3Ds[i].Group))
        //            {
        //                Label2DUI labelUI = LabelPrefab.Spawn();

        //                labelUI.transform.parent = transform.parent.FindChild("GridGroud");
        //                // labelUI.transform.localPosition = Vector3.zero;
        //                labelUI.transform.localPosition = new Vector3(labelUI.transform.localPosition.x, labelUI.transform.localPosition.y, 0);
        //                labelUI.OnInit(label3Ds[i], LabelPrefab);

        //                // Label2DUI labelUI = CreatLabel(label3Ds[i]);
        //                // labelUI.transform.parent = transform.parent;
        //                group.Add(label3Ds[i].Group, labelUI);
        //                labelUI.labelChild.Add(label3Ds[i]);
        //                //Label2DUI labelUIchild = LabelPrefab.Spawn();
        //                //labelUIchild.transform.parent = labelUI.transform.FindChild("ChildMenu");
        //                //labelUIchild.transform.localPosition = new Vector3(labelUI.transform.localPosition.x, labelUI.transform.localPosition.y, 0);
        //                //labelUIchild.OnInit(label3Ds[i], LabelPrefab);
        //            }
        //            else
        //            {
        //                group[label3Ds[i].Group].labelChild.Add(label3Ds[i]);
        //            }

        //        }
        //    }
        //    //foreach (Label3DHandler label3D in label3Ds)
        //    //{
        //    //    if (label3D.Group == null || label3D.Group == "分组情况")
        //    //    {
        //    //        CreatLabel(label3D);
        //    //    }
        //    //    else
        //    //    {
        //    //        if (!group.ContainsKey(label3D.Group))
        //    //        {
        //    //            Label2DUI labelUI=CreatLabel(label3D);
        //    //           // labelUI.transform.parent = transform.parent;
        //    //            group.Add(label3D.Group, labelUI);
        //    //        }
        //    //        else
        //    //        {
        //    //            group[label3D.Group].labelChild.Add(label3D);
        //    //        }

        //    //    }
        //    //foreach (var label3D in label3Ds)
        //    //{
        //    //    var labelUI = LabelPrefab.Spawn();

        //    //    labelUI.transform.parent = transform;
        //    //    // labelUI.transform.localPosition = Vector3.zero;
        //    //    labelUI.transform.localPosition = new Vector3(labelUI.transform.localPosition.x, labelUI.transform.localPosition.y, 0); ;
        //    //    labelUI.OnInit(label3D);
        //    //}
        //    //}
        //}
        //private Label2DUI CreatLabel(Label3DHandler label)
        //{
        //    Label2DUI labelUI = LabelPrefab.Spawn();

        //    labelUI.transform.parent = transform;
        //    // labelUI.transform.localPosition = Vector3.zero;
        //    labelUI.transform.localPosition = new Vector3(labelUI.transform.localPosition.x, labelUI.transform.localPosition.y, 0); ;
        //    labelUI.OnInit(label, LabelPrefab);
        //    return labelUI;
        //}
    }
}
