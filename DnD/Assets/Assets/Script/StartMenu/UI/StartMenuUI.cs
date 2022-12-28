using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private Transform _topPanel;
    [SerializeField] private Transform _bottomPanel;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private float _time;
    [SerializeField] private float _shift;

    private Canvas _canvas;

    private float _startPosTopPanel;
    private float _startPosBottomPanel;
    private float _endPosTopPanel;
    private float _endPosBottomPanel;

    private Coroutine _coroutine;

    private void Awake()
    {
        Auth._OpenMenu += OpenMenu;

        _canvas = GetComponent<Canvas>();
        _startPosTopPanel = _topPanel.localPosition.y + _shift;
        _endPosTopPanel = _topPanel.localPosition.y;
        _startPosBottomPanel = _bottomPanel.localPosition.y - _shift;
        _endPosBottomPanel = _bottomPanel.localPosition.y;

        _topPanel.DOLocalMoveY(_startPosTopPanel, 0);
        _bottomPanel.DOLocalMoveY(_startPosBottomPanel, 0);
        _canvas.enabled = false;
    }

    public void OpenMenu()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _canvas.enabled = true;
        _topPanel.DOLocalMoveY(_endPosTopPanel, _time);
        _bottomPanel.DOLocalMoveY(_endPosBottomPanel, _time);
    }
    public void CloseMenu()
    {
        _coroutine = StartCoroutine(CR_CloseMenu());
    }

    private IEnumerator CR_CloseMenu()
    {
        _topPanel.DOLocalMoveY(_startPosTopPanel, _time);
        _bottomPanel.DOLocalMoveY(_startPosBottomPanel, _time);
        yield return new WaitForSeconds(_time);
        _canvas.enabled = false;
    }
    private void OnDestroy()
    {
        Auth._OpenMenu -= OpenMenu;
    }
}
