using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FriendInfo
{
    public class InvitationFriend : MonoBehaviour
    {
        [SerializeField] private InvitationFriendUI _invitationFriendUI;

        private string _frandId;

        public static event Action<string> _Invite;

        public void InputID(string id)
        {
            _frandId = id;
        }

        public async void StartSearching()
        {
            _invitationFriendUI.OpenLoading();
            
            var name = PlayerData.ReadName(_frandId);
            var icon = Storage._instance.GetIcon(_frandId);
            var iconInfo = PlayerData.ReadIconInfo(_frandId);
            await Task.WhenAll(name, icon, iconInfo);

            if (name.Result == null)
            {
                _invitationFriendUI.OpenError("error");
                return;
            }
            Texture2D newTexture = null;


            if (icon.Result != null)
            {
                newTexture = new Texture2D(iconInfo.Result, iconInfo.Result);
                newTexture.LoadImage(icon.Result);
            }

            _invitationFriendUI.OpenFriend(name.Result, newTexture);
        }

        public void Invite()
        {
            _Invite?.Invoke(_frandId);
            CloseInvitationFriend();
        }


        public void OpenInvitationFriend()
        {
            _invitationFriendUI.OpenPanel();
        }
        public void CloseInvitationFriend()
        {
            _invitationFriendUI.ClosePanel();
            _frandId = "";
        }
    }
}
