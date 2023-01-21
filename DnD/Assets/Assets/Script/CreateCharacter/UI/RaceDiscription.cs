using System.Collections.Generic;
using Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class RaceDiscription : MonoBehaviour
{
    [SerializeField] private CharacterRace _rece;
    [SerializeField] private Button _button;
    //[SerializeField] private TextMeshPro _description;
    [SerializeField] private TextMeshProUGUI _nameofRace;
    [SerializeField] private CharacterRace _characterRace;
    
    private void Start()
    {
       _button.onClick.AddListener(SetDescribation);
    }
    
    private  void SetDescribation()
    {
        _nameofRace.text = "";
        _nameofRace.text += _characterRace.RaceName;
    }
}
