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

        public void InputID(string id)
        {
            _frandId = id;
        }

        public async void StartSearching()
        {
            _invitationFriendUI.OpenLoading();
            
            var name = Database.ReadName(_frandId);
            await Task.WhenAll(name);
            if (name.Result == null)
            {
                _invitationFriendUI.OpenError("error");
                return;
            }
            _invitationFriendUI.OpenFriend(name.Result);
            
        }

        public void OpenInvitationFriend()
        {
            _invitationFriendUI.OpenPanel();
        }
        public void CloseInvitationFriend()
        {
            _invitationFriendUI.ClosePanel();
        }
    }
}
