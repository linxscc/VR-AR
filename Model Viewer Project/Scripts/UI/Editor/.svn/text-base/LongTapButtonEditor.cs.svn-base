using UnityEngine;
using System.Collections;

namespace ModelViewerProject.UI
{
    using UnityEngine.UI;
    using UnityEditor;
    using UnityEditor.UI;

    [CustomEditor ( typeof ( LongTapButton ), true )]
    [CanEditMultipleObjects]
    public class LongTapButtonEditor : SelectableEditor
    {

        SerializedProperty m_OnButtonDownProperty;

        SerializedProperty m_OnButtonUpProperty;

        protected override void OnEnable ( )
        {
            base.OnEnable ( );
            m_OnButtonDownProperty = serializedObject.FindProperty ( "m_OnButtonDown" );

            m_OnButtonUpProperty = serializedObject.FindProperty ( "m_OnButtonUp" );
        }

        public override void OnInspectorGUI ( )
        {
            base.OnInspectorGUI ( );
            EditorGUILayout.Space ( );

            serializedObject.Update ( );
            EditorGUILayout.PropertyField ( m_OnButtonDownProperty );
            EditorGUILayout.PropertyField ( m_OnButtonUpProperty );
            serializedObject.ApplyModifiedProperties ( );
        }
    }
}
