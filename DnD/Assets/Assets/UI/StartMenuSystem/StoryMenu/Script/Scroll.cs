using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace UI.StartMenu
{
    public class Scroll : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Transform _content;
        [SerializeField] private float _speed;
        [SerializeField] private int _id;

        private List<GameObject> _storyPrefabs = new List<GameObject>();

        Coroutine _scrollCorutine;
        Tweener _DoTweenScroll;

        private void Start()
        {
            _text.DOFade(0, 0);
            Story.StoryPanel[] story = GetComponentsInChildren<Story.StoryPanel>();

            for(int i = 0; i < story.Length; i++)
            {
                _storyPrefabs.Add(story[i].gameObject);
                story[i].gameObject.GetComponent<CanvasGroup>().alpha = 0;
            }

            if(_storyPrefabs.Count == 0)
            {
                GetComponent<ScrollRect>().enabled = false;
            }
        }

        private void FixedUpdate()
        {
            float min = float.MaxValue;
            for(int i = 0; i < _storyPrefabs.Count; i++)
            {
                if (Mathf.Abs(_content.localPosition.x + _storyPrefabs[i].transform.localPosition.x) < min)
                {
                    min = Mathf.Abs(_content.localPosition.x + _storyPrefabs[i].transform.localPosition.x);
                    _id = i;
                }
            }
        }

        public void Create()
        {
            if (_storyPrefabs.Count == 0)
            {
                _text.DOFade(1, 0.5f);
                return;
            }
            StartCoroutine(CR_create());
        }

        private IEnumerator CR_create()
        {
            foreach(var obj in _storyPrefabs)
            {
                obj.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
                yield return new WaitForSeconds(0.2f);
            }
        }

        public void ScrollStart()
        {
            if (_scrollCorutine != null)
            {
                _DoTweenScroll.Pause();
                StopCoroutine(_scrollCorutine);
            }
            //_content.DOLocalMoveX(_content.transform.localPosition.x, 0);
        }

        public void ScrollStop()
        {
            if (_storyPrefabs.Count != 0)
            {
                _scrollCorutine = StartCoroutine(CR_ScrollStop());
            }
        }

        private IEnumerator CR_ScrollStop()
        {
            float l = Mathf.Abs(_content.localPosition.x + _storyPrefabs[_id].transform.localPosition.x);
            float time = l / _speed;
            _DoTweenScroll = _content.DOLocalMoveX(-_storyPrefabs[_id].transform.localPosition.x, time).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time);
        }
    }
}
