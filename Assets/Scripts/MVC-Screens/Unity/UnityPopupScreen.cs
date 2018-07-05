// This file is part of the Unity-MVC project
// https://github.com/RLefrancoise/Unity-MVC
// 
// BSD 3-Clause License
// 
// Copyright (c) 2018, Renaud Lefrancoise
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.
// 
// * Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived from
//   this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
using System.Collections;
using System.Collections.Generic;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace Mvc.Screens.Unity
{
    /// <inheritdoc cref="IPopupScreen"/>
    /// <summary>
    /// Popup implementation for Unity
    /// </summary>
    public class UnityPopupScreen : UnitySingleton<UnityPopupScreen>, IPopupScreen
    {
        [SerializeField]
        private Text _title;
        [SerializeField]
        private Text _message;
        [SerializeField]
        private GameObject _buttonsGroup;

        [SerializeField]
        private GameObject _buttonPrefab;

        private PopupButtonClicked _buttonClickedCallback;
        private List<Button> _buttons;

        #region MonoBehaviour

        protected void Awake()
        {
            _buttons = new List<Button>();
        }

        #endregion

        #region Properties

        public string Title
        {
            get { return _title.text; }
            set { _title.text = value; }
        }

        public string Message
        {
            get { return _message.text; }
            set { _message.text = value; }
        }

        public string[] Buttons
        {
            get
            {
                string[] buttons = new string[_buttons.Count];
                int i = 0;
                _buttons.ForEach(button =>
                {
                    buttons[i] = button.GetComponentInChildren<Text>().text;
                    i++;
                });

                return buttons;
            }
            set
            {
                //Clear previous button
                _buttons.ForEach((button) =>
                {
                    button.onClick.RemoveAllListeners();
                    Destroy(button.gameObject);
                });

                _buttons.Clear();

                //Create each button from names
                foreach(var buttonName in value)
                {
                    //Create button
                    GameObject button = Instantiate(_buttonPrefab, _buttonsGroup.transform);
                    button.name = buttonName;
                    button.GetComponentInChildren<Text>().text = buttonName;
                    button.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        NotifyAndCloseOnClick(buttonName);
                    });

                    //Add button to list
                    _buttons.Add(button.GetComponent<Button>());
                }
            }
        }

        #endregion

        public virtual void Show(PopupButtonClicked buttonClickedCallback = null, object data = null)
        {
            _buttonClickedCallback = buttonClickedCallback;
            gameObject.SetActive(true);
            StartCoroutine(transform.GetChild(0).Scale(Vector3.zero, Vector3.one, 0.25f));
        }

        public void Hide()
        {
            _buttonClickedCallback = null;
            StartCoroutine(_Hide());
        }

        #region Callbacks

        private void NotifyAndCloseOnClick(string button)
        {
            _buttonClickedCallback?.Invoke(button);
            Hide();
        }

        #endregion

        #region Coroutines

        private IEnumerator _Hide()
        {
            yield return transform.GetChild(0).Scale(Vector3.one, Vector3.zero, 0.25f);
            gameObject.SetActive(false);
        }

        #endregion
    }
}
