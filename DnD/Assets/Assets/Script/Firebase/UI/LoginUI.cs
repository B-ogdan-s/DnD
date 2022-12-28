using FirebaseUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FirebaseUI
{
    public class LoginUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputEmail;
        [SerializeField] private TMP_InputField _inputPassword;

        [SerializeField] private Button _button;

        private string _emailString = "";
        private string _passwordString = "";

        public static event Action<string, string> _Login;

        private void Start()
        {
            FirebasePanelUI._OnClear += Clear;

            _inputEmail.onEndEdit.AddListener((string value) =>
            {
                _emailString = value;
            });
            _inputPassword.onEndEdit.AddListener((string value) =>
            {
                if (value.Length >= 8)
                {
                    _passwordString = value;
                }
                else
                {
                    _inputPassword.text = "";
                    _passwordString = "";
                }
            });
            _button.onClick.AddListener(() =>
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                System.Text.RegularExpressions.Match match = regex.Match(_emailString);
                if (!match.Success)
                {
                    ErrorUI._instance.SetError("Incorrect mail");
                    return;
                }
                _Login?.Invoke(_emailString, _passwordString);
            });
        }

        public void Clear()
        {
            _inputEmail.text = "";
            _inputPassword.text = "";
            _emailString = "";
            _passwordString = "";
        }
        private void OnDestroy()
        {
            FirebasePanelUI._OnClear -= Clear;
        }
    }
}
