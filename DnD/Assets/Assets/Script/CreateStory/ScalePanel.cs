using System;
using UnityEngine;
using CreateStory;
using Sirenix.OdinInspector;
using System.Collections;

namespace TestCreateStory
{
    public class ScalePanel : MonoBehaviour, ITouch
    {
        [SerializeField] private int _maxScaleNum;
        [SerializeField] private Transform _line;
        [SerializeField, Min(0f)] private float _slideValue;
        [SerializeField, Min(0f)] private float _timeReturn;
        [SerializeField, ReadOnly] private int _num;
        [SerializeField, ReadOnly] private Vector3 _position = Vector3.zero;

        private ScalePanelLogic _scaleLogic;

        public event Action<ScalePanel, Vector3, int> _updateTransformPanel;
        public event Action<int, Vector2Int> _returnSize;

        private void Awake()
        {
            _scaleLogic = new ScalePanelLogic(transform, _line);
        }

        public void StartSettings(Vector3 startPos, Vector3 endPos, Vector3Int vector, int rot, int num, float size)
        {
            _num = num / 2;

            if(num % 2 == 0 && (vector.x > 0 || vector.z > 0))
            {
                _num--;
            }

            transform.eulerAngles = rot * Vector3.up;

            _scaleLogic.StartSettings(startPos, endPos, vector, size);
        }

        public void TouchDown()
        {
            _position = transform.position;
        }

        public void TouchHolding(Vector3 touchPos)
        {
            var value = _scaleLogic.TouchUpdate(touchPos);
            _updateTransformPanel?.Invoke(this, value.Item1, value.Item2);

            UpdatePosition();
        }

        public void TouchUp()
        {
            StartCoroutine(ReturnPos());

            _position = Vector3.zero;
        }

        public void UpdateTransform(Vector3 startPos, Vector3 endPos)
        {
            _scaleLogic.UpdateTransform(startPos, endPos);
        }

        private void UpdatePosition()
        {
            var vector = _scaleLogic.UpdatePosition(_position, _slideValue);

            if (vector.Item1 > 0 && _num < _maxScaleNum)
            {
                _num++;
                _returnSize?.Invoke(1, new Vector2Int(vector.Item3.x, vector.Item3.z));
            }
            else if (vector.Item1 < 0 && _num > 0)
            {
                _num--;
                _returnSize?.Invoke(-1, new Vector2Int(vector.Item3.x, vector.Item3.z));
            }
            else
                return;

            _position += vector.Item2;

        }

        private IEnumerator ReturnPos()
        {
            Vector3 delta = (_position - transform.position) / 10f;

            int num = 0;

            while(num < 10)
            {
                num++;

                var value = _scaleLogic.TouchUpdate(delta);
                _updateTransformPanel?.Invoke(this, value.Item1, value.Item2);

                yield return new WaitForSeconds(_timeReturn / 10f);
            }
        }
    }
}
