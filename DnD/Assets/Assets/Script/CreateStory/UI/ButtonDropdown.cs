using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CreateStory
{
    public class ButtonDropdown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameButton;
        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] private Transform _parents;

        [SerializeField] private Button _button;

        public void StartSetings(string name, List<TileInfo> tiles)
        {
            _button.onClick.AddListener(Open);
            _nameButton.text = name;

            foreach (var tile in tiles)
            {
                GameObject obj = Instantiate(_buttonPrefab);
                obj.transform.SetParent(_parents, false);
                obj.GetComponent<Brush>().StartSettings(tile);
            }
        }

        private void Open()
        {
            _parents.gameObject.SetActive(true);
            _button.onClick.RemoveListener(Open);
            _button.onClick.AddListener(Close);
        }
        private void Close()
        {
            _parents.gameObject.SetActive(false);
            _button.onClick.RemoveListener(Close);
            _button.onClick.AddListener(Open);
        }
    }
}
