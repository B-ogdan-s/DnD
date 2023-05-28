using System;
using UnityEngine;

public class DefaultTile : MonoBehaviour, ITouch
{
    private Vector2Int _position;

    public Action<Vector2Int> Down;

    public bool Check
    {
        get;
        private set;
    }
    public void Enable(Vector2Int newPosition)
    {
        gameObject.SetActive(true);
        _position = newPosition;
        Check = true;
    }
    public void Disable()
    {
        gameObject.SetActive(false);
        Check = false;
    }

    public void TouchDown()
    {
        Debug.Log("Down" + _position);
    }

    public void TouchHolding(Vector3 touchPos)
    {

    }

    public void TouchUp()
    {
        Debug.Log("Up" + _position);
    }
}
