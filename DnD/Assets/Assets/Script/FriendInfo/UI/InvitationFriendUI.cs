using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;

public class InvitationFriendUI : MonoBehaviour
{
    [BoxGroup("Canvas")]
    [SerializeField] private Canvas _canvas;

    [FoldoutGroup("CanvasGroup")]
    [SerializeField] private CanvasGroup _search;
    [FoldoutGroup("CanvasGroup")]
    [SerializeField] private CanvasGroup _loading;
    [FoldoutGroup("CanvasGroup")]
    [SerializeField] private CanvasGroup _friend;
    [FoldoutGroup("CanvasGroup")]
    [SerializeField] private CanvasGroup _error;

    [SerializeField] private TMP_InputField _inputField;

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _errorText;

    private void Start()
    {
        _canvas.enabled = false;
    }

    public void OpenSearch()
    {
        _inputField.text = "";
        CanvasGroupController(_search);
    }
    public void OpenLoading()
    {
        CanvasGroupController(_loading);
    }
    public void OpenFriend(string name)
    {
        CanvasGroupController(_friend);
        _name.text = name;
    }
    public void OpenError(string errorMessage)
    {
        CanvasGroupController(_error);
        _errorText.text = errorMessage;
    }
    public void OpenPanel()
    {
        OpenSearch();
        _canvas.enabled = true;
    }
    public void ClosePanel()
    {
        _canvas.enabled = false;
    }

    private void CanvasGroupController(CanvasGroup group)
    {
        _search.alpha = 0;
        _search.blocksRaycasts = false;
        _loading.alpha = 0;
        _loading.blocksRaycasts = false;
        _friend.alpha = 0;
        _friend.blocksRaycasts = false;
        _error.alpha = 0;
        _error.blocksRaycasts = false;

        group.alpha = 1;
        group.blocksRaycasts = true;
    }
}
