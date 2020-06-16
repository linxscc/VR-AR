using UnityEngine;
using UnityEditor;
using System.Collections;

namespace ModelViewerProject.Label3D
{
    [CanEditMultipleObjects]
    [CustomEditor ( typeof ( Label3DHandler ) )]

    public class LabelHandlerEditor : Editor
    {
        public override void OnInspectorGUI ( )
        {
            if ( target == null )
                return;

            EditorGUILayout.LabelField ( "名称 : ", EditorStyles.boldLabel );
            mLabel.Title = EditorGUILayout.TextField ( mLabel.Title, GUILayout.Height ( 18 ) );
            EditorGUILayout.LabelField("分组 : ", EditorStyles.boldLabel);
            mLabel.Group = EditorGUILayout.TextArea(mLabel.Group, GUILayout.Height(18));
            EditorGUILayout.LabelField("在逐层模式中所属层级 : ", EditorStyles.boldLabel);
            mLabel.Layer = EditorGUILayout.FloatField(mLabel.Layer, GUILayout.Height(18));
            EditorGUILayout.LabelField ( "详细信息 : ", EditorStyles.boldLabel );
            mLabel.Description = EditorGUILayout.TextArea ( mLabel.Description, GUILayout.Height ( 64 ) );
    
           // mLabel.Layer=

            if ( GUI.changed )
                mLabel.RenderText ( );
        }
                
        void OnSceneGUI ( )
        {
            if ( target == null )
                return;

            Handles.Label ( mLabel.transform.position, mLabel.Title );
           // Handles.Label ( mLabel.transform.position, mLabel.Description );

        }

        Label3DHandler mLabel
        {
            get
            {
                return target as Label3DHandler;
            }
        }
    }
}
