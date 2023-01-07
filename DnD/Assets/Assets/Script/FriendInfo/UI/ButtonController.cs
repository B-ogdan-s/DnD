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

        public void Open()
        {
            foreach (FriendButton button in _friendButtons)
            {
                button.StartEnable();
            }
        }

        private void Switch()
        {
            foreach (FriendButton button in _friendButtons)
            {
                button.Enable();
            }
        }
    }
}
