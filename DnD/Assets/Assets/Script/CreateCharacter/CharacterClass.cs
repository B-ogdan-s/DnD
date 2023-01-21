using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Character
{
   [CreateAssetMenu(fileName = "CharacterClass",menuName = "CharacterInfo/CharacterClass")]
   public class CharacterClass : ScriptableObject,ICompositeCharacter
   {
      [SerializeField] private string _className;
      [SerializeField] private string _describeClass;
      [SerializeField] private GameObject _showObj;
      [SerializeField] private StatInfo _statInfo;
      
      public void Show()
      {
         
      }
   }
   
   
}