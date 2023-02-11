using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButtons : MonoBehaviour
{
   [SerializeField] private Button _nextButton;
   [SerializeField] private Button _previousButton;

   public event Action<int> switchPanel;
   private int _minPos = 0;
   private int _maxPos = 0;
   private int _pos = 0;

   private void Start()
   {
      if (_pos == _minPos)
      {
         _previousButton.interactable = false;
      }
      if (_pos == _maxPos)
      {
         _nextButton.interactable = false;
      }
      _previousButton.onClick.AddListener(() =>
      {
         if (_pos>_minPos)
         {
            _pos--;
            if (_pos == _minPos)
            {
               _previousButton.interactable = false;
            }
            _nextButton.interactable = true;
            switchPanel?.Invoke(_pos);
         }
      });
      _nextButton.onClick.AddListener(() =>
      {
         if (_pos<_maxPos)
         {
            _pos++;
            if (_pos == _maxPos)
            {
               _nextButton.interactable = false;
            }
            _previousButton.interactable = true;
            switchPanel?.Invoke(_pos);
         }
      });
   }

   public void StartSettings(int maxPos)
   {
      _maxPos = maxPos;
   }

}
