using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.StartMenu
{
    public class LocalStory : MonoBehaviour
    {
        [SerializeField] private int _numStory;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _parents;

        [SerializeField] private float _widthPrefab;
        [SerializeField] private float _shift;

        private void Awake()
        {
            for(int i = 0; i < _numStory; i++)
            {
                GameObject obj = Instantiate(_prefab);
                obj.transform.SetParent(_parents);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localPosition += new Vector3((_widthPrefab + _shift) * i, 0, 0);
            }
        }
    }
}
