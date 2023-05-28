using UnityEngine;

namespace CreateStory
{
    [RequireComponent(typeof(ScalePaneController))]
    [RequireComponent(typeof(DefaultTiles))]
    public class CreateStory : MonoBehaviour
    {
        [SerializeField] private Vector2Int _initialSize;
        [SerializeField] private float _sizeTile;

        private ScalePaneController _scale;
        private DefaultTiles _defaultTiles;
        private StoryInfo _storyInfo;

        private Vector2Int _size;
        private Vector3 _startPos;

        private void Awake()
        {
            _scale = GetComponent<ScalePaneController>();
            _defaultTiles = GetComponent<DefaultTiles>();

            _storyInfo = new StoryInfo();
            _size = _initialSize;

            _storyInfo._startPosition = _startPos = new Vector3(-_initialSize.x / 2, 0, -_initialSize.y / 2) * _sizeTile;
            _storyInfo._defaultPanels = new DefaultTile[_size.x, _size.y];
        }

        private void Start()
        {
            _scale.SpawnSizePanel(_initialSize, _sizeTile, UpdateSize);

            _defaultTiles.StartSettings(_sizeTile, _startPos);
            _defaultTiles.Spawn(_size, ref _storyInfo);
        }

        private void UpdateSize(int value, Vector2Int vector)
        {
            Vector2Int oldSize = _size;

            for(int i = 0; i < 2; i++)
            {
                if (vector[i] != 0)
                {
                    _size[i] += value;
                }
            }
            _defaultTiles.UpdateTiles(ref _storyInfo, vector, oldSize, _size, value);

            for (int i = 0; i < _size.x; i++)
            {
                for (int j = 0; j < _size.y; j++)
                {
                    _storyInfo._defaultPanels[i, j].Enable(new Vector2Int(i, j));
                }
            }
        }
    }
}
