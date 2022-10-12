using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UI.StartMenu
{
    public class StoryMenuAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _topPanel;
        [SerializeField] private GameObject _bottomPanel;
        [SerializeField] private float _time;
        [SerializeField] private float _shift;

        private PosInfo _topPanelInfo;
        private PosInfo _bottomPanelInfo;

        private void Start()
        {
            GetComponent<Canvas>().enabled = true;
            _topPanelInfo = new PosInfo(_topPanel.transform.localPosition.y + _shift, _topPanel.transform.localPosition.y, _topPanel);
            _bottomPanelInfo = new PosInfo(_bottomPanel.transform.localPosition.y - _shift, _bottomPanel.transform.localPosition.y, _bottomPanel);
            _topPanel.transform.localPosition += new Vector3(0, _shift, 0);
            _bottomPanel.transform.localPosition -= new Vector3(0, _shift, 0);
            GetComponent<Canvas>().enabled = false;
        }

        public void Open()
        {
            GetComponent<Canvas>().enabled = true;
            _topPanelInfo.Open(_time);
            _bottomPanelInfo.Open(_time);
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
