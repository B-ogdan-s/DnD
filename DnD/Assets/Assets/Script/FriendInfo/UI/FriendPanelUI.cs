using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class FriendPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private RawImage _icon;
    [SerializeField] private string _friendkey;
    [SerializeField] private string _invitationKey;

    [SerializeField] private bool _isFriendID = false;
    [ShowIf("_isFriendID")]
    [SerializeField] private TextMeshProUGUI _id;

    private string _friendId;

    public void Spawn(string name, string friendId, Texture2D texture)
    {
        _name.text = name;
        _friendId = friendId;
        if(_isFriendID)
        {
            _id.text = "Id: " + friendId;
        }
        if(texture != null)
        {
            _icon.texture = texture;
            _icon.color = Color.white;
        }
    }

    public void DeclineInvitation()
    {
        FriendData._friendData.RemoveFriends(Auth._user.UserId, _friendId, _invitationKey);
    }

    public void DeclineFriend()
    {

        FriendData._friendData.RemoveFriends(Auth._user.UserId, _friendId, _friendkey);
        FriendData._friendData.RemoveFriends(_friendId, Auth._user.UserId, _friendkey);
        Destroy(gameObject);
    }

    public void AcceptFriend()
    {
        FriendData._friendData.AddFriends(Auth._user.UserId, _friendId, _friendkey);
        FriendData._friendData.AddFriends(_friendId, Auth._user.UserId, _friendkey);

        FriendData._friendData.RemoveFriends(Auth._user.UserId, _friendId, _invitationKey);
        FriendData._friendData.RemoveFriends(_friendId, Auth._user.UserId, _invitationKey);
        Destroy(gameObject);
    }
}
