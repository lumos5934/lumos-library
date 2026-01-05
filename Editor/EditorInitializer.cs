using System.IO;
using UnityEditor;
using UnityEngine;

namespace LumosLib
{
    [InitializeOnLoad]
    public static class EditorInitializer
    {
        static EditorInitializer()
        {
            EditorApplication.delayCall += OnEditorFullyLoaded;
        }

        private static void OnEditorFullyLoaded()
        {
            var name = nameof(LumosLibSetting);
            
            if (Resources.Load<LumosLibSetting>(name) == null)
            {
                string resourcesDir = Path.Combine(Application.dataPath, "Resources");
                if (!Directory.Exists(resourcesDir))
                {
                    Directory.CreateDirectory(resourcesDir);
                }

                var asset = ScriptableObject.CreateInstance<LumosLibSetting>();
        
                string assetPath = $"Assets/Resources/{name}.asset";

                AssetDatabase.CreateAsset(asset, assetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}