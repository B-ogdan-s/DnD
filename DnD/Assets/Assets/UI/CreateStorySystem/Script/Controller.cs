using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.CreateStory
{
    public class Controller : MonoBehaviour
    {
        private FadeAnimation _fade;

        private void Start()
        {
            _fade = GetComponentInChildren<FadeAnimation>();
            _fade.Close();
        }
    }
}
