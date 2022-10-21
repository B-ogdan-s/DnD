using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CreateStory
{
    [CreateAssetMenu(fileName = "ListTypeInfo", menuName = "CreateStory/List/List")]
    public class ListInfo : ScriptableObject
    {
        [SerializeField] private List<ListTypeInfo> _listInfos;
    }

    //[CreateAssetMenu(fileName = "ListTypeInfo", menuName = "CreateStory/List/ListTypeInfo")]
    public abstract class ListTypeInfo : ScriptableObject
    {
        //[SerializeField] private string 
    }

}
