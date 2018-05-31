using MVC.Unity;

namespace Mvc.Examples.Calculator
{
    /// <inheritdoc cref="AbstractControllerFactory{CalculatorControllerFactoryParams}"/>
    /// <inheritdoc cref="ICalculatorControllerFactory"/>
    /// <summary>
    /// Factory for calculator controllers
    /// </summary>
    public class CalculatorControllerFactory : AbstractControllerFactory<CalculatorControllerFactoryParams>, ICalculatorControllerFactory
    {
        protected override TController CreateController<TController>(CalculatorControllerFactoryParams parameters)
        {
            return CreateItem<TController>(new object[] {new CalculatorModel(), parameters.View});
        }
    }
}
