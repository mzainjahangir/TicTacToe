///////////////////////////////////////////////////////////////
//
// ValueRequiredAttribute (c) 2017 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 9/13/2017
//
///////////////////////////////////////////////////////////////

using System;
using UnityEngine;

namespace Custom
{
    /// <summary>
    /// Property attribute used when the developer wants to enforce assigning the object in the editor.
    /// Note: Not meant to be used with arrays!
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ValueRequiredAttribute : PropertyAttribute
    {
        public bool OnlyRequiredInScene { get; private set; }

        public ValueRequiredAttribute(bool onlyRequiredInScene)
        {
            OnlyRequiredInScene = onlyRequiredInScene;
        }

        public ValueRequiredAttribute()
            : this(false)
        {
        }
    }
}
