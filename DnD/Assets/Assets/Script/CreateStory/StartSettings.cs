using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreateStory
{
    public class StartSettings : MonoBehaviour
    {
        [SerializeField] private StartSettingsPanelUI _startSettingsPanelUI;
        [SerializeField] private SpawnTileAnimation _spawnPlaneAnimation;
        [SerializeField] private TileInfo _tile;
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
                    GameObject obj = Instantiate(_tile._Prefab);
                    MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
                    meshRenderer.material = new Material(_tile._Material);
                    meshRenderer.material.DOFade(0, 0);
                    obj.transform.localPosition = (new Vector3(i, 0, j) + start) * _size;
                    obj.transform.parent = _parents;
                    mas[i, j] = obj;
                }
            }
            //_spawnPlaneAnimation.TileSpawn(mas, heigh, length);
            StartCoroutine(_spawnPlaneAnimation.CR_PlanesAnimation(mas, heigh, length));
        }

        private void OnDestroy()
        {
            _startSettingsPanelUI._Spawn -= SpawnPlane;
        }
    }
}
