using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject _obj1;
    [SerializeField] private GameObject _obj2;
    [SerializeField] private Material _material;

    private void Start()
    {
        Material mat = new Material(_material);
        _obj1.GetComponent<MeshRenderer>().material = mat;
        mat = new Material(_material);
        _obj2.GetComponent<MeshRenderer>().material = mat;
    }

}
