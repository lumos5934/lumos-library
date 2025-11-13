using System.Collections.Generic;
using DG.Tweening;
using TriInspector;
using UnityEngine;

namespace LumosLib
{
    public class SequencePresetContainer : MonoBehaviour
    {
        #region >--------------------------------------------------- FIELD
        
        
        [Title("Preset")]
        [Space(10f)]
        
        [InfoBox("Duplicate 'Key' detected — only the first one will be applied.", TriMessageType.Error, nameof(IsValidKey))]
        [TableList(Draggable = true, HideAddButton = false, HideRemoveButton = false, AlwaysExpanded = false)]
        [SerializeField] private List<SequencePreset> _presets;

        
        #endregion
        #region >--------------------------------------------------- GET
        
        
        public Sequence GetSequence(string key, GameObject applyObject)
        {
            for (int i = 0; i < _presets.Count; i++)
            {
                if (_presets[i].Key == key)
                {
                    return _presets[i].GetSequence(applyObject);
                }
            }

            return null;
        }
        
        
        #endregion
        #region >--------------------------------------------------- INSPECTOR
        
        
        private bool IsValidKey()
        {
            var containsKey = new HashSet<string>();
            
            for (int i = 0; i < _presets.Count; i++)
            {
                if (!containsKey.Add(_presets[i].Key))
                {
                    return true;
                }
            }

            return false;
        }
        
        
        #endregion
    }
}