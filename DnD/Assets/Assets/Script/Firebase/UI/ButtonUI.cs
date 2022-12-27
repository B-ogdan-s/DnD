using System;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace FirebaseUI
{
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _content;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField, MinValue(0f), MaxValue(1f)] private float _enableFade = 0;

        private float _time = 0;
        private Button _button;

        public event Action<float> _OnClick;

        public void StartSettings(float time, bool startEnable)
        {
            _button = GetComponent<Button>();
            _time = time;
            if(startEnable)
            {
                _content.alpha = 1;
                _content.blocksRaycasts = true;
                _buttonText.DOFade(1, 0);
                _button.interactable = false;
            }
            else
            {
                _content.alpha = 0;
                _content.blocksRaycasts = false;
                _buttonText.DOFade(_enableFade, 0);
            }
        }

        public void ClickButtom()
        {
            _OnClick?.Invoke(transform.localPosition.x);
            _buttonText.DOFade(1, _time);
            _button.interactable = false;
            _content.DOFade(1, _time);
            _content.blocksRaycasts = true;
        }
        public void DisableButtom()
        {
            _content.DOFade(0, _time);
            _content.blocksRaycasts = false;
            _button.interactable = true;
            _buttonText.DOFade(_enableFade, _time);
        }
    }
}
