using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    
    public class CharacterCharacteristics : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private List<CustomizeInfo> _customizeInfos;
        [SerializeField] private StatInfo _statInfo;

        public string Name => _name;
        public string Description => _description;
        public List<CustomizeInfo> CustomizeInfos => _customizeInfos;
    }
}