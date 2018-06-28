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
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Mvc.Examples.Calculator
{
    /// <inheritdoc cref="ICalculatorView"/>
    /// <summary>
    /// The view for the calculator
    /// </summary>
    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        /// <summary>
        /// The first operand of the calculator
        /// </summary>
        private int firstOperand;

        /// <summary>
        /// The second operand of the calculator
        /// </summary>
        private int secondOperand;

        /// <summary>
        /// The operation of the calculator
        /// </summary>
        private CalculatorOperation operation;

        /// <summary>
        /// The result of the operation using the two operands
        /// </summary>
        private float result;

        public int FirstOperand
        { 
            get { return firstOperand; }
            set
            {
                if(firstOperand != value)
                {
                    firstOperand = value;
                    UpdateDisplay();
                }
            } 
        }

        public int SecondOperand
        { 
            get { return secondOperand; }
            set
            {
                if(secondOperand != value)
                {
                    secondOperand = value;
                    UpdateDisplay();
                }
            } 
        }

        public CalculatorOperation Operation
        { 
            get { return operation; }
            set
            {
                if(operation != value)
                {
                    operation = value;
                    UpdateDisplay();
                }
            } 
        }

        public float Result
        { 
            get { return result; }
            set
            {
                if(result != value)
                {
                    result = value;
                    UpdateDisplay(true);
                }
            } 
        }

        /// <summary>
        /// The text to display the result or the current calculation
        /// </summary>
        [SerializeField]
        private Text display;

        public event EventHandler<CalculatorKeyPressedEventArgs> OnKeyPressed = (sender, e) => {};

        void Start()
        {
            Button[] buttons = GetComponentsInChildren<Button>();
            foreach(var button in buttons)
            {
                button.onClick.AddListener(() => {
                    KeyPressed(button.GetComponentInChildren<Text>().text);
                });
            }
        }

        /// <summary>
        /// Update the display text
        /// </summary>
        /// <param name="showResult">Display the result or the current calculation</param>
        private void UpdateDisplay(bool showResult = false)
        {
            if(!showResult)
            {
                if(Operation != CalculatorOperation.Unknown)
                {
                    display.text = string.Format("{0} {1} {2}", 
                    FirstOperand,
                    OperationToString(),
                    SecondOperand != 0 ? SecondOperand.ToString() : "").Trim();
                }
                else
                {
                    display.text = FirstOperand.ToString();
                }
            }
            else
            {
                display.text = Result.ToString();
            }
        }

        /// <summary>
        /// Press a key on the calculator
        /// </summary>
        /// <param name="key">the key to press</param>
        public void KeyPressed(string key)
        {
            OnKeyPressed.Invoke(this, new CalculatorKeyPressedEventArgs(key));
        }

        /// <summary>
        /// Convert operation enum to a string
        /// </summary>
        /// <returns>the string matching the operation enum</returns>
        private string OperationToString()
        {
            switch(Operation)
            {
                case CalculatorOperation.Add:
                    return "+";
                case CalculatorOperation.Sub:
                    return "-";
                case CalculatorOperation.Mul:
                    return "*";
                case CalculatorOperation.Div:
                    return "/";
                default:
                    throw new Exception("Invalid operation");
            }
        }
    }
}