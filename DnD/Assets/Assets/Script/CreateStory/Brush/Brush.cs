using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Brush : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private TextMeshProUGUI _text;

    private Button _button;
    private TileInfo _tileInfo;

    public Action<TileInfo> UpdateTile;


    public void StartSettings(TileInfo tile)
    {
        _button = GetComponent<Button>();

        _tileInfo = tile;
        _image.texture = _tileInfo._Icon;
        _text.text = _tileInfo._Name;

        _button.onClick.AddListener(() =>
        {
            UpdateTile?.Invoke(_tileInfo);
            //Brushes._instance.SetTitle(_tileInfo);
        });
    }
}
