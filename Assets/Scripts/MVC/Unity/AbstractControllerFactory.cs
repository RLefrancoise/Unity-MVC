using Mvc;
using Mvc.Unity;

namespace MVC.Unity
{
    /// <inheritdoc cref="AbstractFactory"/>
    /// <inheritdoc cref="IControllerFactory"/>
    /// <summary>
    /// Abstract controller factory for unity controllers
    /// </summary>
    /// <typeparam name="TControllerFactoryParams">Type of the controller factory params</typeparam>
    public abstract class AbstractControllerFactory<TControllerFactoryParams> : AbstractFactory, IControllerFactory where TControllerFactoryParams : IControllerFactoryParams
    {
        /// <summary>
        /// Create a controller from the parameters specified through the template parameter
        /// </summary>
        /// <typeparam name="TController">The type of the controller to create</typeparam>
        /// <param name="parameters">Parameters to create the controller</param>
        /// <returns></returns>
        protected abstract TController CreateController<TController>(TControllerFactoryParams parameters) where TController : IController;

        public IController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController
        {
            return CreateController<TController>((TControllerFactoryParams) parameters);
        }
    }
}
