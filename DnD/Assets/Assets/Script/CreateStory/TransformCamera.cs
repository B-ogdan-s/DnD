using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransformCamera : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Button _transformButton;
    [SerializeField] private Button _rotButton;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CinemachineFollowZoom _cinemachineFollowZoom;
    [SerializeField] private Slider _slider;

    [SerializeField] private float _transformSpeed;
    [SerializeField] private float _rotationSpeed;

    private List<PointerEventData> _pointers = new List<PointerEventData>();
    private int _pointersCount => _pointers.Count;

    private Vector2 _oldPos;

    private event Action<Vector2> _touchAction;

    private void Start()
    {
        _cinemachineFollowZoom.m_Width = _slider.value;
        _transformButton.interactable = false;

        _touchAction += CameraMove;

        _transformButton.onClick.AddListener(() =>
        {
            _rotButton.interactable = true;
            _transformButton.interactable = false;

            _touchAction -= CameraRot;
            _touchAction += CameraMove;
        });
        _rotButton.onClick.AddListener(() =>
        {
            _transformButton.interactable = true;
            _rotButton.interactable = false;

            _touchAction -= CameraMove;
            _touchAction += CameraRot;
        });
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _pointers.Add(eventData);

        if(_pointersCount == 1)
            _oldPos = _pointers[0].position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _touchAction(_pointers[0].position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _pointers.Remove(eventData);
    }

    private void CameraMove(Vector2 newPos)
    {
        Vector2 delta = -(newPos - _oldPos);
        _cameraTransform.position += (delta.y * _cameraTransform.forward + delta.x * _cameraTransform.right) * Time.deltaTime * _slider.value / 20f * _transformSpeed;
        _oldPos = newPos;
    }
    private void CameraRot(Vector2 newPos)
    {
        Vector2 delta = (newPos - _oldPos);
        _cameraTransform.localEulerAngles += delta.x * Time.deltaTime * Vector3.up * _rotationSpeed;
        _oldPos = newPos;
    }

    private float MathRot(Vector2 pos1, Vector2 pos2)
    {
        Vector2 a = pos2 - pos1;
        Debug.Log(a);
        return a.x;
    }

    public void CameraZoom()
    {
        _cinemachineFollowZoom.m_Width = _slider.value;
    }
}
