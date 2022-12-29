using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FriendInfo
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private List<FriendButton> _friendButtons;

        private void Awake()
        {
            foreach(FriendButton button in _friendButtons)
            {
                button._OnUpdeteList += Switch;
            }
        }

        private void Switch(string key)
        {
            foreach (FriendButton button in _friendButtons)
            {
                button.Enable();
            }
        }

    }
}
