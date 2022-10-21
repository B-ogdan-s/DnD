using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UI.CreateStoryUI
{
    public class StartSettingsPanelUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _panel;
        [SerializeField] private float _time;
        [SerializeField] private float _shift;

        private PosInfo _posInfo;

        private void Start()
        {
            _canvas.enabled = true;
            _posInfo = new PosInfo(_panel.transform.localPosition.y - _shift, _panel.transform.localPosition.y, _panel);
            _panel.transform.localPosition -= new Vector3(0, _shift, 0);
            _canvas.enabled = false;
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
            _canvas.enabled = true;
            _posInfo.Open(_time);
            yield return null;
        }
        private IEnumerator CR_Close()
        {
            _posInfo.Close(_time);
            yield return new WaitForSeconds(_time);
            _canvas.enabled = false;
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
                _obj.transform.DOLocalMoveY(_endPos, tyme).SetEase(Ease.Linear);
            }
            public void Close(float tyme)
            {
                _obj.transform.DOLocalMoveY(_startPos, tyme).SetEase(Ease.Linear);
            }
        }
    }
}
