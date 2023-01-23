using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CreateStory
{
    public class StartSettingsPanelUI : MonoBehaviour
    {
        [SerializeField] private Image _backgraund;
        [SerializeField] private Transform _panel;
        [SerializeField] private TextMeshProUGUI _heightText;
        [SerializeField] private TextMeshProUGUI _lengthText;
        [SerializeField] private TMP_InputField _heightInputText;
        [SerializeField] private TMP_InputField _lengthInputText;

        [SerializeField] private Button _button;
        [Space(20)]
        [SerializeField] private int _minValue;
        [SerializeField] private int _maxValue;

        [Space(20)]
        [SerializeField] private float _backgraundValue;
        [Min(0)]
        [SerializeField] private float _panelShift;
        [Min(0)]
        [SerializeField] private float _time;

        private float _startPos;
        private float _endPos;

        private int _height;
        private int _length;

        private Canvas _canvas;

        public event Action<int, int> _Spawn;

        private void Start()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = true;

            _endPos = _panel.transform.localPosition.y;
            _startPos = _endPos + _panelShift;

            _height = _minValue;
            _length = _minValue;

            _heightText.text = "Heigh ( " + _minValue + " - " + _maxValue + " )";
            _lengthText.text = "Length ( " + _minValue + " - " + _maxValue + " )";

            _heightInputText.onEndEdit.AddListener((string valeu) =>
            {
                InputValue(valeu, ref _height, _heightInputText);
            });
            _lengthInputText.onEndEdit.AddListener((string valeu) =>
            {
                InputValue(valeu, ref _length, _lengthInputText);
            });

            _button.onClick.AddListener(() =>
            {
                _Spawn?.Invoke(_height, _length);
            });

            _panel.transform.localPosition += new Vector3(0, _panelShift, 0);
            _backgraund.DOFade(0, 0);
            _canvas.enabled = false;

            EnablePanel();
        }

        private void InputValue(string value, ref int saveValue, TMP_InputField TMP_Input)
        {
            if(value == null)
            {
                TMP_Input.text = _minValue.ToString();
                saveValue = _minValue;
                return;
            }
            int.TryParse(value, out int intValue);
            if (intValue < _minValue)
            {
                TMP_Input.text = _minValue.ToString();
                saveValue = _minValue;
            }
            else if(intValue > _maxValue)
            {
                TMP_Input.text = _maxValue.ToString();
                saveValue = _maxValue;
            }
            else
            {
                TMP_Input.text = value;
                saveValue = intValue;
            }
        }

        public void EnablePanel()
        {
            _canvas.enabled = true;
            _backgraund.DOFade(_backgraundValue, _time);
            _panel.DOLocalMoveY(_endPos, _time);
        }
        public void DisablePanel()
        {
            StartCoroutine(CR_DisablePanel());
        }

        private IEnumerator CR_DisablePanel()
        {
            _backgraund.DOFade(0, _time);
            _panel.DOLocalMoveY(_startPos, _time);
            yield return new WaitForSeconds(_time);
            _canvas.enabled = false;
        }
    }
}
