namespace Mvc
{
    /// <summary>
    /// Base interface for any factory
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Create an item from the factory
        /// </summary>
        /// <param name="parameters">Parameters to create the item</param>
        /// <returns>The created item</returns>
        object CreateItem(object parameters);
    }
}
