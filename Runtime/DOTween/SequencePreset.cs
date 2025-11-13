using System;
using DG.Tweening;
using TriInspector;
using UnityEngine;

namespace LumosLib
{
    [Serializable]
    public class SequencePreset
    {
        [field: Group("Key")]
        [field: SerializeField, HideLabel] public string Key { get; private set; }
        
        [field: Group("Preset")]
        [field: SerializeField] public BaseTweenPreset[] Presets { get; private set; }
        

        public Sequence GetSequence(GameObject targetObject)
        {
            var sequence = DOTween.Sequence();
            
            foreach (var preset in Presets)
            {
                var tween = preset.GetTween(targetObject);
                if (tween != null)
                {
                    sequence.Join(tween);
                }
            }

            return sequence;
        }
    }
}