using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace StartMenu
{
    public class FadeAnimation : MonoBehaviour
    {
        [SerializeField] private Canvas _fade;
        [SerializeField] private GameObject _fadePanel;
        [SerializeField] private CanvasGroup _slider;
        [SerializeField] private float _time;
        [SerializeField] private float _timePause;

        public Slider Slider => _slider.GetComponentInChildren<Slider>();

        private void Start()
        {
            _fade.enabled = true;
            _fadePanel.GetComponent<Image>().DOFade(0, 0);
            _slider.DOFade(0, 0);
            _fade.enabled = false;
        }

        public void Open()
        {
            _fade.enabled = true;
            StartCoroutine(CR_Open());
        }

        private IEnumerator CR_Open()
        {
            _fadePanel.GetComponent<Image>().DOFade(1, _time);
            yield return new WaitForSeconds(_timePause);
            _slider.DOFade(1, _time);
        }
    }
}
