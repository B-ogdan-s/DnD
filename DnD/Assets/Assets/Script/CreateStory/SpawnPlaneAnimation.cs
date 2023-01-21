using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlaneAnimation : MonoBehaviour
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _horizontalTime;
    [SerializeField] private float _verticalTime;
    public IEnumerator CR_PlanesAnimation(GameObject[,] masObj, int heigh, int length)
    {
        for (int i = 0; i < (length + heigh); i++)
        {
            StartCoroutine(CR_Plane(masObj, heigh, length, i));
            yield return new WaitForSeconds(_horizontalTime);
        }
    }
    private IEnumerator CR_Plane(GameObject[,] masObj, int heigh, int length, int sum)
    {
        for (int j = 0; j < heigh; j++)
        {
            for (int k = 0; k < length; k++)
            {
                if (j + k == sum)
                {
                    Vector3 pos = masObj[j, k].transform.localPosition;
                    masObj[j, k].transform.localPosition += new Vector3(0, 5, 0);
                    masObj[j, k].SetActive(true);
                    masObj[j, k].transform.DOLocalMove(pos, _spawnTime);
                    yield return new WaitForSeconds(_verticalTime);
                }
            }
        }
    }
}
