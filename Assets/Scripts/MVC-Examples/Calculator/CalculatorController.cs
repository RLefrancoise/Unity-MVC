// This file is part of the Unity-MVC Project
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
using System.Text.RegularExpressions;

namespace Mvc.Examples.Calculator
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of a calculator controller
    /// </summary>
    public class CalculatorController : ICalculatorController
    {
        /// <summary>
        /// The calculator model to use with this controller
        /// </summary>
        private ICalculatorModel Model { get; set; }

        /// <inheritdoc />
        public ICalculatorView View { get; private set; }

        /// <summary>
        /// Is operation given already? Used to know if we are typing first or second operand
        /// </summary>
        private bool operationGiven;

        /// <summary>
        /// Is result computed ? Used to know if we should clear the calculator before typing first operand.
        /// </summary>
        private bool resultComputed;

        /// <summary>
        /// Create a new calculator controller
        /// </summary>
        /// <param name="model">The model to use with the controller</param>
        /// <param name="view">The view to use with the controller</param>
        public CalculatorController(ICalculatorModel model, ICalculatorView view)
        {
            Model = model;
            View = view;

            //listen View key pressed
            View.OnKeyPressed += HandleKeyPressed;

            //listen Model operands & operation
            Model.OnOperandChanged += HandleOperandChanged;
            Model.OnOperationChanged += HandleOperationChanged;
            Model.OnResultComputed += HandleResultComputed;
            Model.OnCleared += HandleModelCleared;

            //Clear model at beginning
            Model.Clear();
        }

        /// <summary>
        /// Handles view OnKeyPressed
        /// </summary>
        /// <param name="sender">sender of the event</param>
        /// <param name="args">arguments of the event. It contains the typed key.</param>
        private void HandleKeyPressed(object sender, CalculatorKeyPressedEventArgs args)
        {
            //is clear or result has already been computed
            if(args.Key == "CE" || resultComputed)
            {
                Model.Clear();
                resultComputed = false;
                operationGiven = false;
            }

            //is a digit
            if(Regex.IsMatch(args.Key, "^[0-9]$"))
            {
                if(!operationGiven)
                {
                    Model.FirstOperand = int.Parse(Model.FirstOperand + args.Key);
                }
                else
                {
                    Model.SecondOperand = int.Parse(Model.SecondOperand + args.Key);
                }
            }
            //is an operand
            else if(Regex.IsMatch(args.Key, @"^[\+\-\*\/]$"))
            {
                operationGiven = true;
                switch(args.Key)
                {
                    case "+":
                        Model.Operation = CalculatorOperation.Add;
                        break;
                    case "-":
                        Model.Operation = CalculatorOperation.Sub;
                        break;
                    case "*":
                        Model.Operation = CalculatorOperation.Mul;
                        break;
                    case "/":
                        Model.Operation = CalculatorOperation.Div;
                        break;
                }
            }
            //is = button
            else if(args.Key == "=")
            {
                if(!resultComputed && operationGiven)
                    Model.ComputeResult();
            }
        }

        /// <summary>
        /// Handles model OnOperandChanged event.
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="args">Contains the changed operand value</param>
        private void HandleOperandChanged(object sender, CalculatorOperandChangedEventArgs args)
        {            
            if(args.Operand == "first") View.FirstOperand = args.Value;
            else View.SecondOperand = args.Value;
        }

        /// <summary>
        /// Handles model OnOperationChanged event.
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="args">Contains the new value of the model operation</param>
        private void HandleOperationChanged(object sender, CalculatorOperationChangedEventArgs args)
        {
            View.Operation = args.Operation;
        }

        /// <summary>
        /// Handles model OnResultComputed event.
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="args">Contains the value of the result</param>
        private void HandleResultComputed(object sender, CalculatorResultComputedEventArgs args)
        {
            View.Result = args.Result;
            resultComputed = true;
        }

        /// <summary>
        /// Handles model OnCleared event.
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="args">Empty arguments</param>
        private void HandleModelCleared(object sender, EventArgs args)
        {
            View.FirstOperand = Model.FirstOperand;
            View.SecondOperand = Model.SecondOperand;
            View.Operation = Model.Operation;
            View.Result = Model.Result;
        }
    }
}