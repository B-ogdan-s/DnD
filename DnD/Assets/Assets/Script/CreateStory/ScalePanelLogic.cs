using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreateStory
{
    public class ScalePanelLogic
    {
        private Transform _scalePanel;
        private Transform _line;

        private int _numSize;
        private float _size;
        private Vector3Int _vector;
        private Vector3 _position;

        private Vector3 _startPos;
        private Vector3 _endPos;

        public ScalePanelLogic (Transform scalePanel, Transform line)
        {
            _scalePanel = scalePanel;
            _line = line;
        }

        public void SetPosition()
        {
            _position = _scalePanel.position;
        }

        public void StartSettings(Vector3 startPos, Vector3 endPos, Vector3Int vector, int num, float size)
        {
            _size = size;
            _vector = vector;

            _startPos = startPos;
            _endPos = endPos;

            UpdatePos(startPos, endPos);

            _numSize = num / 2;

            for (int i = 0; i < 3; i++)
            {
                if (vector[i] < 0 && num % 2 == 0)
                {
                    _numSize--;
                    break;
                }
            }
        }

        public (Vector3, Vector3, Vector3) PanelMove(Vector3 touch, float slideValue)
        {
            float deltaX = touch.x * Mathf.Abs(_vector.z);
            float deltaZ = touch.z * Mathf.Abs(_vector.x);
            Vector3 delta = new Vector3(deltaX, 0 , deltaZ);

            _scalePanel.position += delta;

            for(int i = 0; i < 3; i++)
            {
                if(_vector[2 - i] != 0)
                {
                    float pos = _scalePanel.position[i];
                    float check1 = (_position[i] + _size * (slideValue) * _vector[2 - i]);
                    float check2 = (_position[i] - _size * (1f - slideValue) * _vector[2 - i]);

                    bool boolValue1 = true;
                    bool boolValue2 = _numSize > 0;

                    if (_vector[2 - i] < 0)
                    {
                        float f = check1;
                        check1 = check2;
                        check2 = f;

                        bool b = boolValue1;
                        boolValue1 = boolValue2;
                        boolValue2 = b;

                        _startPos += delta;
                    }
                    else
                    {
                        _endPos += delta;
                    }

                    if (pos > check1 && boolValue1)
                    {
                        _numSize += _vector[2 - i];
                        _position[i] += _size;
                    }
                    else if (pos < check2 && boolValue2)
                    {
                        _numSize -= _vector[2 - i];
                        _position[i] -= _size;

                    }

                    break;
                }
            }

            return (delta, _startPos, _endPos);
        }

        public (Vector3, Vector3, Vector3) ReturnPos()
        {
            float deltaX = _position.x - _scalePanel.position.x;
            float deltaZ = _position.z - _scalePanel.position.z;

            Vector3 delta = new Vector3(deltaX, 0, deltaZ);

            return (_position, _startPos, _endPos);
        }

        public void UpdatePos(Vector3 startPos, Vector3 endPos)
        {
            SetMovePos(startPos, endPos);
            SetScale(startPos, endPos);
        }
        private void SetMovePos(Vector3 startPos, Vector3 endPos)
        {
            Vector3 pos = Vector3.zero;

            for (int i = 0; i < 3; i++)
            {
                if (_vector[i] != 0)
                {
                    pos[i] = startPos[i] + (endPos[i] - startPos[i]) / 2f;
                    if (_vector[i] > 0)
                        pos[2 - i] = endPos[2 - i];
                    else
                        pos[2 - i] = startPos[2 - i];

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
                    Debug.Log(_size);
                    scale = ((endPos[i] - startPos[i]) - _size * 1.8f);
                    break;
                }
            }
            _line.localScale = new Vector3(scale, 2, 1);
        }
    }
}
