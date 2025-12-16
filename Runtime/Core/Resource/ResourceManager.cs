using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LumosLib
{
    public class ResourceManager : MonoBehaviour, IPreInitializable, IResourceManager
    {
        #region  >--------------------------------------------------- FIELD


        private Dictionary<string, object> _cahcedResources = new();
        
        
        #endregion
        #region  >--------------------------------------------------- INIT
        
        
        public IEnumerator InitAsync()
        {
            GlobalService.Register<IResourceManager>(this);
            DontDestroyOnLoad(gameObject);
            
            yield break;
        }

        
        
        #endregion
        #region  >--------------------------------------------------- LOAD
       

        public T Load<T>(string path) where T : Object
        {
            if (_cahcedResources.TryGetValue(path, out var cacheResource))
            {
                return cacheResource as T;
            }

            return Resources.Load<T>(path);
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            if (_cahcedResources.TryGetValue(path, out var cacheResource))
            {
                return cacheResource as T[];
            }

            return Resources.LoadAll<T>(path);
        }

        
        #endregion
    }
}