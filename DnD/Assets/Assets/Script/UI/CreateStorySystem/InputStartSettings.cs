using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.CreateStoryUI
{
    public class InputStartSettings : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _textNumX;
        [SerializeField] private TMP_InputField _textNumZ;
        [SerializeField] private Button _button;

        [SerializeField] private int _minValue;
        [SerializeField] private int _maxValue;

        private StartSettingsPanelUI _panelUI;
        private CreateStory.CreatePlaneStory _create;

        private int _numX;
        private int _numZ;

        private void Start()
        {
            _create = FindObjectOfType<CreateStory.CreatePlaneStory>();
            _panelUI = GetComponent<StartSettingsPanelUI>();

            _numX = _minValue;
            _textNumX.text = _minValue.ToString();
            _numZ = _minValue;
            _textNumZ.text = _minValue.ToString();

            _textNumX.onEndEdit.AddListener(OnValueChangedX);
            _textNumZ.onEndEdit.AddListener(OnValueChangedZ);
            _button.onClick.AddListener(OnClickButton);

            _button.enabled = true;
        }

        public void OnValueChangedX(string input)
        {
            _numX = int.Parse(input);
            if (_numX < _minValue)
            {
                _textNumX.text = _minValue.ToString();
                _numX = _minValue;
            }
            if( _numX > _maxValue)
            {
                _textNumX.text = _maxValue.ToString();
                _numX = _maxValue;
            }
        }
        public void OnValueChangedZ(string input)
        {
            _numZ = int.Parse(input);
            if (_numZ < _minValue)
            {
                _textNumZ.text = _minValue.ToString();
                _numZ = _minValue;
            }
            if (_numZ > _maxValue)
            {
                _textNumZ.text = _maxValue.ToString();
                _numZ = _maxValue;
            }
        }
        public void OnClickButton()
        {
            _create.CreatePlane(_numX, _numZ);
            _panelUI.Close();
        }
    }
}
