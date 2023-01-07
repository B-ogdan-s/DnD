using Firebase.Database;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class FriendData : MonoBehaviour
{
    private static DatabaseReference _databaseReference;

    public static FriendData _friendData;

    private void Start()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        FriendInfo.InvitationFriend._Invite += AddFriends;
        _friendData = this;
    }

    public void AddFriends(string playerId, string friendId, string key)
    {
        _databaseReference.Child("Users").Child(friendId).Child(key).RunTransaction(mutableData =>
        {
            List<object> invitation = mutableData.Value as List<object>;

            if (friendId == playerId)
            {
                mutableData.Value = invitation;
                return TransactionResult.Success(mutableData);
            }
            if (invitation == null)
            {
                invitation = new List<object>();
            }
            foreach (string invitationItem in invitation)
            {
                if(invitationItem == playerId)
                {
                    mutableData.Value = invitation;
                    return TransactionResult.Success(mutableData);
                }
            }
            invitation.Add(playerId);
            mutableData.Value = invitation;
            return TransactionResult.Success(mutableData);
        });
    }

    public void RemoveFriends(string playerId, string friendId, string key)
    {
        _databaseReference.Child("Users").Child(playerId).Child(key).RunTransaction(mutableData =>
        {
            List<object> invitation = mutableData.Value as List<object>;

            List<string> invitationS = new List<string>();

            Debug.Log(mutableData.Value);

            if (invitation == null)
            {
                invitation = new List<object>();
            }

            foreach (object invitationItem in invitation)
            {
                if (invitationItem.ToString() != friendId)
                {
                    Debug.Log(invitationItem.ToString());
                    invitationS.Add(invitationItem.ToString());
                }
                else
                {
                    _databaseReference.Child("Users").Child(playerId).Child(key).Child((invitation.Count - 1).ToString()).RemoveValueAsync();
                }
            }

            Debug.Log(invitationS.Count);

            mutableData.Value = invitationS;
            return TransactionResult.Success(mutableData);
        });
    }

    public async Task<List<string>> ReadFriends(string key)
    {
        var snapshot = _databaseReference.Child("Users").Child(Auth._user.UserId).Child(key).GetValueAsync();
        await snapshot;

        if (snapshot.Result.Value == null)
        {
            return null;
        }

        List<string> result = new List<string>();

        foreach (var item in snapshot.Result.Value as List<object>)
        {
            result.Add(item.ToString());
        }
        return result;
    }


    private void OnDestroy()
    {
        FriendInfo.InvitationFriend._Invite -= AddFriends;
    }
}
