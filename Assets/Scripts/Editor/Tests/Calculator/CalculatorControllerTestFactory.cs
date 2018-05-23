using Calculator;
using MVC;

namespace Tests.Calculator
{
    public class CalculatorControllerTestFactory : ICalculatorControllerFactory
    {
        public TController CreateController<TController>(CalculatorControllerFactoryParams parameters) where TController : IController
        {
            return (TController) CreateItem(parameters);
        }

        public IController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController
        {
            return (TController)CreateItem(parameters);
        }

        public object CreateItem(object parameters)
        {
            IController c = new CalculatorController(new CalculatorModel(), ((CalculatorControllerFactoryParams) parameters).View);
            return c;
        }
    }
}
