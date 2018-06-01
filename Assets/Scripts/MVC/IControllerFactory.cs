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
        /// <typeparam name="TController"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController;
    }
}
