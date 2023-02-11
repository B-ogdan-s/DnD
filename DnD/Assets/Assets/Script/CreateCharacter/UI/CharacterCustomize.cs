using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomizeType
{
    Hat,
    Head,
    Body,
    FrontArm,
    BackArm,
    FrontLeg,
    BackLeg
}

public class CharacterCustomize 
{
     private List<CustomizeSettings> _settingsList;

     public  CharacterCustomize (List<CustomizeSettings> customizeSettingsList)
     {
         _settingsList = customizeSettingsList;
     }
    
    public void Customization(List<CustomizeInfo> customizeInfos)
    {
        foreach (var settings in _settingsList)
        {
            foreach (var info in customizeInfos)
            {
                if (settings.CustomizeType == info.CustomizeType)
                {
                    settings.SpriteRenderer.sprite = info.Sprite;
                }
            }
        }
    }
}

[Serializable]
public class CustomizeInfo
{
    [SerializeField] private CustomizeType _customizeType;
    [SerializeField] private Sprite _sprite;


    public CustomizeType CustomizeType => _customizeType;
    public Sprite Sprite => _sprite;

}

[Serializable]
public class CustomizeSettings
{
    [SerializeField] private CustomizeType _customizeType;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public CustomizeType CustomizeType => _customizeType;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
}

