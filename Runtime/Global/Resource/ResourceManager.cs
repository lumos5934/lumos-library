using UnityEngine;

namespace LumosLib.Core
{
    public class ResourceManager : BaseResourceManager, IResourceManager
    {
        #region  >--------------------------------------------------- PROPERTIES
        
        
        public override int PreInitOrder => (int)PreInitializeOrder.Resource;
        
        
        #endregion
        #region  >--------------------------------------------------- INIT


        public override void PreInit()
        {
            base.PreInit();
            
            
            PreInitialized = true;
        }
        
        
        #endregion
        #region  >--------------------------------------------------- LOAD


        public T Load<T>(string path) where T : Object
        {
            if (cahcedResources.TryGetValue(path, out var cacheResource))
            {
                return cacheResource as T;
            }

            return Resources.Load<T>(path);
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            if (cahcedResources.TryGetValue(path, out var cacheResource))
            {
                return cacheResource as T[];
            }

            return Resources.LoadAll<T>(path);
        }

        #endregion

    }
}