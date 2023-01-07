using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private Transform _leftPanel;
    [SerializeField] private Transform _rightPanel;
    [SerializeField] private Image _fadePanel;

    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _playerId;

    [SerializeField] private float _time;
    [SerializeField] private float _shift;
    [SerializeField] private float _fadeValue;

    private float _startPosLeftPanel;
    private float _startPosRightPanel;
    private float _endPosLeftPanel;
    private float _endPosRightPanel;

    private Canvas _canvas;

    private Coroutine _coroutine;

    private void Start()
    {
        Auth._UpdatePlayerInfo += ReadName;
        Auth._UpdatePlayerInfo += ReadId;

        _canvas = GetComponent<Canvas>();
        _canvas.enabled = true;
        _startPosLeftPanel = _leftPanel.localPosition.x - _shift;
        _startPosRightPanel = _rightPanel.localPosition.x + _shift;
        _endPosLeftPanel = _leftPanel.localPosition.x;
        _endPosRightPanel = _rightPanel.localPosition.x;

        _leftPanel.DOLocalMoveX(_startPosLeftPanel, 0);
        _rightPanel.DOLocalMoveX(_startPosRightPanel, 0);
        _fadePanel.DOFade(0, 0);

        _canvas.enabled = false;

        if (Auth._user != null)
        {
            ReadName();
            ReadId();
        }
    }

    private async void ReadName()
    {
        
        var name = PlayerData.ReadName(Auth._user.UserId);
        await Task.WhenAll(name);
        _playerName.text = name.Result;
        
    }

    private void ReadId()
    {
        _playerId.text = "Id: " + Auth._user.UserId;
    }

    public void OpenPlayerInfo()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
        _canvas.enabled = true;
        _fadePanel.DOFade(_fadeValue, _time);
        _leftPanel.DOLocalMoveX(_endPosLeftPanel, _time);
        _rightPanel.DOLocalMoveX(_endPosRightPanel, _time);
    }
    public void ClosePlayerInfo()
    {
        _coroutine = StartCoroutine(CR_ClosePlayerInfo());
    }
    public void CopyId()
    {
        GUIUtility.systemCopyBuffer = Auth._user.UserId;
        SSTools.ShowMessage("id copied", SSTools.Position.bottom, SSTools.Time.oneSecond);
    }

    private IEnumerator CR_ClosePlayerInfo()
    {
        _fadePanel.DOFade(0, _time);
        _leftPanel.DOLocalMoveX(_startPosLeftPanel, _time);
        _rightPanel.DOLocalMoveX(_startPosRightPanel, _time);
        yield return new WaitForSeconds(_time);
        _canvas.enabled = false;
    }

    private void OnDestroy()
    {
        Auth._UpdatePlayerInfo -= ReadName;
        Auth._UpdatePlayerInfo -= ReadId;
    }
}
