using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestCreateStory
{
    
    public class CameraTransform : MonoBehaviour
    {
        /*
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

        public void CameraZoom()
        {
            _cinemachineFollowZoom.m_Width = _slider.value;
        }

        */
    }
}
