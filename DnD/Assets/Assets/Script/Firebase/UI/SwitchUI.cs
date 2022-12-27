using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirebaseUI
{
    public class SwitchUI : MonoBehaviour
    {
        [SerializeField] private ButtonUI _registerButtom;
        [SerializeField] private ButtonUI _loginButtom;

        [SerializeField] private Transform _underline;
        [SerializeField] private float _underlineScaleValue;
        [SerializeField] private float _buttomWidth;
        [SerializeField, MinValue(0f)] private float _switchingTime;

        private float _linePos;
        private Coroutine _coroutine;

        private void Start()
        {
            _registerButtom.StartSettings(_switchingTime, true);
            _registerButtom._OnClick += Switch;
            _loginButtom.StartSettings(_switchingTime, false);
            _loginButtom._OnClick += Switch;
        }

        private void Switch(float newPos)
        {
            _registerButtom.DisableButtom();
            _loginButtom.DisableButtom();

            _linePos = newPos;
            ErrorUI._instance.SetError("");
            
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(UnderlineAnimation());
        }

        private void OnDestroy()
        {
            _registerButtom._OnClick -= Switch;
            _loginButtom._OnClick -= Switch;
        }

        private IEnumerator UnderlineAnimation()
        {
            DOTween.Sequence()
                .Join(_underline.DOLocalMoveX(_linePos + _buttomWidth/2, _switchingTime).SetEase(Ease.Linear))
                .Join(_underline.DOScaleX(_underlineScaleValue, _switchingTime/2f).SetEase(Ease.Linear));
            yield return new WaitForSeconds(_switchingTime / 2f);
            DOTween.Sequence()
                .Join(_underline.DOScaleX(1f, _switchingTime / 2f).SetEase(Ease.Linear));
        }
    }
}
