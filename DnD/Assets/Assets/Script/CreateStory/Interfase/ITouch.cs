using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouch
{
    public abstract void TouchDown();
    public abstract void TouchHolding(Vector3 touchPos);
    public abstract void TouchUp();
}
