using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreateStory
{
    public class CreateStoryManager : MonoBehaviour
    {
        [SerializeField] private CreatePlaneStory _createPlaneStory;
        [SerializeField] private TransformManager _transformController;

        private int _x;
        private int _z;
        private TypePlane[,] _types;

        private void Start()
        {
            _createPlaneStory.ReturnInfo += SetInfo;
        }
        private void SetInfo(int x, int z, TypePlane[,] type)
        {
            _x = x;
            _z = z;
            _types = type;

            _transformController.CreatePole(_x, _z);
        }
    }
}
