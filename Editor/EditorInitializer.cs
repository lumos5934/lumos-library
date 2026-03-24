using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace LumosLib.Editor
{
    public static class EditorInitializer
    {
        [DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            EditorApplication.delayCall += OnEditorFullyLoaded;
            
        }
        
        private static void OnEditorFullyLoaded()
        {
            string assetPath = $"Assets/{nameof(LumosLibSettings)}.asset";
            var settings = AssetDatabase.LoadAssetAtPath<LumosLibSettings>(assetPath);

            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<LumosLibSettings>();
                AssetDatabase.CreateAsset(settings, assetPath);
                AssetDatabase.SaveAssets();
            }

            var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            if (!preloadedAssets.Contains(settings))
            {
                preloadedAssets.Add(settings);
                PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            }
        }
    }
}