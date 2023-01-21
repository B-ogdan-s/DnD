using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    [CreateAssetMenu(fileName = "CharacterRace", menuName = "CharacterInfo/CharacterRace")]
    public class CharacterRace : ScriptableObject,ICompositeCharacter
    {
        [SerializeField] private string _raceName;
        [SerializeField] private string _describeRace;
        [SerializeField] private GameObject _showObj;
        [SerializeField] private StatInfo _statInfo;
       
        public string RaceName => _raceName;
        public string DescribeRace => _describeRace;
        
        public void Show()
        {
            
        }
    }
}