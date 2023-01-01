using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIcon : MonoBehaviour
{
    [SerializeField] private int _maxSizeImage;
    [SerializeField] private Image _icon;

    private void Start()
    {
        if(Auth._user.UserId != null)
        {
            ReadIcon(Auth._user.UserId);
        }
    }

    public void OnClick()
    {
        PickImage(_maxSizeImage);
    }

    private void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }
                SetIcon(texture);
                Storage._instance.SetIcon(texture.GetRawTextureData());

            }
        });
    }

    public async void ReadIcon(string iconPath)
    {
        var icon = Storage._instance.GetIcon(iconPath);
        await Task.WhenAll(icon);

        Debug.Log("ffff  " + icon.Result[0]);

        Texture2D newTexture = (Texture2D)_icon.material.GetTexture("_Icon");
        newTexture.LoadImage(icon.Result);

        SetIcon(newTexture);
    }
    public void SetIcon(Texture2D texture)
    {
        _icon.material.SetTexture("_Icon", texture);
    }
}
