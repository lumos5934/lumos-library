using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace LumosLib.Core
{
    public abstract class BasePoolManager : MonoBehaviour, IPreInitialize
    {
        public int PreID => (int)PreInitializeOrder.Pool;
        public abstract int PreInitOrder { get; }
        public bool PreInitialized { get; private set; }
        
        
        protected Dictionary<string, object> pools = new();
        protected Dictionary<string, HashSet<MonoBehaviour>> activeObjects = new();
        
        
        public virtual void PreInit()
        {
            Global.Register((IPoolManager)this);

            PreInitialized = true;
        }

        protected abstract ObjectPool<T> CreatePool<T>(string key, T prefab, int defaultCapacity, int maxSize) where T : MonoBehaviour, IPoolable;
    }
}