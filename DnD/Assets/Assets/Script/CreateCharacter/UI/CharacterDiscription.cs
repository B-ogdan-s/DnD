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
        //[SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _nameofRace;
        [SerializeField] private CharacterRace[] _characterRace;
        [SerializeField] private CharacterClass[] _characterClasses;
        [SerializeField] private GameObject _pref;
        [SerializeField] private Transform _parentRace;
        [SerializeField] private Transform _parentClass;
        private void Start()
        {
           StartSpawn(_characterRace,_parentRace);
           StartSpawn(_characterClasses,_parentClass);
        }

        private void ChangeData(string name,string description)
        {
            _description.text = description;
            _nameofRace.text = name;
        }

        private void StartSpawn(CharacterCharacteristics[] characteristicsArray,Transform parent)
        {
            for (int i = 0; i < characteristicsArray.Length; i++)
            {
                TestButton button = Instantiate(_pref, parent).GetComponent<TestButton>();
                button.StartSettings(characteristicsArray[i]);
                button._setText += ChangeData;
            }
        }
        
    }
}