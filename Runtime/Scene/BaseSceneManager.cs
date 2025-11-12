using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LumosLib
{
    public abstract class BaseSceneManager<T> : MonoBehaviour where T : BaseSceneManager<T>
    {
        #region --------------------------------------------------- UNITY


        protected virtual void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        protected virtual void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Global.Unregister<T>();
        }


        #endregion
        #region --------------------------------------------------- INIT


        protected virtual void Init()
        {
            Global.Register((T)this);
        }
        
        private IEnumerator InitAsync() 
        {
            
            if (!Project.Initialized)
            { 
                yield return Project.InitAsync();
            }

            Init();
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(InitAsync());
    
        }


        #endregion
    }
}