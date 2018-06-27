using System;
using System.Collections.Generic;

namespace Mvc
{
    /// <summary>
    /// Interface for IControllerFactory params
    /// </summary>
    public interface IControllerFactoryParams
    {
        
    }

    /// <inheritdoc cref="IFactory"/>
    /// <summary>
    /// Interface for controller factory
    /// </summary>
    public interface IControllerFactory : IFactory
    {
        /// <summary>
        /// Controllers created by the factory
        /// </summary>
        List<IController> Controllers { get; }

        /// <summary>
        /// Create a controller from the specified parameters
        /// </summary>
        /// <param name="controllerType">Type of the controller</param>
        /// <param name="paramters">Parameters to create the controller</param>
        /// <returns></returns>
        IController CreateController(Type controllerType, IControllerFactoryParams paramters);

        /// <summary>
        /// Create a controller from the specified parameters
        /// </summary>
        /// <typeparam name="TController">Type of the controller</typeparam>
        /// <param name="parameters">Parameters to create the controller</param>
        /// <returns></returns>
        IController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController;
    }
}
