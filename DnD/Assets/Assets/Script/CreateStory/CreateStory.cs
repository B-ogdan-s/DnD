using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

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

        private List<DefaultPanel> _defaultList = new List<DefaultPanel>();

        private void Start()
        {
            _scale.SpawnSizePanel(_initialSize, _sizeTile, UpdateSize);

            _storyInfo = new StoryInfo();
            _size = _initialSize;

            _storyInfo._startPosition = _startPos = new Vector3(-_initialSize.x / 2, 0, -_initialSize.y / 2) * _sizeTile;

            _endPos = _startPos + new Vector3(_initialSize.x - 1, 0, _initialSize.y - 1) * _sizeTile;

            _storyInfo._defaultPanels = new DefaultPanel[_size.x, _size.y];
            StartSpawnPlane(_size.x, _size.y);
        }

        private void UpdateSize(int value, Vector2Int vector)
        {
            for(int i = 0; i < 2; i++)
            {
                if (vector[i] != 0)
                {
                    _size[i] += value;
                }
            }
        }


        private void StartSpawnPlane(int heigh, int length)
        {
            for (int i = 0; i < heigh; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    _storyInfo._defaultPanels[i, j] = SpawnDefaultPanel(_defaultTile, _parentsTitle, new Vector3(i, 0, j) * _sizeTile + _startPos, new Vector2(i, j));
                }
            }
        }

        private DefaultPanel SpawnDefaultPanel(GameObject prefab, Transform parent, Vector3 pos, Vector2 masPos)
        {
            foreach (var def in _defaultList)
            {
                if (!def.Check)
                {
                    def.transform.position = pos;
                    def.Enable(masPos);
                    return def;
                }
            }

            GameObject obj = Instantiate(prefab);
            obj.transform.localPosition = pos;
            obj.transform.parent = parent;

            DefaultPanel defaultPanel = obj.GetComponent<DefaultPanel>();

            _defaultList.Add(defaultPanel);
            defaultPanel.Enable(masPos);

            return defaultPanel;
        }
    }
}
