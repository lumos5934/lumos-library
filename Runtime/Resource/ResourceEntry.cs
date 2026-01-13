using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;

namespace LumosLib
{
    [System.Serializable]
    public class ResourceEntry
    {
        public string Label => _label;
        public string FolderPath => _folderPath;
        public List<Object> Resources => _resources;
        
        [SerializeField] private string _label;
        [SerializeField] private string _folderPath;
        [SerializeField, ReadOnly] private List<Object> _resources = new();

        private Dictionary<string, Object> _resourcesDict;

        public void Init()
        {
            _resourcesDict = _resources.ToDictionary(item => item.name, item => item);
        }
        
        public Object GetResource(string resourceName)
        {
            _resourcesDict.TryGetValue(resourceName, out var resource);
            
            return resource;
        }

        public void SetResources(List<Object> resources)
        {
            _resources = resources;
        }
    }
}