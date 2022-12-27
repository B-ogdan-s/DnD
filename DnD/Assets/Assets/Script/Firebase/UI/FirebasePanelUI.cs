using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace FirebaseUI
{
    public class FirebasePanelUI : MonoBehaviour
    {
        [SerializeField] private Image _fadePanel;
        [SerializeField] private Transform _content;
        [SerializeField] private float _time;
        [SerializeField] private float _shift;
        [SerializeField] private float _fadeValue;

        private Canvas _canvas;

        private float _startPos;
        private float _endPos;

        private Coroutine _closeCoroutine;

        private void Start()
        {
            _startPos = _content.localPosition.y + _shift;
            _endPos = _content.localPosition.y;
            _canvas = GetComponent<Canvas>();
            _fadePanel.DOFade(0, 0);
            _content.DOLocalMoveY(_startPos, 0);
            _canvas.enabled = false;

            Auth._CloseFirenase += ClosePanel;
            Auth._OpenFirebase += OpenPanel;
        }

        public void OpenPanel()
        {
            if(_closeCoroutine != null)
            {
                StopCoroutine(_closeCoroutine);
            }    
            _canvas.enabled = true;
            _fadePanel.DOFade(_fadeValue, _time);
            _content.DOLocalMoveY(_endPos, _time);
        }
        public void ClosePanel()
        {
            _closeCoroutine = StartCoroutine(CR_ClosePanel());
        }

        private IEnumerator CR_ClosePanel()
        {
            _fadePanel.DOFade(0, _time);
            _content.DOLocalMoveY(_startPos, _time);
            yield return new WaitForSeconds(_time);
            _canvas.enabled = false;
        }

        private void OnDestroy()
        {
            Auth._CloseFirenase -= ClosePanel;
            Auth._OpenFirebase -= OpenPanel;
        }
    }
}
