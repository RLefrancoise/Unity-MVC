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
using Mvc.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mvc.Screens.Unity
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for any Unity MVC screen application
    /// </summary>
    /// <typeparam name="TApplication">Type of the application</typeparam>
    public abstract class UnityMvcScreenApplication<TApplication> : UnityMvcApplication where TApplication : UnityMvcApplication
    {
        #region Intern Classes

        /// <summary>
        /// Arguments of OnScreenSceneLoaded event
        /// </summary>
        public class ScreenSceneLoadedEventArgs : EventArgs
        {
            /// <summary>
            /// Screen data
            /// </summary>
            public ScreenData ScreenData { get; set; }
            /// <summary>
            /// Create screen mode
            /// </summary>
            public CreateScreenMode Mode { get; set; }
        }

        #endregion

        #region Events
        /// <summary>
        /// Called when a screen scene is loaded
        /// </summary>
        private event EventHandler<ScreenSceneLoadedEventArgs> OnScreenSceneLoaded;
        #endregion

        #region Fields
        private static TApplication _instance;
        #endregion

        #region Properties        
        public static TApplication Instance => _instance ?? (_instance = FindObjectOfType<TApplication>());

        public abstract Dictionary<IScreenType, ScreenData> Screens { get; }
        #endregion
        
        #region MonoBehaviour
        protected void Awake()
        {
            OnScreenSceneLoaded += WhenScreenLoaded;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new screen for the application
        /// </summary>
        /// <param name="screenType">Screen type</param>
        /// <param name="mode">Create screen mode</param>
        public void CreateScreen<TScreenType>(TScreenType screenType, CreateScreenMode mode) where TScreenType : struct, IScreenType
        {
            StartCoroutine(_CreateScreen(screenType, mode));
        }
        #endregion

        #region Callbacks
        /// <summary>
        /// Callback when a screen is loaded.
        /// Add the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void WhenScreenLoaded(object sender, ScreenSceneLoadedEventArgs args)
        {
            IScreen screen = (IScreen)FindObjectOfType(args.ScreenData.Type);

            if (args.Mode == CreateScreenMode.Push)
                ScreenManager.Instance.PushScreen(screen);
            else
                ScreenManager.Instance.SetScreen(screen);
        }
        #endregion

        #region Coroutines
        /// <summary>
        /// Coroutine to load screen scene asynchronously
        /// </summary>
        /// <param name="screenType">Screen type</param>
        /// <param name="mode">Create screen mode</param>
        /// <returns></returns>
        private IEnumerator _CreateScreen<TScreenType>(TScreenType screenType, CreateScreenMode mode) where TScreenType : struct, IScreenType
        {
            string scenePath = $"_Scenes/Screens/{Screens[screenType].Scene}";
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

            while (!asyncOp.isDone) yield return new WaitForEndOfFrame();

            OnScreenSceneLoaded?.Invoke(this, new ScreenSceneLoadedEventArgs
            {
                Mode = mode,
                ScreenData = Screens[screenType] 
            });
        }
        #endregion
    }
}
