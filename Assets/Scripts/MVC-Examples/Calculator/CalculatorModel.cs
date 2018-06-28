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

namespace Mvc.Examples.Calculator
{
    /// <inheritdoc />
    /// <summary>
    /// Model of the calculator
    /// </summary>
    public class CalculatorModel : ICalculatorModel
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

        public int FirstOperand
        {
            get { return firstOperand; }
            set
            {
                if(firstOperand != value)
                {
                    firstOperand = value;
                    OnOperandChanged.Invoke(this, new CalculatorOperandChangedEventArgs("first", value));
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
                    OnOperandChanged.Invoke(this, new CalculatorOperandChangedEventArgs("second", value));
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
                    OnOperationChanged.Invoke(this, new CalculatorOperationChangedEventArgs(value));
                }
            }
        }

        public float Result { get; private set; }

        /// <summary>
        /// Create a new calculator model
        /// </summary>
        public CalculatorModel()
        {
            Clear();
        }

        public void ComputeResult()
        {
            switch(Operation)
            {
                case CalculatorOperation.Add:
                    Result = FirstOperand + SecondOperand;
                    break;
                case CalculatorOperation.Sub:
                    Result = FirstOperand - SecondOperand;
                    break;
                case CalculatorOperation.Mul:
                    Result = FirstOperand * SecondOperand;
                    break;
                case CalculatorOperation.Div:
                    Result = (float) FirstOperand / SecondOperand;
                    break;
                default:
                    throw new Exception("Invalid operation");
            }

            OnResultComputed.Invoke(this, new CalculatorResultComputedEventArgs(Result));
        }

        public void Clear()
        {
            FirstOperand = 0;
            SecondOperand = 0;
            Operation = CalculatorOperation.Unknown;
            Result = 0;

            OnCleared.Invoke(this, new EventArgs());
        }

        public event EventHandler<CalculatorOperandChangedEventArgs> OnOperandChanged = (sender, e) => {};

        public event EventHandler<CalculatorOperationChangedEventArgs> OnOperationChanged = (sender, e) => {};

        public event EventHandler<CalculatorResultComputedEventArgs> OnResultComputed = (sender, e) => {};

        public event EventHandler<EventArgs> OnCleared = (sender, args) => {};
    }
}