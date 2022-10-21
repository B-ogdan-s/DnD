using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CreateStory
{
    public enum TypePlane
    {
        Gress,
        Path,
        Structure,
        Forest
    }

    [CreateAssetMenu(fileName = "PlaneInfo", menuName = "CreateStory/PlaneInfo")]
    public class PlaneInfo : ScriptableObject
    {
        [SerializeField] private Info[] _info;

        public Info[] Info => _info;
    }

    [System.Serializable]
    public struct Info
    {
        [SerializeField] private TypePlane _type;
        [SerializeField] private GameObject _prefab;

        public TypePlane Type => _type;
        public GameObject Prefab => _prefab;
    }
}
