using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;

public class Database : MonoBehaviour
{
    private static DatabaseReference _databaseReference;

    private void Start()
    {
        Auth._SetName += SetName;
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(this);
    }
    
    private void SetName()
    {
        StartCoroutine(CR_SetNam());
    }
    public async static Task<string> ReadName(string id)
    {
        var snapshot = _databaseReference.Child("Users").Child(id).Child("Name").GetValueAsync();
        await snapshot;

        if (snapshot.Result.Value == null)
        {
            return null;
        }
        return snapshot.Result.Value.ToString();
    }

    private IEnumerator CR_SetNam()
    {
        var loginTask = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Name").SetValueAsync(Auth._user.DisplayName);
        yield return new WaitUntil(predicate: () => loginTask.IsCanceled);
    }

    private void OnDestroy()
    {
        Auth._SetName -= SetName;
    }
    
}
