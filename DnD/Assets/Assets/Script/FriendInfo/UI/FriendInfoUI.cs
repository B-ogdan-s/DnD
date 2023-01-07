using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FriendInfo
{
    public class FriendInfoUI : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] private float _shift;

        private Transform _content;
        private CanvasGroup _canvasGroup;

        private float _startPos;
        private float _endPos;

        private Coroutine _coroutine;

        private void Start()
        {
            _content = GetComponent<Transform>();
            _canvasGroup = GetComponent<CanvasGroup>();

            _startPos = _content.localPosition.x + _shift;
            _endPos = _content.localPosition.x;

            _content.DOLocalMoveX(_startPos, 0);
            _canvasGroup.alpha = 0;
        }

        public void PanelOpen()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _canvasGroup.alpha = 1;
            _content.DOLocalMoveX(_endPos, _time);
        }

        private void PanelClose()
        {
            _coroutine = StartCoroutine(CR_PanelClose());
        }
        private IEnumerator CR_PanelClose()
        {
            _content.DOLocalMoveX(_startPos, _time);
            yield return new WaitForSeconds(_time);
            _canvasGroup.alpha = 0;
        }
    }
}