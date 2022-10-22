using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Brush : MonoBehaviour
{

    public event System.Action<Brush> BrushChanged;

    public void Create( int num)
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Yes_" + num);
        });
    }


}
