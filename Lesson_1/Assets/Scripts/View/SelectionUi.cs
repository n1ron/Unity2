﻿using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
    public sealed class SelectionUi : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public string Text
        {
            set => _text.text = value;
        }

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }
    }
}


