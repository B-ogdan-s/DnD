using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CreateStoryUI
{
    public class ControllerUI : MonoBehaviour
    {
        [SerializeField] private Canvas _transformCon;
        [SerializeField] private Button _transformButton;
        [SerializeField] private Button _createButton;

        private ButtonUI _buttonUI;
        private FadeUI _fade;
        private StartSettingsPanelUI _startSettingsPanelUI;

        private void Start()
        {
            _transformCon.enabled = false;
            _transformButton.interactable = true;
            _createButton.interactable = false;


            _buttonUI = FindObjectOfType<ButtonUI>();
            _fade = GetComponentInChildren<FadeUI>();
            _startSettingsPanelUI = GetComponentInChildren<StartSettingsPanelUI>();
            StartCoroutine(CR_Open());
        }
        private IEnumerator CR_Open()
        {
            _fade.Close();
            yield return new WaitForSeconds(1);
            _startSettingsPanelUI.Open();
        }

        public void TransformButton()
        {
            _transformButton.interactable = false;
            _createButton.interactable = true;
            _transformCon.enabled = true;
            _buttonUI.Open();
            
        }
        public void CreateButton()
        {
            _transformButton.interactable = true;
            _createButton.interactable = false;
            _transformCon.enabled = false;
            _buttonUI.Close();
        }
    }
}
