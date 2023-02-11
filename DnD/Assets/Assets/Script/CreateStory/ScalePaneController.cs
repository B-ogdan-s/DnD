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

        private List<ScalePanel> _sizePlatforms = new List<ScalePanel>();

        public void SpawnSizePanel(Vector2Int startNum, float size)
        {
            Vector3 addValue = new Vector3(1, 0, 1) * size * _addValue;
            Vector3 startPos = new Vector3(-startNum.x / 2, 0, -startNum.y / 2) * size - addValue;
            Vector3 endPos = startPos + new Vector3(startNum.x - 1, 0, startNum.y - 1) * size + addValue;

            Vector3Int vector = new Vector3Int(-1, 0, 0);

            for (int i = 0; i < 4; i++)
            {
                GameObject obj = Instantiate(_sizePanelPrefab);
                obj.transform.SetParent(_sizePanelParent);

                _sizePlatforms.Add(obj.GetComponent<ScalePanel>());

                int num = 0;
                if (vector.x != 0)
                    num = startNum.y;
                else
                    num = startNum.x;

                _sizePlatforms[i].StartSettings(startPos, endPos, vector, 90 * i, num, size);
                _sizePlatforms[i]._updateScalePanel += UpdatePanel;

                vector = new Vector3Int(-vector.z, 0, vector.x);
            }
        }

        private void UpdatePanel(ScalePanel scalePanel, Vector3 startPos, Vector3 endPos)
        {
            foreach(var panel in _sizePlatforms)
            {
                if(panel != scalePanel)
                {
                    panel.UpdatePos(startPos, endPos);
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var panel in _sizePlatforms)
            {
                panel._updateScalePanel -= UpdatePanel;
            }
        }
    }
}
