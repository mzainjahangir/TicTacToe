///////////////////////////////////////////////////////////////
//
// CornersToAnchors (c) 2017 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 9/11/2017
//
///////////////////////////////////////////////////////////////

using UnityEditor;
using UnityEngine;

namespace Custom.UnityExtensions
{
    public class CornersToAnchors 
    {
        /// <summary>
        /// Moves the corners of the selected RectTransform to its anchors.
        /// </summary>
        [MenuItem("Custom/Move Corners to Anchors %]")]

        private static void MoveCornersToAnchors()
        {
            var selectedRectTransform = Selection.activeTransform as RectTransform;

            if (selectedRectTransform == null)
            {
                Debug.Log("RectTransform not found. Make sure that the selected object has a RectTransform component.");
                return;
            }

            selectedRectTransform.offsetMin = Vector2.zero;
            selectedRectTransform.offsetMax = Vector2.zero;
        }
    }
}
