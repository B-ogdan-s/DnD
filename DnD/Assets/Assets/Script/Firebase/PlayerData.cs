using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;

public class PlayerData : MonoBehaviour
{
    private static DatabaseReference _databaseReference;

    private void Start()
    {
        Auth._SetName += SetName;
        PlayerIcon._SetIconInfo += SetIconInfo;
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(this);
    }
    
    private void SetName()
    {
        StartCoroutine(CR_SetNam());
    }
    private void SetIconInfo(int size)
    {
        StartCoroutine(CR_SetIconInfo(size));
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
    public async static Task<int> ReadIconInfo(string id)
    {
        var snapshot = _databaseReference.Child("Users").Child(id).Child("IconInfo").GetValueAsync();
        await snapshot;

        if (snapshot.Result.Value == null)
        {
            return 0;
        }

        return int.Parse(snapshot.Result.Value.ToString());

    }

    private IEnumerator CR_SetNam()
    {
        var loginTask = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("Name").SetValueAsync(Auth._user.DisplayName);
        yield return new WaitUntil(predicate: () => loginTask.IsCanceled);
    }
    private IEnumerator CR_SetIconInfo(int size)
    {
        var loginTask = _databaseReference.Child("Users").Child(Auth._user.UserId).Child("IconInfo").SetValueAsync(size);
        yield return new WaitUntil(predicate: () => loginTask.IsCanceled);
    }

    private void OnDestroy()
    {
        Auth._SetName -= SetName;
        PlayerIcon._SetIconInfo -= SetIconInfo;
    }
    
}
