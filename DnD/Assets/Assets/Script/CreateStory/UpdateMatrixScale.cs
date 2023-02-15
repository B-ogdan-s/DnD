using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class UpdateMatrixScale
{
    public (T[,], List <Vector2Int>) AddLayer<T>(T[,] oldMatrix, Vector2Int oldScale, Vector2Int newScale, int vector)
    {
        Vector2Int delta = newScale - oldScale;
        List<Vector2Int> freeList = new List<Vector2Int>();
        T[,] newMatrix = new T[newScale.x, newScale.y];

        for(int i = 0; i < oldScale.x; i++)
        {
            for(int j = 0; j < oldScale.y; j++)
            {
                if (vector > 0)
                    newMatrix[i, j] = oldMatrix[i, j];
                else
                    newMatrix[i + delta.x, j + delta.y] = oldMatrix[i, j];
            }
        }

        for(int i = 0; i < oldScale.x * delta.y + oldScale.y * delta.x; i++)
        {
            if (vector > 0)
                freeList.Add(new Vector2Int(i * delta.y + (newScale.x - 1) * delta.x,
                    i * delta.x + (newScale.y - 1) * delta.y));
            else
                freeList.Add(new Vector2Int(i * delta.y,
                    i * delta.x));
        }

        return (newMatrix, freeList);
    }
    public (T[,], List<T>) RemoveLayer<T>(T[,] oldMatrix, Vector2Int oldScale, Vector2Int newScale, int vector)
    {
        Vector2Int delta = oldScale - newScale;
        List<T> deltaMatrix = new List<T>();
        T[,] newMatrix = new T[newScale.x, newScale.y];

        for(int i = 0; i < newScale.x; i++)
        {
            for(int j = 0; j < newScale.y; j++)
            {
                if (vector > 0)
                    newMatrix[i, j] = oldMatrix[i, j];
                else
                    newMatrix[i, j] = oldMatrix[i + delta.x, j + delta.y];
            }
        }

        for(int i = 0; i < oldScale.x * delta.y + oldScale.y * delta.x; i++)
        {
            if (vector > 0)
                deltaMatrix.Add(oldMatrix[i * delta.y + (oldScale.x - 1) * delta.x, 
                    i * delta.x + (oldScale.y - 1) * delta.y]);
            else
                deltaMatrix.Add(oldMatrix[i * delta.y ,
                    i * delta.x ]);
        }

        return (newMatrix, deltaMatrix);
    }
}
