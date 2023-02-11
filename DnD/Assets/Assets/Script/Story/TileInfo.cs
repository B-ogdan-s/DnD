using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileInfo", menuName = "TileInfo")]
public class TileInfo : ScriptableObject
{
    [PreviewField(75, ObjectFieldAlignment.Left)]
    [SerializeField] private Texture2D _icon;
    [SerializeField] private string _name;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Material _material;

    public Texture2D _Icon => _icon;
    public string _Name => _name;
    public GameObject _Prefab => _prefab;
    public Material _Material => _material;
}
