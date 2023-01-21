using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreateStory
{
    public class StartSettings : MonoBehaviour
    {
        [SerializeField] private StartSettingsPanelUI _startSettingsPanelUI;
        [SerializeField] private SpawnPlaneAnimation _spawnPlaneAnimation;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _size;

        private Transform _parents;

        private void Start()
        {
            _startSettingsPanelUI._Spawn += SpawnPlane;
            _parents = GetComponent<Transform>();
        }

        private void SpawnPlane(int heigh, int length)
        {
            Vector3 start = new Vector3(-heigh / 2, 0, -length / 2);

            GameObject[,] mas = new GameObject[heigh, length];

            for(int i = 0; i < heigh; i++)
            {
                for(var j = 0; j < length; j++)
                {
                    GameObject obj = Instantiate(_prefab);
                    obj.transform.localPosition = (new Vector3(i, 0, j) + start) * _size;
                    obj.transform.parent = _parents;
                    obj.SetActive(false);
                    mas[i, j] = obj;
                }
            }
            StartCoroutine(_spawnPlaneAnimation.CR_PlanesAnimation(mas, heigh, length));
        }

        private void OnDestroy()
        {
            _startSettingsPanelUI._Spawn -= SpawnPlane;
        }
    }
}
