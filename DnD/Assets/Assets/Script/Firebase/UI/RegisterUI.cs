using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FirebaseUI
{
    public class RegisterUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputEmail;
        [SerializeField] private TMP_InputField _inputName;
        [SerializeField] private TMP_InputField _inputPassword;
        [SerializeField] private TMP_InputField _inputConfirmPassword;

        [SerializeField] private Button _button;

        private string _emailString = "";
        private string _nameString = "";
        private string _passwordString = "";
        private string _confirmPasswordString = "";

        private void Start()
        {
            _inputEmail.onEndEdit.AddListener((string value) =>
            {
                _emailString = value;
            });
            _inputName.onEndEdit.AddListener((string value) =>
            {
                _nameString = value;
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
            _inputConfirmPassword.onEndEdit.AddListener((string value) =>
            {
                if (value.Length < 8)
                {
                    _inputConfirmPassword.text = "";
                    _confirmPasswordString = "";
                    return;
                }
                _confirmPasswordString = value;

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
                if (string.IsNullOrEmpty(_nameString))
                {
                    ErrorUI._instance.SetError("Empty name");
                    return;
                }
                if(_confirmPasswordString != _passwordString)
                {
                    ErrorUI._instance.SetError("Password does not match");
                    return;
                }
            });
        }
    }
}
