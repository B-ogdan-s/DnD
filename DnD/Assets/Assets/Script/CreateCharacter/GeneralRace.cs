using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Character
{
    [CreateAssetMenu(fileName = "GeneralRace", menuName = "CharacterInfo/GeneralRace")]
    public class GeneralRace : ScriptableObject
    {
        [OdinSerialize] private List<ICompositeCharacter> _racesList;
        
    }
}