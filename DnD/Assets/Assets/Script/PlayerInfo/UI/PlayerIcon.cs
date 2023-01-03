using System;
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
    [SerializeField] private Texture2D _default;

    public static event Action<int> _SetIconInfo;

    private void Start()
    {
        SetIcon(_default);

        if (Auth._user.UserId != null)
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
                Texture2D texture =  CropImage(NativeGallery.LoadImageAtPath(path, maxSize, false));

                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                _SetIconInfo?.Invoke(texture.height);

                SetIcon(texture);
                Storage._instance.SetIcon(texture.EncodeToJPG());

            }
        });
    }


    private Texture2D CropImage(Texture2D texture)
    {
        if (texture.width == texture.height) { return texture; }

        int width = texture.width;
        int height = texture.height;

        Texture2D resultTexture;

        if (width > height)
        {
            int diff = width - height;
            resultTexture = new Texture2D(width - diff, height);
            resultTexture.SetPixels(texture.GetPixels(diff / 2, 0, width - diff, height));
            resultTexture.Apply();
        }
        else 
        {
            int diff = height - width;
            resultTexture = new Texture2D(width, height - diff);
            resultTexture.SetPixels(texture.GetPixels(0, diff / 2, width, height - diff));
            resultTexture.Apply();
        }

        return resultTexture;
    }


    public async void ReadIcon(string iconPath)
    {
        var icon = Storage._instance.GetIcon(iconPath);
        var iconInfo = Database.ReadIconInfo(iconPath);

        await Task.WhenAll(icon, iconInfo);

        Debug.Log("ffff  " + icon.Result[0]);

        Texture2D newTexture = new Texture2D(iconInfo.Result, iconInfo.Result);

        Debug.Log(icon.Result);
        newTexture.LoadImage(icon.Result);

        SetIcon(newTexture);
    }
    public void SetIcon(Texture2D texture)
    {
        _icon.material.SetTexture("_Icon", texture);
    }
}
