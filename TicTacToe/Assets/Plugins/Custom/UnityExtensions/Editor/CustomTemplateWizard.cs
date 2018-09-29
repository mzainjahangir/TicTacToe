/////////////////////////////////////////////////////////
//
// CustomTemplateWizard (c) 2017 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 09/06/2017
//
/////////////////////////////////////////////////////////

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Custom.UnityExtensions
{
    /// <summary>
    /// Class used to create the object based upon the template.
    /// </summary>
    public class CustomTemplateWizard : EditorWindow
    {
        public string ProgrammerName { get { return _programmerName; } }

        public string Namespace { get { return _namespace; } }

        public string ItemName { get { return _itemName; } }

        private Action<CustomTemplateWizard> CreateCallback { get; set; }

        private SerializedObject SerializedObject { get; set; }

        private bool HasFocusedInitialField { get; set; }

        [SerializeField]
        private string _programmerName;
        [SerializeField]
        private string _namespace;
        [SerializeField]
        private string _itemName;

        protected virtual void OnEnable()
        {
            _programmerName = EditorPrefs.GetString("CustomTemplateWizard_UserName", "Enter your name here...");
            _namespace = EditorPrefs.GetString("CustomTemplateWizard_Namespace", "Custom");

            SerializedObject = new SerializedObject(this);
        }

        protected virtual void OnDisable()
        {
            EditorPrefs.SetString("CustomTemplateWizard_UserName", ProgrammerName);
            EditorPrefs.SetString("CustomTemplateWizard_Namespace", Namespace);
        }

        protected virtual void OnGUI()
        {
            var property = SerializedObject.GetIterator();

            if (!property.Next(true)) return;

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return && GUI.GetNameOfFocusedControl() == "_itemName" && !string.IsNullOrEmpty(ItemName))
            {
                OnWizardCreate();
            }

            EditorGUI.BeginChangeCheck();

            while (property.Next(false))
            {
                var fieldInfo = GetType().GetField(property.name, BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo == null || fieldInfo.DeclaringType != GetType()) continue;

                GUI.SetNextControlName(property.name);

                EditorGUILayout.PropertyField(property);
            }

            if (!HasFocusedInitialField)
            {
                GUI.FocusControl("_itemName");
                HasFocusedInitialField = true;
            }

            if (EditorGUI.EndChangeCheck())
            {
                SerializedObject.ApplyModifiedProperties();
            }

            GUILayout.Space(EditorGUIUtility.singleLineHeight);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Create"))
            {
                OnWizardCreate();
            }
            if (GUILayout.Button("Cancel"))
            {
                OnWizardOtherButton();
            }

            GUILayout.EndHorizontal();
        }

        protected virtual void OnWizardCreate()
        {
            if (string.IsNullOrEmpty(ItemName))
            {
                EditorUtility.DisplayDialog("Custom", "Enter an item name", "Ok");
                return;
            }

            if (CreateCallback == null)
            {
                Debug.LogError("Missing create callback.");
                Close();
                return;
            }

            CreateCallback(this);
            Close();
        }

        protected virtual void OnWizardOtherButton()
        {
            Close();
        }

        public static void DisplayWizard(Action<CustomTemplateWizard> createCallback)
        {
            if (createCallback == null) throw new ArgumentNullException("createCallback");

            var wizard = CreateInstance<CustomTemplateWizard>();

            wizard.ShowUtility();
            wizard.titleContent.text = "C# Template";
            wizard.CreateCallback = createCallback;
        }
    }
}
