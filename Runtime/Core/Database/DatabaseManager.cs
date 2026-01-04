using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BansheeGz.BGDatabase;
using UnityEngine;

namespace LumosLib
{
    public class DatabaseManager : MonoBehaviour, IPreInitializable, IDatabaseManager
    {
        #region >--------------------------------------------------- FIELD
        
       
        private Dictionary<Type, Dictionary<int, BaseData>> _dataDict = new();
        
        
        #endregion
        #region >--------------------------------------------------- UNITY


        private void Awake()
        {
            GlobalService.Register<IDatabaseManager>(this);
        }
        
        
        #endregion
        #region >--------------------------------------------------- INIT

        
        public IEnumerator InitAsync(Action<bool> onComplete)
        {
            onComplete?.Invoke(true);
            yield break;
        }
         
        
        #endregion
        #region >--------------------------------------------------- REGISTER


        public void RegisterData<T>() where T : BaseData
        {
            var type = typeof(T);
            var meta = BGRepo.I[type.Name];
            
            _dataDict[type] = new Dictionary<int, BaseData>();
            
            foreach (var entity in  meta.EntitiesToList())
            {
                var instance = (BaseData)Activator.CreateInstance(typeof(T), entity);
                
                _dataDict[type][instance.TableID] = instance;
            }
        }
        
        
        #endregion
        #region >--------------------------------------------------- GET


        public List<T> GetAll<T>() where T : BaseData
        {
            if (_dataDict.TryGetValue(typeof(T), out var dict))
            {
                return dict.Values.Cast<T>().ToList();
            }

            DebugUtil.LogError($" haven't data '{typeof(T).Name}' ", " GET FAIL ");
            return null;
        }
        
        public T Get<T>(int tableID) where T : BaseData
        {
            if (_dataDict.TryGetValue(typeof(T), out var dict))
            {
                if (dict.TryGetValue(tableID, out var value))
                {
                    return value as T;
                }
            }

            DebugUtil.LogError($" haven't data '{typeof(T).Name}' ", " GET FAIL ");
            return null;
        }
        
        
        #endregion
    }
}