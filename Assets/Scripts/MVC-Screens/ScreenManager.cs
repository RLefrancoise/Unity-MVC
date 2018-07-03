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
using System.Collections.Generic;
using Other;

namespace Mvc.Screens
{
    /// <summary>
    /// The screen manager manages all the screens of the application. You can navigate between screens through it.
    /// </summary>
    public sealed class ScreenManager
    {
        /// <summary>
        /// Screen stack to handle the screens
        /// </summary>
        private Stack<IScreen> Screens { get; }

        /// <summary>
        /// Get the current screen being displayed
        /// </summary>
        public IScreen CurrentScreen => Screens.Count == 0 ? null : Screens.Peek();

        /// <summary>
        /// How many screens are managed ?
        /// </summary>
        public int NumberOfScreens => Screens.Count;

        /// <summary>
        /// Constructs a new screen manager
        /// </summary>
        public ScreenManager()
        {
            Screens = new Stack<IScreen>();
        }

        /// <summary>
        /// Push a screen to be displayed
        /// </summary>
        /// <param name="screen"></param>
        /// <returns>The pushed screen</returns>
        public IScreen PushScreen(IScreen screen)
        {
            //If there is already a screen displayed, hide it
            if (Screens.Count > 0)
            {
                Screens.Peek().OnHide();
            }

            Screens.Push(screen);
            screen.OnCreate();
            screen.OnShow();

            return screen;
        }

        /// <summary>
        /// Pop the current screen and show the previous screen if any
        /// </summary>
        /// <returns>The screen being popped</returns>
        public IScreen PopScreen()
        {
            //If there is no screen, return null
            if (Screens.Count <= 0) return null;

            IScreen screen = Screens.Pop();
            screen.OnHide();
            screen.OnDestroyed();

            //If there is a screen under the popped one, show it
            if (Screens.Count > 0)
            {
                Screens.Peek().OnShow();
            }

            return screen;
        }
        
        /// <summary>
        /// Set the current screen
        /// </summary>
        /// <param name="screen">The screen to set</param>
        public void SetScreen(IScreen screen)
        {
            PopScreen();
            PushScreen(screen);
        }

        /// <summary>
        /// Destroy all screens
        /// </summary>
        public void ClearScreens()
        {
            while (Screens.Count > 0)
            {
                PopScreen();
            }
        }
    }
}

