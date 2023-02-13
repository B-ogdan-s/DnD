using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreateStory
{
    public class ScalePanelLogic
    {
        private Transform _scalePanel;
        private Transform _line;

        private float _size;
        private Vector3Int _vector;


        public ScalePanelLogic(Transform scalePanel, Transform line)
        {
            _scalePanel = scalePanel;
            _line = line;
        }

        public void StartSettings(Vector3 startPos, Vector3 endPos, Vector3Int vector, float size)
        {
            _size = size;
            _vector = vector;

            UpdateTransform(startPos, endPos);
        }

        public (int, Vector3, Vector3Int) UpdatePosition(Vector3 position, float slideValue)
        {
            Vector3 delta = Vector3.Scale(_scalePanel.position - position, _vector);
            Vector3 deltaMove = _size * (Vector3)_vector;

            for (int i = 0; i < 3; i++)
            {
                if (delta[i] > slideValue * _size)
                {
                    return (1, deltaMove, _vector);
                }
                else if(delta[i] < (slideValue - 1) * _size)
                {
                    return (-1, -deltaMove, _vector);
                }
                
            }
            return (0, Vector3.zero, _vector);
        }

        public (Vector3, int) TouchUpdate(Vector3 position)
        {
            Vector3 vector = new Vector3(Mathf.Abs(_vector.x), Mathf.Abs(_vector.y), Mathf.Abs(_vector.z));
            Vector3 delta = Vector3.Scale(position, vector);
            _scalePanel.position += delta;

            int value = 0;
            for(int i = 0; i < 3; i++)
            {
                if (_vector[i] != 0)
                {
                    value = _vector[i];
                }
            }

            return (delta, value);
        }

        public void UpdateTransform(Vector3 startPos, Vector3 endPos)
        {
            SetPos(startPos, endPos);
            SetScale(startPos, endPos);
        }
        private void SetPos(Vector3 startPos, Vector3 endPos)
        {
            Vector3 pos = Vector3.zero;

            for (int i = 0; i < 3; i++)
            {
                if (_vector[i] != 0)
                {
                    pos[2-i] = startPos[2-i] + (endPos[2-i] - startPos[2-i]) / 2f;
                    if (_vector[i] > 0)
                        pos[i] = endPos[i];
                    else
                        pos[i] = startPos[i];

                    break;
                }
            }

            _scalePanel.position = pos;
        }
        private void SetScale(Vector3 startPos, Vector3 endPos)
        {
            float scale = 0;
            for (int i = 0; i < 3; i++)
            {
                if (_vector[i] != 0)
                {
                    scale = ((endPos[2-i] - startPos[2-i]) - _size * 1.8f);
                    break;
                }
            }
            _line.localScale = new Vector3(scale, 2, 1);
        }
    }
}
