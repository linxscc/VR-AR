/*
 *    日期:2017/7/11
 *    作者:
 *    标题:组件编辑器扩展
 *    功能:
*/
using UnityEditor;
using UnityEngine;

namespace PlaceAR
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PrefabChildControl))]
    public class PrefabChildControlEditor : Editor
    {
        private PrefabChildControl meshs;
        public void OnEnable()
        {
            if (target == null) return;
            meshs = (PrefabChildControl)target;
            Debug.Log(meshs.transform.position);
        }
        public override void OnInspectorGUI()
        {

            if (target == null)
                return;

            EditorGUILayout.LabelField("该模型是否隐藏 : ", EditorStyles.boldLabel);
            mLabel.data.isHide = EditorGUILayout.Toggle(mLabel.data.isHide);
            EditorGUILayout.LabelField("名称 : ", EditorStyles.boldLabel);
            mLabel.data.title = EditorGUILayout.TextField(mLabel.data.title, GUILayout.Height(18));
            EditorGUILayout.LabelField("分组 : ", EditorStyles.boldLabel);
            mLabel.data.group = EditorGUILayout.TextArea(mLabel.data.group, GUILayout.Height(18));
            EditorGUILayout.LabelField("所属层 : ", EditorStyles.boldLabel);
            mLabel.data.layer = EditorGUILayout.IntField(mLabel.data.layer, GUILayout.Height(18));
            EditorGUILayout.LabelField("详细信息 : ", EditorStyles.boldLabel);
            mLabel.data.description = EditorGUILayout.TextArea(mLabel.data.description, GUILayout.Height(64));
        }
        public void OnSceneGUI()
        {
            //meshs.UpdateLine();
           // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        }
        PrefabChildControl mLabel
        {
            get
            {
                return target as PrefabChildControl;
            }
        }
    }
}
