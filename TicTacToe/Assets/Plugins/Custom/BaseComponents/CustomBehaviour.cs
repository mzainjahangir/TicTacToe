///////////////////////////////////////////////////////////////
//
// CustomBehaviour (c) 2017 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 09/06/2017
//
///////////////////////////////////////////////////////////////

using System.Reflection;
using System.Text;
using UnityEngine;

namespace Custom
{
    /// <summary>
    /// Base class for all the components we create in Unity.
    /// </summary>
    public class CustomBehaviour : MonoBehaviour
    {

        protected virtual void Awake()
        {
            // Checking for the required value attribute.
            CheckForRequiredValues();
        }

        protected void CheckForRequiredValues()
        {
            foreach (var fieldInfo in GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                // Make sure that the field value can be null.
                var fieldType = fieldInfo.FieldType;
                if (fieldType.IsValueType) continue;

                // Check for the ValueRequired attribute.

                var attributes = fieldInfo.GetCustomAttributes(typeof(ValueRequiredAttribute), true);

                if (attributes.Length == 0) continue;

                // See if the value of the field is null.

                var fieldValue = fieldInfo.GetValue(this);
                var unityObjectFieldValue = fieldValue as Object;

                if (fieldValue != null && unityObjectFieldValue) continue;

                Debug.LogError(name + ": Missing required value for " + fieldInfo.Name.ToFriendly() + " on " + gameObject.name, this);
                enabled = false;
            }
        }

        /// <summary>
        /// Returns the hierarchy for the transform.
        /// </summary>
        public static string GetHierarchyName(Transform transform)
        {
            var nameBuilder = new StringBuilder();

            for (var current = transform; current != null; current = current.parent)
            {
                if (current != transform)
                {
                    nameBuilder.Insert(0, '/');
                }
                nameBuilder.Insert(0, current.name);
            }

            return nameBuilder.ToString();
        }
    }
}
