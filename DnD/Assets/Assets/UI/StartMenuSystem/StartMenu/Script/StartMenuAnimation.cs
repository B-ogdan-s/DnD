using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace StartMenu
{
    public class StartMenuAnimation : MonoBehaviour
    {
        [BoxGroup("Obj")]
        [SerializeField] private GameObject _name;
        [BoxGroup("Obj")]
        [SerializeField] private List<GameObject> _button;
        [BoxGroup("Tyme")]
        [SerializeField] private float _tymeName;
        [BoxGroup("Tyme")]
        [SerializeField] private float _tymeButton;
        [BoxGroup("Tyme")]
        [SerializeField] private float _tymeButtonPouse;
        [BoxGroup("Shift")]
        [SerializeField] private float _shiftName;
        [BoxGroup("Shift")]
        [SerializeField] private float _shiftButton;

        private PosInfo _namePos;
        private List<PosInfo> _buttonPos = new List<PosInfo>();

        private void Start()
        {
            GetComponent<Canvas>().enabled = true;
            float endPos = _name.transform.localPosition.y;
            _namePos = new PosInfo(endPos + _shiftName, endPos, _name);
            _namePos.Obj.transform.localPosition = new Vector3(_namePos.Obj.transform.localPosition.x, _namePos.StartPos, _namePos.Obj.transform.localPosition.z);

            foreach (var button in _button)
            {
                endPos = button.transform.localPosition.y;
                PosInfo pos = new PosInfo(endPos - _shiftButton, endPos, button);
                pos.Obj.transform.localPosition = new Vector3(pos.Obj.transform.localPosition.x, pos.StartPos, pos.Obj.transform.localPosition.z);
                _buttonPos.Add(pos);
            }
            OpenMenu();
        }

        public void OpenMenu()
        {
            _namePos.Open(_tymeName);
            StartCoroutine(Open());
        }

        [Button]
        public void CloseMenu()
        {
            _namePos.Close(_tymeName);
            StartCoroutine(Close());
        }

        private IEnumerator Open()
        {
            foreach (var button in _buttonPos)
            {
                button.Open(_tymeButton);
                yield return new WaitForSeconds(_tymeButtonPouse);
            }
        }

        private IEnumerator Close()
        {
            for(var i = _buttonPos.Count-1; i >= 0; i--)
            {
                _buttonPos[i].Close(_tymeButton);
                yield return new WaitForSeconds(_tymeButtonPouse);
            }

            yield return new WaitForSeconds(_tymeButton);
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
