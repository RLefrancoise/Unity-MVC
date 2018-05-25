using System.Collections.Generic;

namespace Mvc
{
    /// <summary>
    /// Base interface for MVC application
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// List of controllers created for the application
        /// </summary>
        List<IController> Controllers { get; }

        /*
        /// <summary>
        /// Factory for controllers
        /// </summary>
        TControllerFactory ControllerFactory { get; }*/

        /// <summary>
        /// Create a new controller
        /// </summary>
        /// <typeparam name="TController">The type of the controller to create</typeparam>
        /// <param name="parameters">Parameters to create the controller</param>
        /// <returns>The newly created controller</returns>
        TController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController;

        /// <summary>
        /// Destroy a created controller
        /// </summary>
        /// <param name="controller">Controller to destroy</param>
        /// <returns></returns>
        bool DestroyController(IController controller);
    }
}