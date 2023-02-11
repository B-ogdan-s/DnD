using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Character
{


    public class CharacterDiscription : MonoBehaviour
    {
        [SerializeField] private List<PanelInfo> _panelInfos;
        [SerializeField] private GameObject _pref;
        [SerializeField] private Transform _parent;
        [SerializeField] private List<CustomizeSettings> _settingsList;
        [SerializeField] private SwitchButtons _switchButtons;
        
        private CharacterDiscriptionUI _discriptionUI;
        private CharacterCustomize _characterCustomize;
        private List<Button> _buttons = new List<Button>();
        private void Awake()
        {
            _switchButtons.StartSettings(_panelInfos.Count - 1);
            _switchButtons.switchPanel += SwitchPanel;
            _discriptionUI = GetComponent<CharacterDiscriptionUI>();
            _discriptionUI.SetType(_panelInfos[0].StringInfo.Name);
            _discriptionUI.SetDefaultData(_panelInfos[0].StringInfo);
            _characterCustomize = new CharacterCustomize(_settingsList);
            Spawn(_panelInfos[0].CharacteristicsList,_parent);
        }

        private void ChangeData(CharacterCharacteristics characteristics)
        {
            _discriptionUI.SetData(characteristics.StringInfo);
            _characterCustomize.Customization(characteristics.CustomizeInfos);
        }
        
        private void Spawn(List<CharacterCharacteristics> characteristicsArray,Transform parent)
        {
            foreach (var i in _buttons)
            {
                i.gameObject.SetActive(false);
            }
            for (int i = 0; i < characteristicsArray.Count; i++)
            {
                SetButtonName buttonName = PullButton().GetComponent<SetButtonName>();
                buttonName.StartSettings(characteristicsArray[i]);
                buttonName._setText += ChangeData;
            }
        }

        private void SwitchPanel(int value)
        {
            _discriptionUI.SetType(_panelInfos[value].StringInfo.Name);
            _discriptionUI.SetDefaultData(_panelInfos[value].StringInfo);
            Spawn(_panelInfos[value].CharacteristicsList,_parent);
        }
        private Button PullButton()
        {
            foreach (var pulbutton in _buttons)
            {
                if (!pulbutton.IsActive())
                {
                    pulbutton.gameObject.SetActive(true);
                    return pulbutton;
                }
            }
            Button button = Instantiate(_pref,_parent).GetComponent<Button>();
            _buttons.Add(button);
            return button;
        }
        private void OnDestroy()
        {
            _switchButtons.switchPanel -= SwitchPanel;
        }
    }
}