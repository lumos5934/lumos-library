using System;
using System.Collections.Generic;
using UnityEngine;

namespace LumosLib
{
    public class UIManager : MonoBehaviour, IUIManager, IPreInitialize
    {
        #region >--------------------------------------------------- PROPERTIE
        
        
        public int PreInitOrder => (int)PreInitializeOrder.UI;
        public bool PreInitialized => true;
        
        
        #endregion
        #region >--------------------------------------------------- FIELDS


        protected Dictionary<Type, UIBase> _ui = new();
        protected Dictionary<Type, UIBase> _globalUIPrefabs = new();
        


        #endregion
        #region >--------------------------------------------------- UNITY

        protected virtual void Awake()
        {
            
            var uiGlobalPrefabs =  Global.GetInternal<IResourceManager>().LoadAll<UIBase>("");

            for (int i = 0; i < uiGlobalPrefabs.Length; i++)
            {
                var value = uiGlobalPrefabs[i];

                _globalUIPrefabs[uiGlobalPrefabs[i].GetType()] = value;
            }
            
            Global.Register<IUIManager>(this);
            
            DontDestroyOnLoad(gameObject);
        }

        #endregion
        #region >--------------------------------------------------- GET & SET


        public void SetEnable<T>(bool enable) where T : UIBase
        {
            var ui = Get<T>();

            if (ui == null) return;

            ui.SetEnable(enable);
        }
        
        public void SetToggle<T>() where T : UIBase
        {
            var ui = Get<T>();

            if (ui == null) return;

            ui.SetEnable(!ui.IsEnabled);
        }

        public virtual T Get<T>() where T : UIBase
        {
            if (_ui.TryGetValue(typeof(T), out var ui))
            {
                return ui as T;
            }
            else
            {
                return CreateUI<T>();
            }
        }
        
        private T CreateUI<T>() where T : UIBase
        {
            var type = typeof(T);
            
            if (!_globalUIPrefabs.TryGetValue(type, out var prefab)) return null;

            var createdUI = Instantiate(prefab, transform);
            
            _ui[type] = createdUI;
            
            createdUI.gameObject.SetActive(false);
            
            return createdUI as T;
        }


        #endregion
    }
}