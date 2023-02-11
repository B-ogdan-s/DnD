using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestCreateStory
{
    public class CreateStory : MonoBehaviour
    {
        [SerializeField] private Vector2Int _startNum;
        [SerializeField] private Transform _parentsTitle;
        [SerializeField] private Transform _sizePlatformParent;
        [SerializeField] private GameObject _scalePanelPrefab;
        [SerializeField] private GameObject _defaultTile;
        [SerializeField] private float _size;
        [SerializeField] private Vector2Int _num;

        private StoryInfo _layers;

        private Vector3 _startPos;
        private Vector3 _endPos;

        private Vector3 _startScalePos;
        private Vector3 _endScalePos;

        private List<DefaultPanel> _defaultList = new List<DefaultPanel>();

        private List<ScalePanel> _sizePlatforms = new List<ScalePanel>();

        private UpdateMatrixScale _updateMatrixScale = new UpdateMatrixScale();

        private void Start()
        {
            _layers = new StoryInfo();
            _layers._defaultPanels = new DefaultPanel[_startNum.x, _startNum.y];
            _num = _startNum;

            StartSpawnPlane(_startNum.x, _startNum.y);

            for(int i = 0; i < 4; i++)
            {
                GameObject obj = Instantiate(_scalePanelPrefab);
                obj.transform.SetParent(_sizePlatformParent);

                _sizePlatforms.Add(obj.GetComponent<ScalePanel>());
                _sizePlatforms[i]._updatePos += UpdateScalePose;
                _sizePlatforms[i]._updateScale += UpdateScaleTitle;
            }

            Vector3Int vector = new Vector3Int(-1, 0, 0);

            for (int i = 0; i < 4; i++)
            {
                int num = 0;
                if (vector.x != 0)
                    num = _startNum.y;
                else
                    num = _startNum.x;

                _sizePlatforms[i].StartSettings(_startScalePos, _endScalePos, vector, 90 * i, num, _size);

                vector = new Vector3Int(-vector.z, 0, vector.x);
            }
        }

        private void UpdateScaleTitle(Vector3Int vector, int value)
        {
            Vector2Int vector2Int = new Vector2Int(vector.x, vector.z);

            for (int i= 0; i < 3; i++)
            {
                if (vector[i] < 0)
                {
                    _startPos[2 - i] -= value * _size;
                }
                else if (vector[i] > 0)
                {
                    _endPos[2 - i] += value * _size;
                }
            }

            Vector2Int oldPos = _num;
            for (int i = 0; i < 2; i++)
            {
                if (vector2Int[i] != 0)
                {
                    _num[1 - i] += value;
                    break;
                }
            }

            int scaleVector = 0;

            if (vector2Int.x > 0 || vector2Int.y > 0)
                scaleVector = 1;
            else if (vector2Int.x < 0 || vector2Int.y < 0)
                scaleVector = -1;


            if(value > 0)
            {
                _layers._defaultPanels = _updateMatrixScale.AddLayer(_layers._defaultPanels, out List<Vector2Int> freeList, oldPos, _num, scaleVector);

                foreach (Vector2Int item in freeList)
                {
                    _layers._defaultPanels[item.x, item.y] = SpawnDefaultPanel(_defaultTile, _parentsTitle, new Vector3(item.x, 0, item.y) * _size + _startPos, new Vector2(item.x, item.y));
                }
            }
            else
            {
                _layers._defaultPanels = _updateMatrixScale.RemoveLayer(_layers._defaultPanels, out List<DefaultPanel> deltaList,
                    oldPos, _num, scaleVector);

                foreach (var delta in deltaList)
                {
                    if(delta != null)
                        delta.Disable();
                }
            }

            for (int i = 0; i < _num.x; i++)
            {
                for (int j = 0; j < _num.y; j++)
                {
                    _layers._defaultPanels[i, j].Enable(new Vector2(i, j));
                }
            }
        }

        private void UpdateScalePose(ScalePanel plane, Vector3Int vector, Vector3 pos)
        {
            if (vector.x < 0 || vector.z < 0)
                _startScalePos += pos;
            else if (vector.x > 0 || vector.z > 0)
                _endScalePos += pos;

            foreach(var panel in _sizePlatforms)
            {
                if(panel != plane)
                {
                    panel.UpdatePos(_startScalePos, _endScalePos);
                }
            }
        }

        private void StartSpawnPlane(int heigh, int length)
        {
            _startPos = new Vector3(-heigh / 2, 0, -length / 2) * _size;
            _startScalePos = _startPos;
            _startScalePos -= new Vector3(1, 0, 1) * _size * 1.5f;

            _endPos = _startPos + new Vector3(heigh-1, 0, length-1) * _size;
            _endScalePos = _endPos;
            _endScalePos += new Vector3(1, 0, 1) * _size * 1.5f;

            for (int i = 0; i < heigh; i++)
            {
                for(var j = 0; j < length; j++)
                {
                    _layers._defaultPanels[i, j] = SpawnDefaultPanel(_defaultTile, _parentsTitle, new Vector3(i, 0, j) * _size + _startPos, new Vector2(i, j));
                }
            }
        }

        private DefaultPanel SpawnDefaultPanel(GameObject prefab, Transform parent, Vector3 pos, Vector2 masPos)
        {
            foreach(var def in _defaultList)
            {
                if(!def.Check)
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
