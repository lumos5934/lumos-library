using UnityEngine;

namespace LumosLib
{
    public abstract class BaseSceneManager<T> : MonoBehaviour where T : BaseSceneManager<T>
    {
        #region --------------------------------------------------- UNITY


        protected virtual void Awake()
        {
            GlobalService.Register((T)this);
        }

        protected virtual void OnDestroy()
        {
            GlobalService.Unregister<T>();
        }


        #endregion
    }
}