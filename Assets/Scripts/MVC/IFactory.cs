namespace Mvc
{
    /// <summary>
    /// Base interface for any object that can be created by a factory
    /// </summary>
    public interface IFactoryItem
    {
        
    }

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
        TFactoryItem CreateItem<TFactoryItem>(object[] parameters) where TFactoryItem : IFactoryItem;
    }
}
