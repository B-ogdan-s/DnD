using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    
    public class CharacterDiscriptionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _type;
        [SerializeField] private GameObject _textPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private float _sizeConvert;

        private List<TextMeshProUGUI> _discriptionList = new List<TextMeshProUGUI>();
        private StringInfo _dafeultInfo;

        private void Start()
        {
            _type.GetComponent<Button>().onClick.AddListener(()=> SetData(_dafeultInfo));
        }

        public void SetDefaultData(StringInfo stringInfo)
        {
            _dafeultInfo = stringInfo;
            SetData(_dafeultInfo);
        }
        public void SetData(StringInfo stringInfo)
        {
            ClearText();
            _name.text = stringInfo.Name;
            SpawnText(stringInfo.ListDescriptionInfos);
        }

        public void SetType(string type)
        {
            _type.text = type;
        }

        private void SpawnText(List<DescriptionInfo> descriptionInfos)
        {
            foreach (var info in descriptionInfos)
            {
                TextMeshProUGUI text = PullText();
                text.fontSize = info.FontSize;
                text.text = info.Discription;
                text.GetComponent<RectTransform>().sizeDelta = new Vector2(0,info.FontSize * info.LineCounter 
                                                                             + _sizeConvert * info.LineCounter * info.FontSize);
            }
        }

        private void ClearText()
        {
            foreach (var list in _discriptionList)
            {
                list.gameObject.SetActive(false);
            }
        }

        private TextMeshProUGUI PullText()
        {
            foreach (var discription in _discriptionList)
            {
                if (!discription.IsActive())
                {
                    discription.gameObject.SetActive(true);
                    return discription;
                }
            }
            TextMeshProUGUI text = Instantiate(_textPrefab, _parent).GetComponent<TextMeshProUGUI>();
            _discriptionList.Add(text);
            return text;
        }
    }

    [Serializable]
    public class StringInfo
    {
        [SerializeField] private string _name;
        [SerializeField] private List<DescriptionInfo> _descriptionInfos;

        public string Name => _name;
        public List<DescriptionInfo> ListDescriptionInfos => _descriptionInfos;
    }

    [Serializable]
    public class DescriptionInfo
    {
        [SerializeField] private int _lineCounter;
        [SerializeField] private int _fontSize;
        [MultiLineProperty(5)][SerializeField] private string _discription;
        

        public int LineCounter => _lineCounter;
        public int FontSize => _fontSize;
        public string Discription => _discription;
    }
}