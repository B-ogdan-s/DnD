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
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _nameofRace;
        [SerializeField] private CharacterRace[] _characterRace;
        [SerializeField] private CharacterClass[] _characterClasses;
        [SerializeField] private GameObject _pref;
        [SerializeField] private Transform _parentRace;
        [SerializeField] private Transform _parentClass;
        [SerializeField] private List<CustomizeSettings> _settingsList;
        
        private CharacterCustomize _characterCustomize;
        
        private void Start()
        {
            _characterCustomize = new CharacterCustomize(_settingsList);
            StartSpawn(_characterRace,_parentRace); 
            StartSpawn(_characterClasses,_parentClass);
        }

        private void ChangeData(CharacterCharacteristics characteristics)
        {
            _description.text = characteristics.Description;
            _nameofRace.text = characteristics.Name;
           _characterCustomize.Customization(characteristics.CustomizeInfos);
        }
        
        private void StartSpawn(CharacterCharacteristics[] characteristicsArray,Transform parent)
        {
            for (int i = 0; i < characteristicsArray.Length; i++)
            {
                SetButtonName buttonName = Instantiate(_pref, parent).GetComponent<SetButtonName>();
                buttonName.StartSettings(characteristicsArray[i]);
                buttonName._setText += ChangeData;
            }
        }
    }
}