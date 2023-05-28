using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreateStory
{
    public class DefaultTiles : MonoBehaviour
    {
        [SerializeField] private Transform _tileParents;
        [SerializeField] private GameObject _defaultTilePrefab;

        private List<DefaultTile> _defaultList = new List<DefaultTile>();

        private float _size;
        private Vector3 _startPos;

        private UpdateMatrixScale _updateMatrixScale = new UpdateMatrixScale();

        public void StartSettings(float size, Vector3 startPos)
        {
            _size = size;
            _startPos = startPos;
        }

        public void UpdateTiles(ref StoryInfo storyInfo, Vector2Int vector, Vector2Int oldSize, Vector2Int newSize, int value)
        {
            if (vector.x < 0)
            {
                _startPos.x -= value * _size;
            }
            if (vector.y < 0)
            {
                _startPos.z -= value * _size;
            }

            int scaleVector = 0;

            if (vector.x > 0 || vector.y > 0)
                scaleVector = 1;
            else if (vector.x < 0 || vector.y < 0)
                scaleVector = -1;

            if (value > 0)
            {
                var updateMatrix = _updateMatrixScale.AddLayer(storyInfo._defaultPanels, oldSize, newSize, scaleVector);
                storyInfo._defaultPanels = updateMatrix.Item1;

                foreach (Vector2Int item in updateMatrix.Item2)
                {
                    DefaultTile tile = PoolTile(ref _defaultList, _defaultTilePrefab);
                    tile.transform.position = _startPos + new Vector3(item.x, 0, item.y) * _size;
                    tile.Enable(item);

                    storyInfo._defaultPanels[item.x, item.y] = tile;
                }
            }
            else
            {
                var updateMatrix = _updateMatrixScale.RemoveLayer(storyInfo._defaultPanels, oldSize, newSize, scaleVector);
                storyInfo._defaultPanels = updateMatrix.Item1;

                foreach (var delta in updateMatrix.Item2)
                {
                    if (delta != null)
                        delta.Disable();
                }
            }
        }

        public void Spawn(Vector2Int scale, ref StoryInfo storyInfo)
        {
            for(int i = 0; i < scale.x; i++)
            {
                for(int j = 0; j < scale.y; j++)
                {
                    DefaultTile tile = PoolTile(ref _defaultList, _defaultTilePrefab);
                    tile.transform.position = _startPos + new Vector3(i, 0, j) * _size;

                    tile.Enable(new Vector2Int(i, j));
                    storyInfo._defaultPanels[i, j] = tile;
                }
            }
        }

        private DefaultTile PoolTile(ref List<DefaultTile> tileList, GameObject tile)
        {
            foreach(DefaultTile t in tileList)
            {
                if(!t.Check)
                {
                    return t;
                }
            }

            GameObject tileObject = Instantiate(tile);
            tileObject.transform.SetParent(_tileParents);
            DefaultTile newTile = tileObject.GetComponent<DefaultTile>();

            tileList.Add(newTile);
            return newTile;
        }
    }
}
