using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    
    public class CharacterCharacteristics : ScriptableObject
    {
        [SerializeField] private StringInfo _stringInfo;
        [SerializeField] private List<CustomizeInfo> _customizeInfos;
        [SerializeField] private StatInfo _statInfo;


        public StringInfo StringInfo => _stringInfo;
        public List<CustomizeInfo> CustomizeInfos => _customizeInfos;
    }
}