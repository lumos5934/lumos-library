using UnityEngine;
using DG.Tweening;
using TriInspector;

namespace LumosLib
{
    [CreateAssetMenu(fileName = "ScaleTweenPreset", menuName = "[ ✨Lumos Lib ]/Scriptable Object/Tween Preset/Scale")]
    public class ScaleTweenPreset : BaseTweenPreset
    {
        [field: PropertySpace(20f)]
        [field: Title("Scale")]
        [field: SerializeField] public Ease ScaleEase { get; private set; }
        [field: SerializeField] public Vector2 TargetScale { get; private set; }
        [field: SerializeField] public bool UseInitialScale { get; private set; }
        [field: SerializeField, ShowIf("UseInitialScale")] public Vector2 InitialScale { get; private set; }
        
        public override Sequence GetTween(Component component)
        {
            if (component is Transform transform)
            {
                if (UseInitialScale)
                {
                    transform.localScale = InitialScale;
                }
                
                transform.DOScale(TargetScale, Duration).SetEase(ScaleEase);
            }
            return null;
        }
    }
}