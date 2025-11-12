using System;
using TriInspector;
using UnityEngine;

namespace LumosLib
{
    [Serializable]
    public class UISequencePreset
    {
        public enum Type
        {
            PointerEnter,
            PointerExit,
            PointerClick,
            PointerDown,
            PointerUp,
            Select,
            Deselect,
            Submit,
            Cancel,
        }
    
        [field: Group("Event Type")]
        [field: SerializeField, HideLabel] public Type EventType { get; private set; }
        
        [field: Group("Preset Key")]
        [field: SerializeField, HideLabel] public string PresetKey { get; private set; }
    }
}