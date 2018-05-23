namespace MVC
{
    /// <summary>
    /// Interface for IControllerFactory params
    /// </summary>
    public interface IControllerFactoryParams
    {
        
    }

    public interface IControllerFactory : IFactory
    {
        IController CreateController<TController>(IControllerFactoryParams parameters) where TController : IController;
    }

    /// <summary>
    /// Interface for controller factory
    /// </summary>
    public interface IControllerFactory<in TControllerFactoryParams> : IControllerFactory where TControllerFactoryParams : IControllerFactoryParams
    {
        /// <summary>
        /// Create a controller according to given parameters
        /// </summary>
        /// <typeparam name="TController">Type of the controller</typeparam>
        /// <param name="parameters">Parameters to use to create the controller</param>
        /// <returns></returns>
        TController CreateController<TController>(TControllerFactoryParams parameters)
            where TController : IController;
    }
}
