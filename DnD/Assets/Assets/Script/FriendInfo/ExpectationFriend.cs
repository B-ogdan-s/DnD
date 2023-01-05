using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ExpectationFriend : MonoBehaviour
{
    [SerializeField] private Transform _parents;
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameObject _emptyPanel;

    private List<GameObject> _panelsList = new List<GameObject>();

    private CancellationTokenSource cts;

    public void OpenPanel(string key, GameObject prefab)
    {
        _emptyPanel.SetActive(false);
        _loadingPanel.SetActive(true);
        cts = new CancellationTokenSource();
        AS_OpenPanel(key, prefab, cts.Token);
    }

    public async void AS_OpenPanel(string key, GameObject prefab, CancellationToken cancellationToken)
    {
        List<FriendInfo> friendInfos = new List<FriendInfo>();

        var friendsId = FriendData.ReadFriends(key);
        await Task.WhenAll(friendsId);

        if (cancellationToken.IsCancellationRequested)
        {
            Debug.Log("Exit");
            return;
        }

        if (friendsId.Result == null)
        {
            _loadingPanel.SetActive(false);
            _emptyPanel.SetActive(true);
            return;
        }
        foreach (var panel in friendsId.Result)
        {
            var name = PlayerData.ReadName(panel);
            var icon = Storage._instance.GetIcon(panel);
            var iconInfo = PlayerData.ReadIconInfo(panel);

            await Task.WhenAll(name, icon, iconInfo);

            if (cancellationToken.IsCancellationRequested)
            {
                friendInfos.Clear();
                Debug.Log("Exit");
                return;
            }

            Texture2D newTexture = new Texture2D(iconInfo.Result, iconInfo.Result);
            newTexture.LoadImage(icon.Result);

            friendInfos.Add(new FriendInfo(name.Result, newTexture));
        }

        _loadingPanel.SetActive(false);
        foreach (FriendInfo info in friendInfos)
        {

            GameObject friend = Instantiate(prefab, _parents);
            _panelsList.Add(friend);
            friend.GetComponent<FriendPanelUI>().Spawn(info.Name, info.Icon);
        }
        friendInfos.Clear();
    }

    public void ClosePanel()
    {
        if(cts != null)
            cts.Cancel();

        foreach(GameObject gameObject in _panelsList)
        {
            Destroy(gameObject);
        }
        _panelsList = new List<GameObject>();
    }


    private class FriendInfo
    {
        private string _name;
        private Texture2D _icon;

        public string Name => _name;
        public Texture2D Icon => _icon;

        public FriendInfo(string name, Texture2D texture)
        {
            _name = name;
            _icon = texture;
        }
    }
}
