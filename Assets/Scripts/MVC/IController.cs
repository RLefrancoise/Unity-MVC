namespace Mvc
{
    /// <summary>
    /// Base interface for MVC controller
    /// </summary>
    public interface IController
    {
        
    }

    /// <inheritdoc />
    /// <summary>
    /// Base interface for MVC controller with a specified view type
    /// </summary>
    /// <typeparam name="TView">View type to use with this controller.</typeparam>
    public interface IController<out TView> : IController where TView : IView
    {
        /// <summary>
        /// The view used with the controller
        /// </summary>
        TView View { get; }
    }
}