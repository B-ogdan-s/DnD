using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Brush : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private TextMeshProUGUI _text;

    private TileInfo _tileInfo;

    public void StartSettings(TileInfo tile)
    {
        _tileInfo = tile;
        _image.texture = _tileInfo._Icon;
        _text.text = _tileInfo._Name;
    }
}
