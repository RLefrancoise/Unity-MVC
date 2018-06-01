using UnityEngine;

namespace Mvc.Unity
{
    /// <inheritdoc cref="IApplication"/>
    /// <summary>
    /// Base class for any Unity Mvc application
    /// </summary>
    public abstract class UnityMvcApplication : MonoBehaviour, IApplication
    {
        public abstract IControllerFactory ControllerFactory { get; }

        public bool DestroyController(IController controller)
        {
            return ControllerFactory.Controllers.Remove(controller);   
        }
    }
}
