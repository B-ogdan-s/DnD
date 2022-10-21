using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class FadeUI : MonoBehaviour
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

        public void Close()
        {
            _fade.enabled = true;
            _fadePanel.GetComponent<Image>().DOFade(1, 0);
            _slider.DOFade(1, 0);
            StartCoroutine(CR_Close());
        }

        private IEnumerator CR_Open()
        {
            _fadePanel.GetComponent<Image>().DOFade(1, _time);
            yield return new WaitForSeconds(_timePause);
            _slider.DOFade(1, _time);
        }

        private IEnumerator CR_Close()
        {
            _slider.GetComponentInChildren<Slider>().value = 1;
            _slider.DOFade(0, _time);
            yield return new WaitForSeconds(_timePause);
            _fadePanel.GetComponent<Image>().DOFade(0, _time);
            yield return new WaitForSeconds(_time);
            _fade.enabled = false;
        }
    }
}
