using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UI.CreateStoryUI
{
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] _buttons;
        [SerializeField] private float _time;
        [SerializeField] private float _timePouse;
        [SerializeField] private float _shift;

        private PosInfo[] _posInfo;

        private void Start()
        {
            _posInfo = new PosInfo[_buttons.Length];
            for (int i = 0; i < _buttons.Length; i++)
            {
                float posX = _buttons[i].transform.localPosition.x;
                _posInfo[i] = new PosInfo(posX + _shift, posX, _buttons[i]);
                _buttons[i].transform.localPosition += new Vector3(_shift, 0, 0);
            }
        }

        public void Open()
        {
            StartCoroutine(CR_Open());
        }
        public void Close()
        {
            StartCoroutine(CR_Close());
        }

        private IEnumerator CR_Open()
        {
            for(int i = 0; i < _posInfo.Length; i++)
            {
                _posInfo[i].Open(_time);
                yield return new WaitForSeconds(_timePouse);
            }
        }
        private IEnumerator CR_Close()
        {
            for (int i = 0; i < _posInfo.Length; i++)
            {
                _posInfo[i].Close(_time);
                yield return new WaitForSeconds(_timePouse);
            }
        }

        private class PosInfo
        {
            private float _startPos;
            private float _endPos;
            private GameObject _obj;

            public float StartPos => _startPos;
            public GameObject Obj => _obj;

            public PosInfo(float startPos, float endPos, GameObject obj)
            {
                _startPos = startPos;
                _endPos = endPos;
                _obj = obj;
            }

            public void Open(float tyme)
            {
                _obj.transform.DOLocalMoveX(_endPos, tyme).SetEase(Ease.Linear);
            }
            public void Close(float tyme)
            {
                _obj.transform.DOLocalMoveX(_startPos, tyme).SetEase(Ease.Linear);
            }
        }
    }
}
