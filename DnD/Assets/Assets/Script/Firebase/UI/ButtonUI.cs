using System;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine.UI;

namespace FirebaseUI
{
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _buttonText;
        [CustomValueDrawer("CustomSlider")]
        [SerializeField] private float _enableFade = 0;

        private float _time = 0;
        private Button _button;

        public event Action<float> _OnClick;

        public void StartSettings(float time, bool startEnable)
        {
            _button = GetComponent<Button>();
            _time = time;
            if(startEnable)
            {
                _buttonText.DOFade(1, 0);
                _button.interactable = false;
            }
            else
            {
                _buttonText.DOFade(_enableFade, 0);
            }
        }

        public void ClickButtom()
        {
            _OnClick?.Invoke(transform.localPosition.x);
            _buttonText.DOFade(1, _time);
            _button.interactable = false;
        }
        public void DisableButtom()
        {
            _button.interactable = true;
            _buttonText.DOFade(_enableFade, _time);
        }

        private float CustomSlider(float value, GUIContent label)
        {
            return EditorGUILayout.Slider(label, value, 0f, 1f);
        }
    }
}
