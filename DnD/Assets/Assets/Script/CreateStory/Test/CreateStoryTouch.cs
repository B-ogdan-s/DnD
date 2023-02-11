using Cinemachine;
using TestCreateStory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TestCreateStory
{
    public class CreateStoryTouch : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Transform _cameraTransform;

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
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.GetComponentInParent<ITouch>() != null)
                    {
                        _iTouch = hit.transform.GetComponentInParent<ITouch>();
                    }
                }
                if(_iTouch != null)
                    _iTouch.TouchDown();

                _oldTouchPos = hit.point;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_iTouch != null)
            {
                _iTouch.TouchUp();
                _iTouch = null;
            }
            _pointers.Remove(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 newPos = hit.point;

                if (_iTouch != null)
                {
                    _iTouch.TouchHolding(newPos - _oldTouchPos);
                }
                _oldTouchPos = newPos;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {

        }
    }
}
