using System.Collections.Generic;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace Mvc.Screens.Unity
{
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

        public List<string> Buttons
        {
            get
            {
                List<string> buttons = new List<string>();
                _buttons.ForEach(button =>
                {
                    buttons.Add(button.GetComponentInChildren<Text>().text);
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
                value.ForEach(buttonName =>
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
                });
            }
        }

        #endregion

        /*#region Events

        public event EventHandler<PopupScreenButtonArgs> OnButtonClicked = (sender, e) => { };

        #endregion*/

        public void Show(PopupButtonClicked buttonClickedCallback = null)
        {
            _buttonClickedCallback = buttonClickedCallback;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _buttonClickedCallback = null;
            gameObject.SetActive(false);
        }

        #region Callbacks

        private void NotifyAndCloseOnClick(string button)
        {
            _buttonClickedCallback?.Invoke(button);
            Hide();
        }

        #endregion
    }
}
