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
        FriendInfo.InvitationFriend._Invite += AddFriend;
    }

    public  void AddFriend(string id)
    {
        _databaseReference.Child("Users").Child(id).Child("Invitation").RunTransaction(mutableData =>
        {
            List<object> invitation = mutableData.Value as List<object>;

            if (invitation == null)
            {
                Debug.Log("No1");
                invitation = new List<object>();
            }
            foreach (string invitationItem in invitation)
            {
                if(invitationItem == Auth._user.UserId)
                {
                    Debug.Log("Yes1");
                    mutableData.Value = invitation;
                    return TransactionResult.Success(mutableData);
                }
            }
            invitation.Add(Auth._user.UserId);
            mutableData.Value = invitation;
            Debug.Log("Yes2");
            return TransactionResult.Success(mutableData);
        });
    }

    private void OnDestroy()
    {
        FriendInfo.InvitationFriend._Invite -= AddFriend;
    }
}
