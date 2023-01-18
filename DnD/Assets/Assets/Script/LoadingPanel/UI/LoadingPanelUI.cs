using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Transform _loadingIcon;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _iconTime;

    private Sequence _sequence;
    private Coroutine _iconCoroutine;

    public event Action _disableAnimation;

    private void Start()
    {
        _slider.value = 0;
        _canvasGroup.DOFade(0, 0);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void Enable()
    {
        _canvasGroup.DOFade(1, _fadeTime);
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _iconCoroutine = StartCoroutine(CR_IconAnimation());
    }
    public void Disable()
    {
        StartCoroutine(CR_Disable());
    }

    private IEnumerator CR_Disable()
    {
        _canvasGroup.DOFade(0, _fadeTime);
        yield return new WaitForSeconds(_fadeTime);

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        if (_iconCoroutine != null)
            StopCoroutine(_iconCoroutine);
        _sequence.Kill();

        _disableAnimation?.Invoke();
    }

    private IEnumerator CR_IconAnimation()
    {
        while(true)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(_loadingIcon.DOLocalRotate(new Vector3(0, 0, _loadingIcon.eulerAngles.z - 360), _iconTime, RotateMode.FastBeyond360).SetEase(Ease.Linear));
            yield return new WaitForSeconds(_iconTime);
        }
    }
}
