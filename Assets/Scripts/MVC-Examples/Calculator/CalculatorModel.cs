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