using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

namespace CreateStory
{
    public class TransformController : MonoBehaviour, IPointerEnterHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Button _transButton;
        [SerializeField] private Button _rotButton;
        [SerializeField] private Button _scaleButton;

        [SerializeField] private GameObject _cam;
        [SerializeField] private GameObject _camScale;

        [SerializeField] private float _speedMove;
        [SerializeField] private float _speedRot;
        [SerializeField] private float _speedScale;

        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;

        [SerializeField, MinValue(1)] private float _attenuation;

        private ButtonType _type = ButtonType.Transform;
        private Vector2 _startTouch;
        private Coroutine _coroutine;

        private int _numX;
        private int _numZ;

        private void Start()
        {
            _transButton.onClick.AddListener(() =>
            {
                _type = ButtonType.Transform;
                _transButton.interactable = false;
                _rotButton.interactable = true;
                _scaleButton.interactable = true;
            });
            _rotButton.onClick.AddListener(() =>
            {
                _type = ButtonType.Rotate;
                _transButton.interactable = true;
                _rotButton.interactable = false;
                _scaleButton.interactable = true;
            });
            _scaleButton.onClick.AddListener(() =>
            {
                _type = ButtonType.Scale;
                _transButton.interactable = true;
                _rotButton.interactable = true;
                _scaleButton.interactable = false;
            });
            _transButton.interactable = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(Input.touches.Length > 0)
                _startTouch = Input.touches[0].position;

            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
        public void OnDrag(PointerEventData eventData)
        {
            switch(_type)
            {
                case ButtonType.Transform:
                    Transform();
                    break;
                case ButtonType.Rotate:
                    Rotation();
                    break;
                case ButtonType.Scale:
                    Scale();
                    break;
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            switch (_type)
            {
                case ButtonType.Transform:
                    _coroutine = StartCoroutine(CR_Transform(Input.touches[0].position));
                    break;
                case ButtonType.Rotate:
                    _coroutine = StartCoroutine(CR_Rotation(Input.touches[0].position));
                    break;
            }
        }

        private void Transform()
        {
            Vector2 endPos = Input.touches[0].position;
            endPos -= _startTouch;

            Vector3 pos = endPos.y * -_cam.transform.forward + endPos.x * -_cam.transform.right;

            if(CheckTransform(pos))
                _cam.transform.localPosition += new Vector3(pos.x * _speedMove * Time.deltaTime, 0, pos.z * _speedMove * Time.deltaTime);

            _startTouch += endPos;
        }
        private void Rotation()
        {
            Vector2 endPos = Input.touches[0].position;
            Vector2 pos = endPos - _startTouch;

            _cam.transform.localEulerAngles += new Vector3(0, pos.x * _speedRot * Time.deltaTime, 0);

            _startTouch = endPos;
        }
        private void Scale()
        {
            Vector2 endPos = Input.touches[0].position;
            endPos -= _startTouch;
            Vector3 pos = endPos.y * _camScale.transform.forward;

            if (CheckScale(pos))
                _camScale.transform.position += pos * Time.deltaTime * _speedScale;

            _startTouch += endPos;
        }

        private IEnumerator CR_Transform(Vector2 endPos)
        {
            Vector3 newPos = new Vector3(endPos.x - _startTouch.x, 0, endPos.y - _startTouch.y);
            newPos = newPos.z * -_cam.transform.forward + newPos.x * -_cam.transform.right;

            while (Mathf.Abs(newPos.x) > 0.01f || Mathf.Abs(newPos.y) > 0.01f)
            {
                if (!CheckTransform(newPos))
                    break;
                    
                _cam.transform.localPosition += new Vector3(newPos.x * _speedMove * Time.deltaTime, 0, newPos.z * _speedMove * Time.deltaTime);
                newPos -= newPos / _attenuation;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            yield return null;
        }
        private IEnumerator CR_Rotation(Vector2 endPos)
        {
            Vector2 newPos = endPos - _startTouch;

            while (Mathf.Abs(newPos.x) > 0.01f || Mathf.Abs(newPos.y) > 0.01f)
            {
                _cam.transform.localEulerAngles += new Vector3(0, newPos.x * _speedRot * Time.deltaTime, 0);
                newPos -= newPos / _attenuation;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            yield return null;
        }
        private bool CheckTransform(Vector3 pos)
        {
            Vector3 check = _cam.transform.localPosition + new Vector3(pos.x * _speedMove * Time.deltaTime, 0, pos.z * _speedMove * Time.deltaTime);
            float sizeX = _numX * 1.5f / 2f;
            float sizeZ = _numZ * 1.5f / 2f;

            Debug.Log("X: " + sizeX + "; Z: " + sizeZ + "; Pos: " + check);

            return (check.x >= -sizeX && check.x <= sizeX && check.z >= -sizeZ && check.z <= sizeZ);
        }
        private bool CheckScale(Vector3 pos)
        {
            Vector3 check = _camScale.transform.position;
            check += pos * Time.deltaTime * _speedScale;
            check -= _cam.transform.position;

            float distans = Mathf.Sqrt(check.x * check.x + check.y * check.y + check.z * check.z);

            return distans <= _maxScale && distans >= _minScale;
        }

        public void CreatePole(int x, int z)
        {
            _numX = x;
            _numZ = z;
        }

        private enum ButtonType
        {
            Transform,
            Rotate,
            Scale
        }
    }
}
