using UnityEngine;
using DG.Tweening;
using TriInspector;

namespace LumosLib
{
    //[CreateAssetMenu(fileName = "#Name#TweenPreset", menuName = "[ ✨Lumos Lib ]/Scriptable Object/Tween Preset/#Name#")]
    public abstract class BaseTweenPreset : ScriptableObject
    {
        [field: Title("Base")]
        [field: SerializeField] public float Duration { get; private set;}
        [field: SerializeField] public bool UseRandomDuration{ get; private set;}
        [field: SerializeField, ShowIf("UseRandomDuration"), Range(0, 1)] public float DurationRandomFactor{ get; private set;}
        
        [field: PropertySpace(20f)]
        [field: Title("Loop")]
        [field: SerializeField] public bool IsLoop{ get; private set;}
        [field: SerializeField, ShowIf("IsLoop")] public LoopType LoopType { get; private set;}
        [field: SerializeField, ShowIf("IsLoop")] public int LoopCount { get; private set;}


        public abstract Sequence GetTween(Component component);
    }
}