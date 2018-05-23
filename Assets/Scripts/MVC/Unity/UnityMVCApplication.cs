using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MVC.Unity
{
    /// <inheritdoc cref="IApplication"/>
    /// <summary>
    /// Base class for any Unity Mvc application
    /// </summary>
    /// <typeparam name="TControllerFactory"></typeparam>
    public abstract class UnityMvcApplication<TControllerFactory> : MonoBehaviour, IApplication where TControllerFactory : IControllerFactory
    {
        public List<IController> Controllers { get; private set; }

        /// <summary>
        /// Factory to create controllers
        /// </summary>
        protected TControllerFactory ControllerFactory { get; private set; }

        protected virtual void Awake()
        {
            Controllers = new List<IController>();
            ControllerFactory = InitializeControllerFactory();
        }

        /// <summary>
        /// Initializes the controller factory
        /// </summary>
        /// <returns></returns>
        protected abstract TControllerFactory InitializeControllerFactory();

        public TController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController
        {
            Controllers.Add(ControllerFactory.CreateController<TController>(parameters));
            return (TController) Controllers.Last();
        }

        public bool DestroyController(IController controller)
        {
            return Controllers.Remove(controller);   
        }
    }
}
