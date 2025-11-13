using UnityEngine;
using DG.Tweening;
using TriInspector;

namespace LumosLib
{
    [CreateAssetMenu(fileName = "ScaleTweenPreset", menuName = "[ ✨Lumos Lib ]/Scriptable Object/Tween Preset/Scale", order = int.MinValue)]
    public class ScaleTweenPreset : BaseTweenPreset
    {
        [PropertySpace(20f)]
        [Title("Scale")]
        [SerializeField] private Vector2 _targetScale;
        [SerializeField] private bool _useInitialScale;
        [SerializeField, ShowIf("_useInitialScale")] private Vector2 _initialScale;


        protected override Tween SetTween(GameObject targetObject)
        {
            if (_useInitialScale)
            {
                targetObject.transform.localScale = _initialScale;
            }
                
            return targetObject.transform.DOScale(_targetScale, Duration);
        }
    }
}