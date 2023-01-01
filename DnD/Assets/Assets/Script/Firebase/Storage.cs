using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Storage;
using System.Threading.Tasks;
using Firebase.Extensions;

public class Storage : MonoBehaviour
{
    //private FirebaseStorage storage = FirebaseStorage.DefaultInstance;

    public static Storage _instance;

    private void Start()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }
    public async void SetIcon(byte[] image)
    {
        StorageReference storageRef = FirebaseStorage.DefaultInstance.RootReference;
        StorageReference riversRef = storageRef.Child("Player Icon/" + Auth._user.UserId);

        var metadata = new MetadataChange();
        metadata.ContentType = "image/jpg";

        await riversRef.PutBytesAsync(image, metadata).ContinueWith((Task<StorageMetadata> task) => {
            if (task.IsFaulted || task.IsCanceled)
            {
                // Uh-oh, an error occurred!
                Debug.LogError("Update Icon: Error");
            }
        });

    }
    public async Task<byte[]> GetIcon(string iconPath)
    {
        byte[] fileContents = null;

        StorageReference storageRef = FirebaseStorage.DefaultInstance.RootReference.Child("Player Icon/" + Auth._user.UserId);

        await storageRef.GetBytesAsync(1024*1024*10).ContinueWithOnMainThread(task => {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogException(task.Exception);
            }
            else
            {
                Debug.Log("Finished downloading!");
                fileContents = task.Result;
            }
        });

        return fileContents;
    }
}
