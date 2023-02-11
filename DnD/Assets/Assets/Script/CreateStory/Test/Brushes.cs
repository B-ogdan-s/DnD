using TestCreateStory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Brushes : MonoBehaviour
{
    [SerializeField] private GameObject _prefabButton;
    [SerializeField] private List<BrusheInfo> _brushes = new List<BrusheInfo>();

    private List<ButtonDropdown> _buttonDropdowns = new List<ButtonDropdown>();

    [SerializeField] private TileInfo _tileInfo;

    public static Brushes _instance;

    private void Start()
    {
        if(_instance == null)
            _instance = this;


        foreach (var brush in _brushes)
        {
            GameObject obj = Instantiate(_prefabButton);

            obj.transform.SetParent(transform, false);
            _buttonDropdowns.Add(obj.GetComponent<ButtonDropdown>());
            obj.GetComponent<ButtonDropdown>().StartSetings(brush._Name, brush._Tiles);

        }
    }

    public void SetTitle(TileInfo info)
    {
        _tileInfo = info;
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
