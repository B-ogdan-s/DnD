using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private RawImage _icon;

    [SerializeField] private Button _removeButton;

    public void Spawn(string name, Texture2D texture)
    {
        _name.text = name;
        if(texture != null)
        {
            _icon.texture = texture;
            _icon.color = Color.white;
        }
    }
}
