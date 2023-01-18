using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private LoadingPanelUI _loadingPanelUI;

    public static event Action _disable;
    public static event Action _disableAnimation;

    private void Start()
    {
        _loadingPanelUI._disableAnimation += DisableAnimation;
        DontDestroyOnLoad(this);

    }
    [Button]
    private void Enable()
    {
        _loadingPanelUI.Enable();
    }
    [Button]
    private void Disable()
    {
        _loadingPanelUI.Disable();
    }
    private void DisableAnimation()
    {
        _disableAnimation?.Invoke();
        Debug.Log("disable");
    }
}
