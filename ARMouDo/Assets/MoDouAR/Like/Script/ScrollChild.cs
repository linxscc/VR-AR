/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace MoDouAR
{
    public class ScrollChild : MonoBehaviour
    {
        private int index = 0;
        List<ChildItemData> itemData = new List<ChildItemData>();
        Dictionary<int, ModelButton> modelList = new Dictionary<int, ModelButton>();
        private TypeButton typeButton;
        public void SetItem(TypeButton typeButton)
        {
            foreach (KeyValuePair<int, ModelButton> item in this.modelList)
            {
                ObjectBool.Return(item.Value.gameObject);
            }
            this.modelList.Clear();
            this.itemData = typeButton.childItem;
            this.modelList = typeButton.modelList;
            this.typeButton = typeButton;
            RectTransform r = ModelLibrary.Instance.childGrid.GetComponent<RectTransform>();
            int y = itemData.Count / 2;
            if (itemData.Count % 2 != 0)
                y += 1;
            y = (340 + 18) * y;
            r.sizeDelta = new Vector2(r.sizeDelta.x, y);
            ModelLibrary.Instance.childGrid.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
            index = 0;
            CreatButton();
        }
        private void CreatButton()
        {
            if (index <= itemData.Count - 1)
            {

                if (!modelList.ContainsKey(itemData[index].item.id))
                {
                    GameObject item = ObjectBool.Get(ModelLibrary.Instance.ModelButtonP);
                    // yield return item;
                    item.transform.parent = ModelLibrary.Instance.childGrid;
                    item.transform.SetAsFirstSibling();
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localRotation = Quaternion.identity;
                    item.transform.localScale = new Vector3(1f, 1f, 1f);
                    if (!modelList.ContainsKey(itemData[index].item.id))
                        modelList.Add(itemData[index].item.id, item.GetComponent<ModelButton>());
                    item.GetComponent<ModelButton>().OnInit(itemData[index], typeButton);
                    if (index <= itemData.Count - 1)
                    {
                        index++;
                        CreatButton();
                    }
                }
                else
                {
                    index++;
                    CreatButton();
                }
            }
            else
            {
                //ModelLibrary.Instance.childGrid.GetComponent<ContentSizeFitter>().enabled = true;
                ModelLibrary.Instance.childGrid.parent.GetComponent<ScrollRect>().horizontalNormalizedPosition = 1;
            }
        }
    }
}