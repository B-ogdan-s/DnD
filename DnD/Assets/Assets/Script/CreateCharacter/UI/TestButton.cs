using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonName;
    
    private Button _button;
    private CharacterCharacteristics _characteristics;
    public event Action<string,string> _setText;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(()=>_setText?.Invoke(_characteristics.Name,_characteristics.Description));
    }

    public void StartSettings(CharacterCharacteristics characteristics)
    {
        _characteristics = characteristics;
        _buttonName.text = _characteristics.Name;
    }
    
}
