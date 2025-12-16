using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace LumosLib
{
    public class PointerManager : MonoBehaviour, IPreInitializable, IPointerManager
    {
        #region >--------------------------------------------------- FIELD


        [SerializeField] private InputActionReference _posInputReference;
        [SerializeField] private InputActionReference _clickInputReference;

        private bool _isOverUI;
        private Vector2 _pos;
        private GameObject _scanObj;
        private Coroutine _clickCoroutine;
        
        
        #endregion
        #region >--------------------------------------------------- EVENT

        
        public event UnityAction OnDown;
        public event UnityAction OnHold;
        public event UnityAction OnUp;
        
        
        #endregion
        #region >--------------------------------------------------- UNITY
        
        
        private void LateUpdate()
        {
            _isOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }


        #endregion
        #region >--------------------------------------------------- INIT
        
        
        public IEnumerator InitAsync()
        {
            var pointerClickRef = _clickInputReference;
            if (pointerClickRef != null)
            {
                pointerClickRef.action.started += StartedPointerDown;
                pointerClickRef.action.canceled += CanceledPointerDown;
                pointerClickRef.action.actionMap.Enable(); 
            }
            
            var pointerPosRef = _posInputReference;
            if (pointerPosRef != null)
            {
                pointerPosRef.action.performed += SetPointerPos;
                pointerPosRef.action.actionMap.Enable(); 
            }
            
            GlobalService.Register(this);
            DontDestroyOnLoad(gameObject);
            
            yield break;
        }
        
        
        #endregion
        #region >--------------------------------------------------- SET


        public bool GetOverUI()
        {
            return _isOverUI;
        }
        
        public Vector2 GetPos()
        {
            return _pos;
        }

        public GameObject GetScanObject(bool ignoreUI = true)
        {
            if (GetOverUI() && !ignoreUI)  return null;

            var cam = Camera.main;
            if (cam == null) return null;
            
            Vector2 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }
            
            return hit.collider != null ? hit.collider.gameObject : null;
        }
        
        private void SetPointerPos(InputAction.CallbackContext context)
        {
            _pos = context.ReadValue<Vector2>();
        }
        
        
        #endregion
        #region >--------------------------------------------------- CORE

        
        private void StartedPointerDown(InputAction.CallbackContext context)
        {
            _clickCoroutine = StartCoroutine(PointerClickCoroutine());
            
            OnDown?.Invoke();
        }
        
        private void CanceledPointerDown(InputAction.CallbackContext context)
        {
            if (_clickCoroutine != null)
            {
                StopCoroutine(_clickCoroutine);
            }

            OnUp?.Invoke();
        }

        private IEnumerator PointerClickCoroutine()
        {
            while (true)
            {
                yield return null;
                
                OnHold?.Invoke();
            }
        }
        
        
        #endregion
    }
}