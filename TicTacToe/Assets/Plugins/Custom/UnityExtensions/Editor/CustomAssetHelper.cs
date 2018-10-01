/////////////////////////////////////////////////////////
//
// CustomAssetHelper (c) 2017 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 09/06/2017
//
/////////////////////////////////////////////////////////

using System.IO;
using UnityEngine;
using UnityEditor;

namespace Custom.UnityExtensions
{
    /// <summary>
    /// Helper class to help with asset creation and retrieval. 
    /// </summary>
    public class CustomAssetHelper
    {
        /// <summary>
        /// Gets the folder path for the object passed.
        /// </summary>
        public static string GetObjectFolder(Object @object)
        {
            string objectPath = AssetDatabase.GetAssetPath(@object);

            if (string.IsNullOrEmpty(objectPath))
            {
                objectPath = "Assets";
            }
            else if (Path.GetExtension(objectPath) != "")
            {
                // Get the folder path
                objectPath = objectPath.Replace(Path.GetFileName(objectPath), "");
            }

            return objectPath;
        }

        /// <summary>
        /// Gets the folder path of the selected object.
        /// </summary>
        public static string GetSelectedObjectFolder()
        {
            return GetObjectFolder(Selection.activeObject);
        }
        
        /// <summary>
        /// Creates the asset at the selected folder.
        /// </summary>
        public static void CreateAssetAtSelectedFolder(Object asset, string name)
        {
            var assetFolder = GetSelectedObjectFolder();

            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(string.Format("{0}/{1}.asset", assetFolder, name)));
            AssetDatabase.SaveAssets();
        }
    }
}
