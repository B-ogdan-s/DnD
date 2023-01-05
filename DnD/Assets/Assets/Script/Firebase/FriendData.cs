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

    private void Start()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        FriendInfo.InvitationFriend._Invite += AddFriends;
    }

    public void AddFriends(string id, string key)
    {
        _databaseReference.Child("Users").Child(id).Child(key).RunTransaction(mutableData =>
        {
            List<object> invitation = mutableData.Value as List<object>;

            if (id == Auth._user.UserId)
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
                if(invitationItem == Auth._user.UserId)
                {
                    mutableData.Value = invitation;
                    return TransactionResult.Success(mutableData);
                }
            }
            invitation.Add(Auth._user.UserId);
            mutableData.Value = invitation;
            return TransactionResult.Success(mutableData);
        });
    }

    public void RemoveFriends(string playerId, string friendId, string key)
    {
        _databaseReference.Child("Users").Child(playerId).Child(key).RunTransaction(mutableData =>
        {
            List<object> invitation = mutableData.Value as List<object>;

            if(invitation.Contains(friendId))
                invitation.Remove(friendId);

            mutableData.Value = invitation;
            return TransactionResult.Success(mutableData);
        });
    }

    public static async Task<List<string>> ReadFriends(string key)
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
