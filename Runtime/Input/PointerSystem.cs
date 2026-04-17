using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace LLib
{
    public class PointerSystem : IDisposable
    {
        private InputActionReference _posReference;
        private InputActionReference _clickReference;
        private PointerEventData _eventData;
        private readonly List<RaycastResult> _eventCastResults = new();

        public Vector2 Position { get; private set; }
        public bool IsDown { get; private set; }
        public bool IsHold { get; private set; }
        public bool IsUp { get; private set; }


        public PointerSystem(InputActionReference posRef, InputActionReference clickRef)
        {
            _posReference = posRef ?? throw new ArgumentNullException(nameof(posRef));
            _posReference.action.Enable();
            
            _clickReference = clickRef ?? throw new ArgumentNullException(nameof(clickRef));
            _clickReference.action.Enable();
            
            InputSystem.onAfterUpdate += OnInputUpdate;
        }

        
        public bool IsOverEventSystem()
        {
            if (EventSystem.current == null) 
                return false;

            if (_eventData == null)
            {
                _eventData = new PointerEventData(EventSystem.current);
            }
            
            _eventData.position = Position;
            
            _eventCastResults.Clear();
            
            EventSystem.current.RaycastAll(_eventData, _eventCastResults);

            return _eventCastResults.Count > 0;
        }

        
        private void OnInputUpdate()
        {
            Position = _posReference.action.ReadValue<Vector2>();

            IsDown = _clickReference.action.WasPressedThisFrame();
            IsHold = _clickReference.action.IsPressed();
            IsUp = _clickReference.action.WasReleasedThisFrame();
        }

        public void Dispose()
        {
            InputSystem.onAfterUpdate -= OnInputUpdate;
        }
    }
}