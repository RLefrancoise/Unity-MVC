using Mvc.Unity;

namespace Mvc.Examples.Calculator
{
    /// <inheritdoc cref="AbstractFactory"/>
    /// <inheritdoc cref="ICalculatorControllerFactory"/>
    /// <summary>
    /// Factory for calculator controllers
    /// </summary>
    public class CalculatorControllerFactory : AbstractFactory, ICalculatorControllerFactory
    {
        public TController CreateController<TController>(CalculatorControllerFactoryParams parameters) where TController : IController
        {
            return CreateItem<TController>(new object[] {new CalculatorModel(), parameters.View});
        }

        public IController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController
        {
            return CreateController<TController>(parameters as CalculatorControllerFactoryParams);
        }
    }
}
