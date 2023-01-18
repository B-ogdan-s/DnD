using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
    [SerializeField] private int _panelWidth;

    private void Start()
    {
        Debug.Log(_canvas.sizeDelta.x);
        int shift = ((int)_canvas.sizeDelta.x - _panelWidth) / 2;

        _horizontalLayoutGroup.padding.left = shift;
        _horizontalLayoutGroup.padding.right = shift;
    }
}
