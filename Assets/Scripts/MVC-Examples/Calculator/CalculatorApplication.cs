using UnityEngine;
using Mvc.Unity;

namespace Mvc.Examples.Calculator
{
    /// <inheritdoc cref="UnityMvcApplication{ICalculatorControllerFactory}"/>
    /// <summary>
    /// The calculator application
    /// </summary>
    public class CalculatorApplication : UnityMvcApplication<ICalculatorControllerFactory>, ICalculatorApplication
    {
        /// <summary>
        /// The GameObject of the view
        /// </summary>
        [SerializeField]
        private GameObject calculatorViewEntity;
        
        /// <summary>
        /// The view of the calculator entity
        /// </summary>
        public ICalculatorView EntityView
        {
            get { return calculatorViewEntity.GetComponent<ICalculatorView>(); }
        }

        protected override void Awake()
        {
            base.Awake();
            CreateController<CalculatorController>(new CalculatorControllerFactoryParams {View = EntityView});
        }

        protected override ICalculatorControllerFactory InitializeControllerFactory()
        {
            return new CalculatorControllerFactory();
        }
    }
}