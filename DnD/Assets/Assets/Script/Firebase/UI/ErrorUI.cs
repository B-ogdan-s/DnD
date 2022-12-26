using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FirebaseUI
{
    public class ErrorUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _errorText;

        public static ErrorUI _instance;

        private void Start()
        {
            _errorText.text = "";
            _instance = this;
        }

        public void SetError(string message)
        {
            _errorText.text = message;
        }
    }
}
