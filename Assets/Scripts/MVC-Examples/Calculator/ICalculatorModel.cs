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
    /// <summary>
    /// Arguments of OnOperandChanged event
    /// </summary>
    public class CalculatorOperandChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Operand that changed : "first" if first operand has changed, "second" if second operand has changed
        /// </summary>
        public string Operand { get; private set; }
        /// <summary>
        /// The value of the changed operand
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Create a new argument
        /// </summary>
        /// <param name="operand">"first" if first operand has changed, "second" if second operand has changed</param>
        /// <param name="value">the value of the changed operand</param>
        public CalculatorOperandChangedEventArgs(string operand, int value)
        {
            Operand = operand;
            Value = value;
        }
    }

    /// <summary>
    /// Arguments of OnOperationChanged event
    /// </summary>
    public class CalculatorOperationChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The new value of the operation
        /// </summary>
        public CalculatorOperation Operation { get; private set; }

        /// <summary>
        /// Create a new argument
        /// </summary>
        /// <param name="operation">The value of the new operation</param>
        public CalculatorOperationChangedEventArgs(CalculatorOperation operation)
        {
            Operation = operation;
        }
    }

    /// <summary>
    /// Arguments of OnResultComputed event
    /// </summary>
    public class CalculatorResultComputedEventArgs : EventArgs
    {
        /// <summary>
        /// The value of the computed result
        /// </summary>
        public float Result { get; private set; }

        /// <summary>
        /// Create a new argument
        /// </summary>
        /// <param name="result">the value of the new result</param>
        public CalculatorResultComputedEventArgs(float result)
        {
            Result = result;
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// Interface for a calculator model
    /// </summary>
    public interface ICalculatorModel : IModel
    {
        /// <summary>
        /// The first operand of the calculator
        /// </summary>
        int FirstOperand { get; set; }

        /// <summary>
        /// The second operand of the calculator
        /// </summary>
        int SecondOperand { get; set; }

        /// <summary>
        /// The operation of the calculator
        /// </summary>
        CalculatorOperation Operation { get; set; }

        /// <summary>
        /// The result of the operation using the two operands
        /// </summary>
        float Result { get; }

        /// <summary>
        /// Computer the result from operands & operation
        /// </summary>
        void ComputeResult();

        /// <summary>
        /// Clear the calculator model by reseting operands & operation
        /// </summary>
        void Clear();

        /// <summary>
        /// Called when an operand of the model has changed
        /// </summary>
        event EventHandler<CalculatorOperandChangedEventArgs> OnOperandChanged;

        /// <summary>
        /// Called when the operation of the model has changed
        /// </summary>
        event EventHandler<CalculatorOperationChangedEventArgs> OnOperationChanged;

        /// <summary>
        /// Called when the result has been computed
        /// </summary>
        event EventHandler<CalculatorResultComputedEventArgs> OnResultComputed;

        /// <summary>
        /// Called when the model is cleared
        /// </summary>
        event EventHandler<EventArgs> OnCleared;
    }
}