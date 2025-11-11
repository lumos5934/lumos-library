using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using TriInspector;

namespace LumosLib
{
    public class PreInitializeConfig : ScriptableObject
    {
        public enum TableType
        {
            None,
            GoogleSheet,
            Local
        }
        
        [field: Title("Data")]
        [field: SerializeField] public TableType SelectedTableType { get; private set; }
        [field: SerializeField, HideIf("SelectedTableType", TableType.None)] public string TablePath { get; private set; }
        
        
        [field: PropertySpace(20f)]
        [field: Title("Audio")]
        [field: SerializeField] public AudioMixer Mixer { get; private set; }
        [field: SerializeField] public AudioPlayer AudioPlayerPrefab { get; private set; }


        [field: PropertySpace(20f)]
        [field: Title("PreInitialize")]
        [field: SerializeField] public List<MonoBehaviour> PreInitializeList { get; private set; } = new();


        private void Awake()
        {
#if UNITY_EDITOR
            SelectedTableType = TableType.None;
            
            if (PreInitializeList.Count == 0)
            {
                PreInitializeList.Add(Resources.Load<DataManager>(nameof(DataManager)));
                PreInitializeList.Add(Resources.Load<PoolManager>(nameof(PoolManager)));
                PreInitializeList.Add(Resources.Load<AudioManager>(nameof(AudioManager)));
                PreInitializeList.Add(Resources.Load<UIManager>(nameof(UIManager)));
                PreInitializeList.Add(Resources.Load<ResourceManager>(nameof(ResourceManager)));
            }
            
            if (AudioPlayerPrefab == null)
            {
                AudioPlayerPrefab = Resources.Load<AudioPlayer>(nameof(AudioPlayer));
            }
        }
#endif
    }
}
