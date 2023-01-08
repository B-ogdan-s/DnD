using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CreateStory
{
    public class CreationToolsUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;

        [SerializeField] private RectTransform _icon;

        [SerializeField] private float _time;
        [SerializeField] private float _ratio;
        [SerializeField] private Button _button;
    
        private float _startPos;
        private float _endPos;

        private void Start()
        {
            _endPos = _panel.position.x;
            Debug.Log(Screen.width * _ratio);
            _startPos = _panel.position.x - (Screen.width * _ratio);
            _panel.DOMoveX(_startPos, 0);
            _icon.DOLocalRotate(new Vector3(0, 0, 90), 0);

            Open();
        }

        public void Open()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(Close);

            _panel.DOMoveX(_endPos, _time);
            _icon.DOLocalRotate(new Vector3(0, 0, -90), _time);
        }
        public void Close()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(Open);

            _panel.DOMoveX(_startPos, _time);
            _icon.DOLocalRotate(new Vector3(0, 0, 90), _time);
        }
    }
}
