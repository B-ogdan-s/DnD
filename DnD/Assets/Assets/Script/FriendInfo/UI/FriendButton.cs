using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendButton : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private bool _activate = false;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _disableColor;

    private Color _enableColor;

    private Button _button;

    public event Action<string> _OnUpdeteList;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _enableColor = _text.color;
    }

    private void Start()
    {

        _button.onClick.AddListener(() =>
        {
            Click();
        });

        if(!_activate)
        {
            return;
        }
        Click();
    }

    private void Click()
    {
        _OnUpdeteList?.Invoke(_key);
        _button.interactable = false;
        _text.color = _disableColor;
    }

    public void Enable()
    {
        _text.color = _enableColor;
        _button.interactable = true;
    }
}
