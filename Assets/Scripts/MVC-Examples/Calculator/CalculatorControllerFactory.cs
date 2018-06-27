using System;
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
        protected override IController CreateController(Type controllerType, CalculatorControllerFactoryParams parameters)
        {
            return (IController) CreateItem(controllerType, new object[] {new CalculatorModel(), parameters.View});
        }

        protected override TController CreateController<TController>(CalculatorControllerFactoryParams parameters)
        {
            return CreateItem<TController>(new object[] {new CalculatorModel(), parameters.View});
        }
    }
}
