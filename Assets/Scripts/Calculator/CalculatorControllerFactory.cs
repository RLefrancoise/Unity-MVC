﻿using MVC;

namespace Calculator
{
    /// <inheritdoc />
    /// <summary>
    /// Factory for calculator controllers
    /// </summary>
    public class CalculatorControllerFactory : ICalculatorControllerFactory
    {
        public TController CreateController<TController>(CalculatorControllerFactoryParams parameters) where TController : IController
        {
            return (TController) CreateItem(parameters);
        }

        public IController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController
        {
            return (IController) CreateItem(parameters);
        }

        public object CreateItem(object parameters)
        {
            IController c = new CalculatorController(new CalculatorModel(), ((CalculatorControllerFactoryParams) parameters).View);
            return c;
        }
    }
}