using UnityEngine;
using Mvc.Unity;

namespace Mvc.Examples.Calculator
{
    /// <inheritdoc cref="UnityMvcApplication"/>
    /// <summary>
    /// The calculator application
    /// </summary>
    public class CalculatorApplication : UnityMvcApplication, ICalculatorApplication
    {
        /// <summary>
        /// The GameObject of the view
        /// </summary>
        [SerializeField]
        private GameObject calculatorViewEntity;

        private IControllerFactory _controllerFactory;

        /// <summary>
        /// The view of the calculator entity
        /// </summary>
        public ICalculatorView EntityView
        {
            get { return calculatorViewEntity.GetComponent<ICalculatorView>(); }
        }

        public override IControllerFactory ControllerFactory
        {
            get { return _controllerFactory ?? (_controllerFactory = new CalculatorControllerFactory()); }
        }

        protected void Awake()
        {
            ControllerFactory.CreateController<CalculatorController>(new CalculatorControllerFactoryParams {View = EntityView});
        }
    }
}