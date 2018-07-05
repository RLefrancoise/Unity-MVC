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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Mvc.Unity;
using Other;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mvc.Screens.Unity
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for any Unity MVC screen application
    /// </summary>
    /// <typeparam name="TApplication">Type of the application</typeparam>
    public abstract class UnityMvcScreenApplication<TApplication> : UnityMvcApplication<TApplication> where TApplication : UnityMvcApplication<TApplication>
    {
        #region Fields

        /// <summary>
        /// Screen Manager
        /// </summary>
        private ScreenManager _screenManager;

        /// <summary>
        /// Gameobject containing all the scenes
        /// </summary>
        private GameObject _scenesGroupGameObject;

        /// <summary>
        /// Each root of each scene identified by the screen name
        /// </summary>
        private Dictionary<string, GameObject> _scenesRoots;

        /// <summary>
        /// Reference to the main scene
        /// </summary>
        private Scene _mainScene;

        /// <summary>
        /// Popup gameobject
        /// </summary>
        private GameObject _popupGameObject;

        /// <summary>
        /// Loading screen gameobject
        /// </summary>
        private GameObject _loadingGameObject;

        /// <summary>
        /// Folder containing the screens of the application
        /// </summary>
        public string ScreensFolder;

        /// <summary>
        /// Folder of the theme containing the scenes to use
        /// </summary>
        public string ThemeFolder;

        /// <summary>
        /// Time in seconds between two screens
        /// </summary>
        public float ScreenTransitionTime = 0.5f;

        #endregion

        #region Properties

        /// <summary>
        /// Get gameobject of current screen
        /// </summary>
        public GameObject CurrentScreenGameObject => (_screenManager.CurrentScreen as Component).gameObject;

        /// <summary>
        /// Get current theme path 
        /// </summary>
        public string CurrentThemePath => string.IsNullOrEmpty(ThemeFolder) ? $"{ScreensFolder}" : $"{ScreensFolder}/{ThemeFolder}";

        #endregion

        #region Intern Classes

        /// <summary>
        /// Arguments of OnScreenSceneLoaded event
        /// </summary>
        public class ScreenSceneLoadedEventArgs : EventArgs
        {
            /// <summary>
            /// Scene that has been loaded
            /// </summary>
            public Scene LoadedScene { get; set; }

            /// <summary>
            /// Gameobject that contains the screen
            /// </summary>
            public GameObject ScreenGameObject { get; set; }

            /// <summary>
            /// Identifier of the screen to load
            /// </summary>
            public IConvertible Screen { get; set; }

            /// <summary>
            /// Create screen mode
            /// </summary>
            public CreateScreenMode Mode { get; set; }

            /// <summary>
            /// Transition between loaded screen and current one
            /// </summary>
            public ScreenTransition Transition { get; set; }

            /// <summary>
            /// Data to send to the screen
            /// </summary>
            public object Data { get; set; }
        }

        #endregion

        #region Events
        /// <summary>
        /// Called when a screen scene is loaded
        /// </summary>
        private event EventHandler<ScreenSceneLoadedEventArgs> OnScreenSceneLoaded;

        #endregion
        
        #region MonoBehaviour
        private void Awake()
        {
            _screenManager = new ScreenManager();

            _scenesRoots = new Dictionary<string, GameObject>();
            _scenesGroupGameObject = new GameObject("Scenes");

            _mainScene = SceneManager.GetActiveScene();

            //Load popup scene
            SceneManager.LoadScene($"{CurrentThemePath}/Popup", LoadSceneMode.Additive);
            
            //Load loading scene
            SceneManager.LoadScene($"{CurrentThemePath}/Loading", LoadSceneMode.Additive);

            //Listen to screen loading
            OnScreenSceneLoaded += WhenScreenLoaded;
        }

        protected virtual void Start()
        {
            Scene popupScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 2);

            //Move popup scene content to scene group
            _popupGameObject = popupScene.GetRootGameObjects()[0];
            _popupGameObject.name = "Popup";
            _popupGameObject.transform.SetParent(_scenesGroupGameObject.transform);
            
            //Unload popup scene as it is not used anymore
            SceneManager.UnloadSceneAsync(popupScene);

            UnityPopupScreen.Instance.Hide();

            //Loading scene
            Scene loadingScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            _loadingGameObject = loadingScene.GetRootGameObjects()[0];
            _loadingGameObject.name = "Loading";
            _loadingGameObject.transform.SetParent(_scenesGroupGameObject.transform);
            _loadingGameObject.SetActive(false);

            SceneManager.UnloadSceneAsync(loadingScene);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a new screen for the application
        /// </summary>
        /// <param name="screenType">Screen type</param>
        /// <param name="mode">Create screen mode</param>
        /// <param name="transition">Transition between screens</param>
        /// <param name="data">Data to send to the screen</param>
        public void CreateScreen<TScreenType>(TScreenType screenType, CreateScreenMode mode, ScreenTransition transition = ScreenTransition.None, object data = null) where TScreenType : struct, IConvertible
        {
            StartCoroutine(_CreateScreen(screenType, mode, transition, data));
        }

        /// <summary>
        /// Pop the current screen and go back to previous one
        /// </summary>
        /// <param name="transition"></param>
        public void PopScreen(ScreenTransition transition = ScreenTransition.None)
        {
            StartCoroutine(_PopScreen(transition));
        }

        /// <summary>
        /// Display a popup message with OK button
        /// </summary>
        /// <param name="title">Title of the popup</param>
        /// <param name="message">Message of the popup</param>
        /// <param name="buttonClickedCallback">Callbakc to call when a button has been clicked</param>
        /// <param name="data">Data to send to the popup</param>
        public void PopupOk(string title, string message, PopupButtonClicked buttonClickedCallback = null, object data = null)
        {
            Popup(title, message, new []{"Ok"}, buttonClickedCallback);
        }

        /// <summary>
        /// Display a popup message with OK and Cancel buttons
        /// </summary>
        /// <param name="title">Title of the popup</param>
        /// <param name="message">Message of the popup</param>
        /// <param name="buttonClickedCallback">Callbakc to call when a button has been clicked</param>
        /// <param name="data">Data to send to the popup</param>
        public void PopupOkCancel(string title, string message, PopupButtonClicked buttonClickedCallback = null, object data = null)
        {
            Popup(title, message, new[] { "Ok", "Cancel" }, buttonClickedCallback);
        }

        /// <summary>
        /// Display a popup message
        /// </summary>
        /// <param name="title">Title of the popup</param>
        /// <param name="message">Message of the popup</param>
        /// <param name="button">Button of the popup</param>
        /// <param name="buttonClickedCallback">Callback to call when a button has been clicked</param>
        /// <param name="data">Data to send to the popup</param>
        public void Popup(string title, string message, string button, PopupButtonClicked buttonClickedCallback = null, object data = null)
        {
            Popup(title, message, new [] {button}, buttonClickedCallback, data);
        }

        /// <summary>
        /// Display a popup message
        /// </summary>
        /// <param name="title">Title of the popup</param>
        /// <param name="message">Message of the popup</param>
        /// <param name="buttons">Buttons of the popup</param>
        /// <param name="buttonClickedCallback">Callback to call when a button has been clicked</param>
        /// <param name="data">Data to send to the popup</param>
        public void Popup(string title, string message, string[] buttons, PopupButtonClicked buttonClickedCallback = null, object data = null)
        {
            CurrentScreenGameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

            _popupGameObject.transform.SetAsLastSibling();

            UnityPopupScreen.Instance.Title = title;
            UnityPopupScreen.Instance.Message = message;
            UnityPopupScreen.Instance.Buttons = buttons;
            UnityPopupScreen.Instance.Show(button =>
            {
                CurrentScreenGameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                buttonClickedCallback?.Invoke(button);
            });
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Destroy a screen (scene + remove from dictionary)
        /// </summary>
        /// <param name="screen"></param>
        private void _DeleteScreen(GameObject screen)
        {
            Destroy(_scenesRoots[screen.name]);
            _scenesRoots.Remove(screen.name);
        }

        /// <summary>
        /// Display / Hide loading screen
        /// </summary>
        /// <param name="show">show or hide</param>
        private void _ShowLoadingScreen(bool show)
        {
            //Show loading screen
            if(show) _loadingGameObject.transform.SetAsLastSibling();
            _loadingGameObject.SetActive(show);
        }

        #endregion

        #region Callbacks
        /// <summary>
        /// Callback when a screen is loaded.
        /// Add the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void WhenScreenLoaded(object sender, ScreenSceneLoadedEventArgs args)
        {
            //Active loaded scene
            SceneManager.SetActiveScene(args.LoadedScene);

            //Add screen to screen manager
            IScreen screen = args.ScreenGameObject.GetComponent<IScreen>();

            //Check if we need to destroy current scene (when we are in SET mode)
            IScreen currentScreen = _screenManager.CurrentScreen;
            bool unloadScene = args.Mode == CreateScreenMode.Set && _screenManager.NumberOfScreens != 0;

            if (args.Mode == CreateScreenMode.Push)
                _screenManager.PushScreen(screen, args.Data);
            else
                _screenManager.SetScreen(screen, args.Data);

            //If we need to unload screen scene, unload it
            if (unloadScene)
            {
                string currentScreenName = currentScreen.GetType().Name.Replace("Screen", "");
                SceneManager.UnloadSceneAsync($"{CurrentThemePath}/{currentScreenName}");
                //Destroy scene node in scene group
                _DeleteScreen(_scenesRoots[currentScreenName]);
            }
        }
        #endregion

        #region Coroutines

        /// <summary>
        /// Coroutine to load screen scene asynchronously
        /// </summary>
        /// <param name="screenType">Screen type</param>
        /// <param name="mode">Create screen mode</param>
        /// <param name="transition">Transition between screens</param>
        /// <param name="data">Data to send to the screen</param>
        /// <returns></returns>
        private IEnumerator _CreateScreen<TScreenType>(TScreenType screenType, CreateScreenMode mode, ScreenTransition transition, object data) where TScreenType : struct, IConvertible
        {
            //Check if type is an enum
            Type t = typeof(TScreenType);
            if(!t.IsEnum) throw new Exception("Screen type is not an enumeration");

            //We can deduce the name of the scene from the screenType
            string screenName = screenType.ToString(CultureInfo.InvariantCulture);
            string scenePath = $"{CurrentThemePath}/{screenName}";

            //Blocks interactions while loading
            if(_screenManager.NumberOfScreens != 0) CurrentScreenGameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

            //Show loading screen
            _ShowLoadingScreen(true);

            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            while (!asyncOp.isDone) yield return new WaitForEndOfFrame();

            Scene loadedScene = SceneManager.GetSceneByName(screenName);
            while(!loadedScene.isLoaded) yield return new WaitForEndOfFrame();

            //Hide loading screen
            _ShowLoadingScreen(false);

            //Add scene to scene group
            _scenesRoots.Add(screenName, loadedScene.GetRootGameObjects()[0]);
            
            loadedScene.GetRootGameObjects()[0].name = screenName;
            loadedScene.GetRootGameObjects()[0].transform.SetParent(_scenesGroupGameObject.transform);

            //Play transition between screens
            yield return _PlayScreenTransition(_scenesRoots[screenName], transition);

            OnScreenSceneLoaded?.Invoke(this, new ScreenSceneLoadedEventArgs
            {
                LoadedScene = loadedScene,
                ScreenGameObject = _scenesRoots[screenName],
                Mode = mode,
                Screen = screenType,
                Transition = transition,
                Data = data
            });
        }

        /// <summary>
        /// Coroutine to pop a screen
        /// </summary>
        /// <param name="transition">Transition to use when poping the screen</param>
        /// <returns></returns>
        private IEnumerator _PopScreen(ScreenTransition transition)
        {
            Scene activeScene = SceneManager.GetActiveScene();

            //Play pop transition
            yield return _PlayScreenTransition(CurrentScreenGameObject, transition);

            //Pop the screen
            IScreen screen = _screenManager.PopScreen();

            //Delete scene node in scene group
            //_DeleteScreen(CurrentScreenGameObject);
            _DeleteScreen((screen as Component).gameObject);

            //unload active scene (which is current screen scene)
            AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(activeScene);
            while(!asyncOp.isDone) yield return new WaitForEndOfFrame();

            //If no more screens in the stack, make main scene active
            if (_screenManager.NumberOfScreens == 0)
            {
                SceneManager.SetActiveScene(_mainScene);
            }
            //Else make current screen scene active
            else
            {
                SceneManager.SetActiveScene(CurrentScreenGameObject.scene);
                //Reenable interactions
                CurrentScreenGameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }

        /// <summary>
        /// Play transition between screens
        /// </summary>
        /// <param name="screenGameObject">GameObject of the screen to move</param>
        /// <param name="screenTransition">Transition to play</param>
        /// <returns></returns>
        private IEnumerator _PlayScreenTransition(GameObject screenGameObject, ScreenTransition screenTransition)
        {
            switch (screenTransition)
            {
                case ScreenTransition.MoveLeft:
                    yield return screenGameObject.transform.GetChild(0).Move(new Vector3(-screenGameObject.GetComponent<Canvas>().pixelRect.width, 0, 0), Vector3.zero, ScreenTransitionTime);
                    break;
                case ScreenTransition.MoveUp:
                    yield return screenGameObject.transform.GetChild(0).Move(new Vector3(0, -screenGameObject.GetComponent<Canvas>().pixelRect.height, 0), Vector3.zero, ScreenTransitionTime);
                    break;
                case ScreenTransition.MoveDown:
                    yield return screenGameObject.transform.GetChild(0).Move(new Vector3(0, screenGameObject.GetComponent<Canvas>().pixelRect.height, 0), Vector3.zero, ScreenTransitionTime);
                    break;
                case ScreenTransition.MoveRight:
                    yield return screenGameObject.transform.GetChild(0).Move(new Vector3(screenGameObject.GetComponent<Canvas>().pixelRect.width, 0, 0), Vector3.zero, ScreenTransitionTime);
                    break;
                case ScreenTransition.ExitLeft:
                    yield return screenGameObject.transform.GetChild(0).Move(Vector3.zero, new Vector3(-screenGameObject.GetComponent<Canvas>().pixelRect.width, 0, 0), ScreenTransitionTime);
                    break;
                case ScreenTransition.ExitUp:
                    yield return screenGameObject.transform.GetChild(0).Move(Vector3.zero, new Vector3(-screenGameObject.GetComponent<Canvas>().pixelRect.height, 0, 0), ScreenTransitionTime);
                    break;
                case ScreenTransition.ExitDown:
                    yield return screenGameObject.transform.GetChild(0).Move(Vector3.zero, new Vector3(screenGameObject.GetComponent<Canvas>().pixelRect.height, 0, 0), ScreenTransitionTime);
                    break;
                case ScreenTransition.ExitRight:
                    yield return screenGameObject.transform.GetChild(0).Move(Vector3.zero, new Vector3(screenGameObject.GetComponent<Canvas>().pixelRect.width, 0, 0), ScreenTransitionTime);
                    break;
                default:
                    yield break;
            }
        }
        #endregion
    }
}
