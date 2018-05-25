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