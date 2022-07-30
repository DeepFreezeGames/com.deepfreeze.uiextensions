using UnityEditor;
using UnityEngine;

namespace DeepFreeze.Packages.UIExtensions.Editor
{
    public abstract class ExtendedEditor : UnityEditor.Editor
    {
        protected SerializedObject SerializedObject;
        protected SerializedProperty CurrentProperty;
    
        private string _selectedPropertyPath;
        protected SerializedProperty SelectedProperty;

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

        protected void DrawField(string propertyName, bool relative = true)
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
