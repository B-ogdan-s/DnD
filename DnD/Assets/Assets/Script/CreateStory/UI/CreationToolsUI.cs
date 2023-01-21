using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CreateStory
{
    public class CreationToolsUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _leftPanel;
        [SerializeField] private RectTransform _icon;

        [SerializeField] private float _time;
        [SerializeField] private float _ratio;
        [SerializeField] private float _rightPanelShift;
        [SerializeField] private Button _button;
    
        private float _leftStartPos;
        private float _leftEndPos;

        private void Start()
        {
            _leftEndPos = _leftPanel.position.x;
            _leftStartPos = _leftPanel.position.x - (Screen.width * _ratio);

            _leftPanel.DOMoveX(_leftStartPos, 0);
            _icon.DOLocalRotate(new Vector3(0, 0, 90), 0);

            _button.onClick.AddListener(Open);
        }

        public void Open()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(Close);

            _leftPanel.DOMoveX(_leftEndPos, _time);
            _icon.DOLocalRotate(new Vector3(0, 0, -90), _time);
        }
        public void Close()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(Open);

            _leftPanel.DOMoveX(_leftStartPos, _time);
            _icon.DOLocalRotate(new Vector3(0, 0, 90), _time);
        }
    }
}
