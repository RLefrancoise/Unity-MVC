using MVC;

namespace Calculator
{
    /// <inheritdoc />
    /// <summary>
    /// Interface for any calculator controller
    /// </summary>
    public interface ICalculatorController : IController<ICalculatorView>
    {
    }
}