﻿using System.Collections.Generic;
using System.Linq;
using Calculator;
using MVC;

namespace Tests.Calculator
{
    public class CalculatorTestApplication : ICalculatorApplication
    {
        private readonly ICalculatorControllerFactory _controllerFactory;

        public CalculatorTestApplication()
        {
            Controllers = new List<IController>();
            _controllerFactory = new CalculatorControllerTestFactory();

            CreateController<CalculatorController>(new CalculatorControllerFactoryParams {View = new CalculatorTestView()});
        }

        public List<IController> Controllers { get; private set; }

        public TController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController
        {
            Controllers.Add(_controllerFactory.CreateController<TController>(parameters));
            return (TController) Controllers.Last();
        }

        public bool DestroyController(IController controller)
        {
            return Controllers.Remove(controller);
        }
    }
}