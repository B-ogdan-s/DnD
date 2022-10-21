using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CreateStory
{
    [CreateAssetMenu(fileName = "StorysInfo", menuName = "Story/StoryInfo")]
    public class StorysInfo : ScriptableObject
    {
        [SerializeField] private List<StoryInfo> _storyInfo = new List<StoryInfo>();


        public void Create(StoryInfo inf)
        {
            _storyInfo.Add(inf);
        }
    }

    [System.Serializable]
    public class StoryInfo
    {
        [SerializeField] private int _x;
        [SerializeField] private int _y;
        private TypePlane[,] _planeType;

        public StoryInfo(int x, int y, TypePlane[,] planeType)
        {
            _x = x;
            _y = y;
            _planeType = planeType;
        }
    }
}
