using UnityEngine;
using UnityEngine.Events;

namespace LumosLib
{
    public interface IPointerManager
    {
        public bool GetOverUI();
        public Vector2 GetPos();
        public GameObject GetScanObject(bool ignoreUI);
        
        public event UnityAction OnDown;
        public event UnityAction OnHold;
        public event UnityAction OnUp;
    }
}