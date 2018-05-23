using System;
using MVC;

namespace Calculator
{
    /// <summary>
    /// Arguments of OnKeyPressed event triggered by ICalculatorView
    /// </summary>
    public class CalculatorKeyPressedEventArgs : EventArgs
    {
        public CalculatorKeyPressedEventArgs(string key)
        {
            Key = key;
        }

        /// <summary>
        /// The key being pressed
        /// </summary>
        public string Key { get; private set; }
    }

    /// <inheritdoc />
    /// <summary>
    /// Interface for a view of the calculator
    /// </summary>
    public interface ICalculatorView : IView
    {
        /// <summary>
        /// The first operand of the calculator
        /// </summary>
        int FirstOperand { set; }

        /// <summary>
        /// The second operand of the calculator
        /// </summary>
        int SecondOperand { set; }

        /// <summary>
        /// The operation of the calculator
        /// </summary>
        CalculatorOperation Operation { set; }

        /// <summary>
        /// The result of the operation using the two operands
        /// </summary>
        float Result { set; }

        /// <summary>
        /// Called when a key has been pressed
        /// </summary>
        event EventHandler<CalculatorKeyPressedEventArgs> OnKeyPressed;
    }
}