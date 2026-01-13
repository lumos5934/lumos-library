using System.Collections.Generic;
using UnityEngine;

namespace LumosLib
{
    public interface IResourceManager
    {
        public T Load<T>(string path) where T : Object;
        public T Load<T>(string label, string path) where T : Object;
        public List<T> LoadAll<T>(string label) where T : Object;
    }
}