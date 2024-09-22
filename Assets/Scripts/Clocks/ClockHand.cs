using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Clocks
{
    public class ClockHand : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private bool _isDragging = false;

        [SerializeField] private float _rotationStep = 6f;

        private float _currentRotation;
        public float CurrentRotation => _currentRotation;
        public event Action UptadeRotation;

        public void OnBeginDrag(PointerEventData eventData) =>
            _isDragging = true;

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging)
                RotateHand(eventData);
        }

        public void OnEndDrag(PointerEventData eventData) =>
            _isDragging = false;

        private void RotateHand(PointerEventData eventData)
        {
            _currentRotation = transform.localEulerAngles.z;

            if (eventData.delta.y > 0)
                _currentRotation += _rotationStep;
            else if (eventData.delta.y < 0)
                _currentRotation -= _rotationStep;

            _currentRotation = _currentRotation % 360;

            if (_currentRotation < 0) 
                _currentRotation += 360;

            transform.localEulerAngles = new Vector3(0, 0, _currentRotation);

            UptadeRotation?.Invoke();
        }
    }
}
