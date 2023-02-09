using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButtons : MonoBehaviour
{
   [SerializeField] private Button _nextButton;
   [SerializeField] private Button _previousButton;
   [SerializeField] private GameObject _racePanel;
   [SerializeField] private GameObject _classPanel;

   private void Start()
   {
      _previousButton.interactable = false;
      _nextButton.onClick.AddListener(SetNext);
      _previousButton.onClick.AddListener(SetPrevious);
   }

   private void SetNext()
   {
      _previousButton.interactable = true;
      _racePanel.SetActive(false);
      _classPanel.SetActive(true);
   }

   private void SetPrevious()
   {
      _racePanel.SetActive(true);
      _classPanel.SetActive(false);
      _previousButton.interactable = false;
   }
}
