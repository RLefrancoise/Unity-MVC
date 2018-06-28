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
using System.Collections.Generic;
using Mvc;
using Mvc.Unity;

namespace MVC.Unity
{
    /// <inheritdoc cref="AbstractFactory"/>
    /// <inheritdoc cref="IControllerFactory"/>
    /// <summary>
    /// Abstract controller factory for unity controllers
    /// </summary>
    /// <typeparam name="TControllerFactoryParams">Type of the controller factory params</typeparam>
    public abstract class AbstractControllerFactory<TControllerFactoryParams> : AbstractFactory, IControllerFactory where TControllerFactoryParams : IControllerFactoryParams
    {
        public List<IController> Controllers { get; private set; }

        protected AbstractControllerFactory()
        {
            Controllers = new List<IController>();
        }

        /// <summary>
        /// Create a controller from the parameters specified through the template parameter
        /// </summary>
        /// <param name="controllerType">The type of the controller to create</param>
        /// <param name="parameters">Parameters to create the controller</param>
        /// <returns></returns>
        protected abstract IController CreateController(Type controllerType, TControllerFactoryParams parameters);

        /// <summary>
        /// Create a controller from the parameters specified through the template parameter
        /// </summary>
        /// <typeparam name="TController">The type of the controller to create</typeparam>
        /// <param name="parameters">Parameters to create the controller</param>
        /// <returns></returns>
        protected abstract TController CreateController<TController>(TControllerFactoryParams parameters) where TController : IController;

        public IController CreateController(Type controllerType, IControllerFactoryParams parameters)
        {
            IController controller = CreateController(controllerType, (TControllerFactoryParams) parameters);
            Controllers.Add(controller);
            return controller;
        }

        public IController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController
        {
            TController controller = CreateController<TController>((TControllerFactoryParams) parameters);
            Controllers.Add(controller);
            return controller;
        }
    }
}
