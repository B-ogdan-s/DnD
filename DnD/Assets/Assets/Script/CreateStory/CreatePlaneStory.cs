using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CreateStory
{
    public class CreatePlaneStory : MonoBehaviour
    {
        [SerializeField] private PlaneInfo _plane;
        [SerializeField] private GameObject _empty;
        [SerializeField] private Transform _parents;
        [SerializeField, MinValue(0)] private float _scale;
        [SerializeField, MinValue(0)] private int _edge;

        private GameObject[,] _planes;
        private TypePlane[,] _planeType;
        private int _x;
        private int _z;

        public event System.Action<int, int, TypePlane[,]> ReturnInfo;

        public void CreatePlane(int x, int z)
        {
            _x = x + _edge * 2;
            _z = z + _edge * 2;
            
            _planes = new GameObject[_x, _z];
            _planeType = new TypePlane[_x, _z];

            GameObject parents = Instantiate(_empty);
            parents.transform.SetParent(_parents);
            parents.transform.localPosition = Vector3.zero;

            float startPosX = transform.localPosition.x - ((_x * _scale)/2 - (_scale / 2));
            float startPosZ = transform.localPosition.z - ((_z * _scale)/2 - (_scale / 2));

            CreateType();
            ReturnInfo(x, z, _planeType);

            for (int i = 0; i < _x; i++)
            {
                for(int j = 0; j < _z; j++)
                {
                    foreach(Info info in _plane.Info)
                    {
                        if (_planeType[i, j] == info.Type)
                        {
                            GameObject obj = Instantiate(info.Prefab);
                            obj.transform.SetParent(parents.transform);
                            obj.transform.localPosition += new Vector3(startPosX + (i * _scale), 0, startPosZ + (j * _scale));
                            _planes[i, j] = obj;
                        }
                    }
                }
            }
        }

        private void CreateType()
        {
            for(int i = 0; i < _x; i++)
            {
                for(int j = 0; j < _z; j++)
                {
                    if((i < _edge || j < _edge) || (i >= _x-_edge || j >= _z-_edge))
                    {
                        _planeType[i, j] = TypePlane.Forest;
                    }
                    else
                    {
                        _planeType[i, j] = TypePlane.Gress;
                    }
                }
            }
        }

        private void CheckType()
        {
            int num = System.Enum.GetNames(typeof(TypePlane)).Length;
            Debug.Log(num);
        }
    }
}
