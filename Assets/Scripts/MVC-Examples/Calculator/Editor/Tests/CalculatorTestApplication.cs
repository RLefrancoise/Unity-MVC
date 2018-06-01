namespace Mvc.Examples.Calculator.Tests
{
    public class CalculatorTestApplication : ICalculatorApplication
    {
        private ICalculatorControllerFactory _controllerFactory;

        public CalculatorTestApplication()
        {
            _controllerFactory = new CalculatorControllerFactory();

            ControllerFactory.CreateController<CalculatorController>(new CalculatorControllerFactoryParams {View = new CalculatorTestView()});
        }

        public IControllerFactory ControllerFactory
        {
            get { return _controllerFactory ?? (_controllerFactory = new CalculatorControllerFactory()); }
        }

        public bool DestroyController(IController controller)
        {
            return ControllerFactory.Controllers.Remove(controller);
        }
    }
}
