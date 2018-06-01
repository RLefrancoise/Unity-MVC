namespace Mvc
{
    /// <summary>
    /// Base interface for MVC application
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// Factory for controllers
        /// </summary>
        IControllerFactory ControllerFactory { get; }

        /// <summary>
        /// Destroy a created controller
        /// </summary>
        /// <param name="controller">Controller to destroy</param>
        /// <returns></returns>
        bool DestroyController(IController controller);
    }
}