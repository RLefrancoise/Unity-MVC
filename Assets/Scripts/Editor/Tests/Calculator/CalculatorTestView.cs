using System;
using Calculator;

namespace Tests.Calculator
{
    public class CalculatorTestView : ICalculatorView
    {
        public int FirstOperand { get; set; }
        public int SecondOperand { get; set; }
        public CalculatorOperation Operation { get; set; }
        public float Result { get; set; }

        public event EventHandler<CalculatorKeyPressedEventArgs> OnKeyPressed = (sender, e) => { };

        public void SimulateKeyPress(int key)
        {
            SimulateKeyPress(key.ToString());
        }

        public void SimulateKeyPress(string key)
        {
            OnKeyPressed.Invoke(this, new CalculatorKeyPressedEventArgs(key));
        }

        public void RequestResult()
        {
            SimulateKeyPress("=");
        }

        public void RequestClear()
        {
            SimulateKeyPress("CE");
        }
    }
}
