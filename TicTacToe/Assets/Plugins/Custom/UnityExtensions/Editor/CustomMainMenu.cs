///////////////////////////////////////////////////////////////
//
// CustomMainMenu (c) 2017 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 09/06/2017
//
///////////////////////////////////////////////////////////////

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Custom.UnityExtensions
{
    /// <summary>
    /// Class used to draw the Custom main menus in Unity.
    /// </summary>
    public class CustomMainMenu
    {
        #region Standard Menu Items

        [MenuItem("Custom/Delete Player Preferences")]
        public static void DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("Custom/Clear Cache")]
        public static void ClearCache()
        {
            Caching.ClearCache();
        }

        #endregion

        #region Code Templates 

        private const string TemplateHeader = @"///////////////////////////////////////////////////////////////
//
// $ItemName (c) $Year Muhammad Zain Jahangir
//
// Created by $User on $DateTime
//
///////////////////////////////////////////////////////////////
";

        private const string BehaviourTemplate = @"
using UnityEngine;

namespace $Namespace
{
    public class $ItemName : CustomBehaviour
    {
    }
}
";

        private const string EnumTemplate = @"
namespace $Namespace
{
    public enum $ItemName
    {
    }
}
";

        private const string InterfaceTemplate = @"
namespace $Namespace
{
    public interface $ItemName
    {
    }
}
";
        private const string ClassTemplate = @"
namespace $Namespace
{
    public class $ItemName
    {
    }
}
";

        [MenuItem("Assets/Custom/Create/CustomBehavior")]
        private static void CreateCodeItemFromTemplate()
        {
            CustomTemplateWizard.DisplayWizard(wizard => CreateItemFromTemplate(TemplateHeader + BehaviourTemplate, ".cs", wizard));
        }

        [MenuItem("Assets/Custom/Create/Enum")]
        private static void CreateEnumItemFromTemplate()
        {
            CustomTemplateWizard.DisplayWizard(wizard => CreateItemFromTemplate(TemplateHeader + EnumTemplate, ".cs", wizard));
        }

        [MenuItem("Assets/Custom/Create/Interface")]
        private static void CreateInterfaceItemFromTemplate()
        {
            CustomTemplateWizard.DisplayWizard(wizard => CreateItemFromTemplate(TemplateHeader + InterfaceTemplate, ".cs", wizard));
        }

        [MenuItem("Assets/Custom/Create/Class")]
        private static void CreateClassItemFromTemplate()
        {
            CustomTemplateWizard.DisplayWizard(wizard => CreateItemFromTemplate(TemplateHeader + ClassTemplate, ".cs", wizard));
        }

        private static void CreateItemFromTemplate(string template, string fileExtension, CustomTemplateWizard wizard)
        {
            try
            {
                string path = Path.ChangeExtension(Path.Combine(CustomAssetHelper.GetSelectedObjectFolder(), wizard.ItemName), fileExtension);

                if (File.Exists(path))
                {
                    if (!EditorUtility.DisplayDialog("File exists already!", "Do you want to overwrite the file?", "Yes", "No")) return;
                }

                using (var writer = new StreamWriter(path, false))
                {
                    writer.Write(ReplaceTemplateTokens(template,
                        new[] { "$ItemName", wizard.ItemName },
                        new[] { "$User", wizard.ProgrammerName },
                        new[] { "$Namespace", wizard.Namespace },
                        new[] { "$Year", DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) },
                        new[] { "$DateTime", DateTime.Now.ToShortDateString() },
                        new[] { "$EditingItemName", FindEditingItemName(wizard.ItemName) }));
                }

                AssetDatabase.ImportAsset(path);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        private static string ReplaceTemplateTokens(string template, params string[][] tokens)
        {
            return tokens.Aggregate(template, (current, token) => current.Replace(token[0], token[1]));
        }

        private static string FindEditingItemName(string itemName)
        {
            if (!itemName.EndsWith("Editor")) return "";

            var indexOfEditor = itemName.IndexOf("Editor", StringComparison.Ordinal);
            return itemName.Substring(0, indexOfEditor);
        }

        #endregion
    }
}