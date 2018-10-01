///////////////////////////////////////////////////////////////
//
// AnchorsToCorners (c) 2017 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 9/11/2017
//
///////////////////////////////////////////////////////////////

using UnityEditor;
using UnityEngine;

namespace Custom.UnityExtensions
{
    public class AnchorsToCorners
    {
        /// <summary>
        /// Moves the anchors of the selected RectTransform to its corners.
        /// </summary>
        [MenuItem("Custom/Move Anchors to Corners %[")]
        private static void MoveAnchorsToCorners()
        {
            var selectedRectTransform = Selection.activeTransform as RectTransform;

            if (selectedRectTransform == null)
            {
                Debug.Log("RectTransform not found. Make sure that the selected object has a RectTransform component.");
                return;
            }

            var parentRectTransform = Selection.activeTransform.parent as RectTransform;
            if (parentRectTransform == null) return;

            var newAnchorMin =
                new Vector2(
                    selectedRectTransform.anchorMin.x +
                    selectedRectTransform.offsetMin.x / parentRectTransform.rect.width,
                    selectedRectTransform.anchorMin.y +
                    selectedRectTransform.offsetMin.y / parentRectTransform.rect.height);

            var newAnchorMax =
                new Vector2(
                    selectedRectTransform.anchorMax.x +
                    selectedRectTransform.offsetMax.x / parentRectTransform.rect.width,
                    selectedRectTransform.anchorMax.y +
                    selectedRectTransform.offsetMax.y / parentRectTransform.rect.height);

            selectedRectTransform.anchorMin = newAnchorMin;
            selectedRectTransform.anchorMax = newAnchorMax;
            selectedRectTransform.offsetMin = Vector2.zero;
            selectedRectTransform.offsetMax = Vector2.zero;
        }
    }
}

