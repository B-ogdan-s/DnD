using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPanel : MonoBehaviour, ITouch
{
    private Vector2 _position;
    
    public bool Check
    {
        get;
        private set;
    }

    public void Enable(Vector2 pos)
    {
        gameObject.SetActive(true);
        _position = pos;
        Check = true;
    }
    public void Disable()
    {
        gameObject.SetActive(false);
        Check = false;
    }

    public void TouchDown()
    {
        Debug.Log(_position);
    }

    public void TouchHolding(Vector3 touchPos)
    {

    }

    public void TouchUp()
    {

    }
}
