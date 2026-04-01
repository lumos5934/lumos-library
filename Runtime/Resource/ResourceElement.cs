using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace LLib
{
    [System.Serializable]
    public class ResourceElement
    {
        [HideLabel] public string key;
        [HideInInspector] public List<Object> resources;
        
        public T GetResource<T>()
        {
            foreach (var resource in resources)
            {
                if (resource is T t)
                {
                    return t;
                }
            }
            
            return default;
        }
    }
}