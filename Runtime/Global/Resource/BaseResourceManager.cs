using System.Collections.Generic;
using UnityEngine;

namespace LumosLib.Core
{
    public abstract class BaseResourceManager : MonoBehaviour, IPreInitialize
    {
        #region  >--------------------------------------------------- PROPERTIE

        
        public int PreID => (int)PreInitializeOrder.Resource;
        public abstract int PreInitOrder { get; }
        public bool PreInitialized { get; protected set; }
        
        
        #endregion
        #region  >--------------------------------------------------- FIELD


        protected Dictionary<string, object> cahcedResources = new();
        
        
        #endregion
        #region  >--------------------------------------------------- INIT


        public virtual void PreInit()
        {
            Global.Register((IResourceManager)this);
        }
        
        
        #endregion
    }
}