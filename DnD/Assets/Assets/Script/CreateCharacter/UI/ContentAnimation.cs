using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ContentAnimation : MonoBehaviour
{
   [SerializeField] private GameObject _panel;
   [SerializeField] private CanvasGroup _canvasGroup;
   [SerializeField] private Button _button;
   private Coroutine _hideAdd;
  

   private void Start()
   {
      _button.onClick.AddListener(Showed);
   }

   private void Showed()
   {
      if (_hideAdd != null)
      {
          StopCoroutine(_hideAdd);
      }
      _button.onClick.RemoveListener(Showed);
      _button.onClick.AddListener(Hided);
      _panel.SetActive(true);
      _canvasGroup.DOFade(1f, 1f);
   }
   
   private void Hided()
   {
      _hideAdd = StartCoroutine(CR_HideAdditional());
      _button.onClick.RemoveListener(Hided);
      _button.onClick.AddListener(Showed);
   }
   
   private IEnumerator CR_HideAdditional()
   {
      _canvasGroup.DOFade(0f, 1f);
      yield return new WaitForSeconds(0.5f);
      _panel.SetActive(false);
   }
}
