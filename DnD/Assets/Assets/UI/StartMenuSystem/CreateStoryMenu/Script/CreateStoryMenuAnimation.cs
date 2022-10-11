using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace StartMenu
{
    public class CreateStoryMenuAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private float _tyme;
        [SerializeField] private float _shift;

        private PosInfo _panelInfo;

        private void Start()
        {
            GetComponent<Canvas>().enabled = true;
            _panelInfo = new PosInfo(_panel.transform.localPosition.y - _shift, _panel.transform.localPosition.y, _panel);
            _panel.transform.localPosition -= new Vector3(0, _shift, 0);
            GetComponent<Canvas>().enabled = false;
        }

        public void OpenPanel()
        {
            GetComponent<Canvas>().enabled = true;
            _panelInfo.Open(_tyme);
        }

        public void ClosePanel()
        {
            StartCoroutine(CR_ClosePanel());
        }

        private IEnumerator CR_ClosePanel()
        {
            _panelInfo.Close(_tyme);
            yield return new WaitForSeconds(_tyme);
            GetComponent<Canvas>().enabled = false;
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
                _obj.transform.DOLocalMoveY(_endPos, tyme).SetEase(Ease.OutBack);
            }
            public void Close(float tyme)
            {
                _obj.transform.DOLocalMoveY(_startPos, tyme).SetEase(Ease.InBack);
            }
        }
    }
}
