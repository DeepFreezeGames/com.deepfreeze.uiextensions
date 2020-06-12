using System;
using UnityEditor;
using UnityEngine;

namespace UIExtensions.Editor
{
    public class ExtendedEditorWindow : EditorWindow
    {
        protected SerializedObject SerializedObject;
        protected SerializedProperty CurrentProperty;

        private string _selectedPropertyPath;
        protected SerializedProperty SelectedProperty;

        protected static string Title = "Editor";
        
        public static void Initialize()
        {
            var window = GetWindow<ExtendedEditorWindow>();
            window.titleContent = new GUIContent(Title);
            window.Show();
        }

        protected void DrawProperties(SerializedProperty property, bool drawChildren)
        {
            var lastPropPath = string.Empty;
            foreach (SerializedProperty prop in property)
            {
                if (prop.isArray && prop.propertyType == SerializedPropertyType.Generic)
                {
                    using (new HorizontalBlock())
                    {
                        prop.isExpanded = EditorGUILayout.Foldout(prop.isExpanded, prop.displayName);
                    }

                    if (prop.isExpanded)
                    {
                        EditorGUI.indentLevel++;
                        DrawProperties(prop, drawChildren);
                        EditorGUI.indentLevel--;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(lastPropPath) && prop.propertyPath.Contains(lastPropPath))
                    {
                        continue;
                    }

                    lastPropPath = prop.propertyPath;
                    EditorGUILayout.PropertyField(prop, drawChildren);
                }
            }
        }

        protected void DrawSidebar(SerializedProperty property)
        {
            foreach (SerializedProperty prop in property)
            {
                if (GUILayout.Button(prop.displayName))
                {
                    _selectedPropertyPath = prop.propertyPath;
                }
            }

            if (!string.IsNullOrEmpty(_selectedPropertyPath))
            {
                SelectedProperty = SerializedObject.FindProperty(_selectedPropertyPath);
            }
        }

        protected void DrawField(string propertyName, bool relative)
        {
            if (relative && CurrentProperty != null)
            {
                EditorGUILayout.PropertyField(CurrentProperty.FindPropertyRelative(propertyName), true);
            }
            else if(SerializedObject != null)
            {
                EditorGUILayout.PropertyField(SerializedObject.FindProperty(propertyName), true);
            }
        }

        protected void Apply()
        {
            SerializedObject.ApplyModifiedProperties();
        }
    }
}
