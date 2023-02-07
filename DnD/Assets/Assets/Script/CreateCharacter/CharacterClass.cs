using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Character
{
   [CreateAssetMenu(fileName = "CharacterClass",menuName = "CharacterInfo/CharacterClass")]
   public class CharacterClass : CharacterCharacteristics,ICompositeCharacter
   {
      public void Show()
      {
         
      }
   }
   
   
}