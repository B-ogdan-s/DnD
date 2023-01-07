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

    [SerializeField] private FriendsListPopup _friend;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _disableColor;

    [SerializeField] private GameObject _prefab;

    private Color _enableColor;

    private Button _button;

    public event Action _OnUpdeteList;

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
    }

    public void StartEnable()
    {
        if (!_activate)
        {
            return;
        }
        else
        {
            Click();
        }
    }

    private void Click()
    {
        _friend.ClosePanel();

        _OnUpdeteList?.Invoke();
        _button.interactable = false;
        _text.color = _disableColor;

        _friend.OpenPanel(_key, _prefab);
    }

    public void Enable()
    {
        _text.color = _enableColor;
        _button.interactable = true;
    }
}
