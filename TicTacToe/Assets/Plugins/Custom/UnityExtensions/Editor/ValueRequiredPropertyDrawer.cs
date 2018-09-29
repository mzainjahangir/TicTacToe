///////////////////////////////////////////////////////////////
//
// ValueRequiredPropertyDrawer (c) 2017 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 9/13/2017
//
///////////////////////////////////////////////////////////////

using UnityEditor;
using UnityEngine;

namespace Custom.UnityExtensions
{
    /// <summary>
    /// Class used to draw the Value Required property attribute in the Editor.
    /// </summary>
    [CustomPropertyDrawer(typeof(ValueRequiredAttribute))]
    public class ValueRequiredPropertyDrawer : PropertyDrawer
    {
        private bool IsInitialized { get; set; }

        private GUIStyle ErrorStyle { get; set; }

        private void InitializeGUI()
        {
            if (IsInitialized) return;

            ErrorStyle = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).FindStyle("sv_label_6");
            IsInitialized = true;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            switch (property.propertyType)
            {
                default:
                    base.OnGUI(position, property, label);
                    break;

                case SerializedPropertyType.ObjectReference:
                    if (ShouldShowMissingValueGUI(property))
                    {
                        HandleMissingValueGUI(position, property, label);
                    }
                    else
                    {
                        EditorGUI.PropertyField(position, property, label);
                    }
                    break;
            }
        }

        private void HandleMissingValueGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InitializeGUI();

            var errorRect = new Rect(position)
            {
                width = 19,
                height = 8
            };
            errorRect.x += EditorGUIUtility.labelWidth - errorRect.width;
            errorRect.y += 2;

            if (Event.current.type == EventType.Repaint)
            {
                GUI.Label(errorRect, "!", ErrorStyle);
            }

            EditorGUI.PropertyField(position, property, label);
        }

        private bool ShouldShowMissingValueGUI(SerializedProperty property)
        {
            if (property.objectReferenceValue != null) return false;

            var valueRequiredAttribute = attribute as ValueRequiredAttribute;
            if (valueRequiredAttribute == null) return false;

            return !valueRequiredAttribute.OnlyRequiredInScene || PrefabUtility.GetPrefabType(property.serializedObject.targetObject) != PrefabType.Prefab;
        }
    }
}
