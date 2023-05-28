using TestCreateStory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Brushes : MonoBehaviour
{
    [SerializeField] private ButtonDropdown _prefabButton;
    [SerializeField] private List<BrusheInfo> _brushes = new List<BrusheInfo>();
    [SerializeField] private ShowBrush _showBrush;

    private List<ButtonDropdown> _buttonDropdowns = new List<ButtonDropdown>();

    private void Awake()
    {
        foreach (var brush in _brushes)
        {
            ButtonDropdown obj = Instantiate(_prefabButton);

            obj.transform.SetParent(transform, false);
            _buttonDropdowns.Add(obj);
            obj.StartSetings(brush._Name, brush._Tiles, SetTitle);
        }
    }

    public void SetTitle(TileInfo info)
    {
        _showBrush.SetBrush(info);
    }
}

[System.Serializable]
public class BrusheInfo
{
    [SerializeField] private string _nameBrushesList;
    [SerializeField] private List<TileInfo> _tiles = new List<TileInfo>();

    public string _Name => _nameBrushesList;
    public List<TileInfo> _Tiles => _tiles;
}
