using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ShowBrush : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private TextMeshProUGUI _text;

    private Canvas _canvas;
    private TileInfo _tile;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    public void SetBrush(TileInfo tile)
    {
        _canvas.enabled = true;
        _tile = tile;

        _image.texture = tile._Icon;
        _text.text = tile._Name;
    }

    public void Clear()
    {
        _canvas.enabled = false;
        _tile = null;
    }

}
