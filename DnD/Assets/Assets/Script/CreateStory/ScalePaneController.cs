using System;
using System.Collections;
using System.Collections.Generic;
using TestCreateStory;
using UnityEngine;

namespace CreateStory
{
    public class ScalePaneController : MonoBehaviour
    {
        [SerializeField] private Transform _sizePanelParent;
        [SerializeField] private GameObject _sizePanelPrefab;
        [SerializeField] private float _addValue;

        private Vector3 _startPos;
        private Vector3 _endPos;

        private Vector3 _startScalePanelPos;
        private Vector3 _endScalePanelPos;

        private List<ScalePanel> _sizePlatforms = new List<ScalePanel>();

        public void SpawnSizePanel(Vector2Int startNum, float size, Action<int, Vector2Int> unityAction)
        {
            Vector3 addValue = new Vector3(1, 0, 1) * size * _addValue;

            _startPos = new Vector3(-startNum.x / 2, 0, -startNum.y / 2) * size;
            _endPos = _startPos + new Vector3(startNum.x - 1, 0, startNum.y - 1) * size;

            _startScalePanelPos = _startPos - addValue;
            _endScalePanelPos = _endPos + addValue;

            Vector3Int vector = new Vector3Int(0, 0, 1);

            for (int i = 0; i < 4; i++)
            {
                GameObject obj = Instantiate(_sizePanelPrefab);
                obj.transform.SetParent(_sizePanelParent);

                _sizePlatforms.Add(obj.GetComponent<ScalePanel>());

                int num = 0;
                if (vector.x != 0)
                    num = startNum.x;
                else
                    num = startNum.y;

                _sizePlatforms[i].StartSettings(_startScalePanelPos, _endScalePanelPos, vector, 180 - 90 * i, num, size);
                _sizePlatforms[i]._updateTransformPanel += UpdatePanel;

                _sizePlatforms[i]._returnSize += unityAction;

                vector = new Vector3Int(-vector.z, 0, vector.x);
            }
        }

        private void UpdatePanel(ScalePanel scalePanel, Vector3 delta, int vector)
        {
            if(vector < 0)
            {
                _startScalePanelPos += delta;
            }
            else if(vector > 0)
            {
                _endScalePanelPos += delta;
            }

            foreach(var panel in _sizePlatforms)
            {
                if(panel != scalePanel)
                {
                    panel.UpdateTransform(_startScalePanelPos, _endScalePanelPos);
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var panel in _sizePlatforms)
            {
                panel._updateTransformPanel -= UpdatePanel;
            }
        }
    }
}
