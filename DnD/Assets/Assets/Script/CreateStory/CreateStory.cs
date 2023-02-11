using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

namespace CreateStory
{
    public class CreateStory : MonoBehaviour
    {
        [SerializeField] private Vector2Int _initialSize;
        [SerializeField] private Transform _parentsTitle;
        [SerializeField] private GameObject _defaultTile;
        [SerializeField] private ScalePaneController _scale;
        [SerializeField] private float _sizeTile;
        [SerializeField, ReadOnly] private Vector2Int _size;

        private StoryInfo _storyInfo;

        private Vector3 _startPos;
        private Vector3 _endPos;

        private void Start()
        {
            _scale.SpawnSizePanel(_initialSize, _sizeTile);

            _storyInfo = new StoryInfo();
            _size = _initialSize;

            _storyInfo._startPosition = _startPos = new Vector3(-_initialSize.x / 2, 0, -_initialSize.y / 2) * _sizeTile;

            _endPos = _startPos + new Vector3(_initialSize.x - 1, 0, _initialSize.y - 1) * _sizeTile;
        }
    }
}
