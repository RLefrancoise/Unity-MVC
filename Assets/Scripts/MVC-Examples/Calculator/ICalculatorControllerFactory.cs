namespace Mvc.Examples.Calculator
{
    /// <inheritdoc cref="IControllerFactoryParams"/>
    /// <summary>
    /// Params of Calculator controller factory
    /// </summary>
    public class CalculatorControllerFactoryParams : IControllerFactoryParams
    {
        /// <summary>
        /// The view to use with the controller
        /// </summary>
        public ICalculatorView View { get; set; }
    }

    /// <inheritdoc />
    /// <summary>
    /// Interface to implement a factory for a calculator controllers
    /// </summary>
    public interface ICalculatorControllerFactory : IControllerFactory<CalculatorControllerFactoryParams>
    {
    }
}
