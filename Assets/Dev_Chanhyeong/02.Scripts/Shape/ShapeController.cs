using System;
using Chanhyeong.Decorate;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

namespace Chanhyeong
{
    public class ShapeController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Func<bool> onInteractCondition;
        public event Action<Vector3, Vector3> onPointerDown;
        public event Action<Vector3, Vector3> onBeginDrag;
        public event Action<Vector3, Vector3> onDrag;
        public event Action<Vector3, Vector3> onEndDrag;

        private bool InteractionCondition
        {
            get
            {
                var interactCondition = onInteractCondition?.Invoke();

                return interactCondition.GetValueOrDefault(true);
            }
        }

        private Vector2 _startPoint;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!InteractionCondition) return;
            
            _startPoint = InputManager.MousePosition;
            onPointerDown?.Invoke(InputManager.MousePosition, InputManager.MousePosition);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!InteractionCondition) return;
            
            onBeginDrag?.Invoke(InputManager.MousePosition, InputManager.MousePosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!InteractionCondition) return;
            
            onDrag?.Invoke(_startPoint, InputManager.MousePosition);
        }
 
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!InteractionCondition) return;
            
            onEndDrag?.Invoke(_startPoint, InputManager.MousePosition);
            _startPoint = Vector2.zero;
        }
    }
}