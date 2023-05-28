using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestCreateStory
{
    public class CreateStoryTouch : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private LayerMask _layerMask;

        private List<PointerEventData> _pointers = new List<PointerEventData>();
        private int _pointersCount => _pointers.Count;


        private ITouch _iTouch;
        private Vector3 _oldTouchPos;

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointers.Add(eventData);

            if (_pointersCount == 1)
            {
                Ray ray = Camera.main.ScreenPointToRay(eventData.position);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
                {
                    ITouch touch = hit.transform.GetComponentInParent<ITouch>();

                    if (touch != null)
                    {
                        Debug.Log(touch);
                        _iTouch = touch;
                    }
                }

                _iTouch?.TouchDown();

                _oldTouchPos = hit.point;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
            {
                if (hit.transform.TryGetComponent(out ITouch touch))
                {
                    _iTouch = touch;
                }

                if (_iTouch != null)
                {
                    _iTouch.TouchUp();
                    _iTouch = null;
                }
                _pointers.Remove(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
            {
                Vector3 newPos = hit.point;

                if (_iTouch != null)
                {
                    _iTouch.TouchHolding(newPos - _oldTouchPos);
                }
                _oldTouchPos = newPos;
            }
        }
    }
}
