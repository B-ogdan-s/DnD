using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "PanelInfo",menuName = "CharacterInfo/PanelInfo")]
    public class PanelInfo : ScriptableObject
    {
        [SerializeField] private StringInfo _stringInfo;
        [SerializeField] private List<CharacterCharacteristics> _characteristicsList;

        public List<CharacterCharacteristics> CharacteristicsList => _characteristicsList;
        public StringInfo StringInfo => _stringInfo;
    }
}