using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreateStory;

namespace TestCreateStory
{
    public class ScalePanel : MonoBehaviour, ITouch
    {
        [SerializeField] private int _maxScaleNum;
        [SerializeField] private Transform _line;
        [SerializeField, Min(0f)] private float _slideValue;
        [SerializeField, Min(0f)] private float _timeReturn;
        private ScalePanelLogic _scaleLogic;

        public event Action<ScalePanel, Vector3, Vector3> _updateScalePanel;
        public event Action<ScalePanel, Vector3Int, Vector3> _updatePos;
        public event Action<Vector3Int, int> _updateScale;

        private void Awake()
        {
            _scaleLogic = new ScalePanelLogic(transform, _line);
        }

        public void TouchDown()
        {
            _scaleLogic.SetPosition();
        }

        public void TouchHolding(Vector3 touchPos)
        {
            var value = _scaleLogic.PanelMove(touchPos, _slideValue);


            transform.position += value.Item1;

            _updateScalePanel?.Invoke(this, value.Item2, value.Item3);

            //_updatePos?.Invoke(this, value.Item1, value.Item2);
            //_updateScale?.Invoke(value.Item1, value.Item3);
        }

        public void TouchUp()
        {
            StartCoroutine(ReturnPos());
        }

        public void StartSettings(Vector3 startPos, Vector3 endPos, Vector3Int vector, int rot, int num, float size)
        {
            transform.eulerAngles = rot * Vector3.up;

            _scaleLogic.StartSettings(startPos, endPos, vector, num, size);
        }

        public void UpdatePos(Vector3 startPos, Vector3 endPos)
        {
            _scaleLogic.UpdatePos(startPos, endPos);
        }

        private IEnumerator ReturnPos()
        {
            var value = _scaleLogic.ReturnPos();

            transform.DOMove(value.Item1, _timeReturn).SetEase(Ease.Linear);

            for(int i = 0; i < 5; i++)
            {
                _updateScalePanel?.Invoke(this, value.Item2, value.Item3);
                yield return new WaitForSeconds(_timeReturn / 5f);
            }
        }
    }
}
