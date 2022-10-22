using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EventSystems;
using UnityEngine;
using UnityEngine.EventSystems;


namespace UI.CreateStoryUI
{
    public class BrushManager : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabTypeBrush;
        [SerializeField] private Transform _parents;

        private List<Brush> _brushes = new List<Brush>();

        private void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(_prefabTypeBrush);
                obj.transform.SetParent(_parents);
                obj.GetComponent<Brush>().Create(i);
                _brushes.Add(obj.GetComponent<Brush>());
            }
        }
    }
}
