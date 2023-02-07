using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    
    public class CharacterCharacteristics : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private GameObject _showObj;
        [SerializeField] private StatInfo _statInfo;

        public string Name => _name;
        public string Description => _description;
    }
}