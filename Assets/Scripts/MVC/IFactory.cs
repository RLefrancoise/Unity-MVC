using System;

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
        /// <param name="item">Type of the item to create</param>
        /// <param name="paramters">Parameters to create the item</param>
        /// <returns></returns>
        IFactoryItem CreateItem(Type item, object[] paramters);

        /// <summary>
        /// Create an item from the factory
        /// </summary>
        /// <param name="parameters">Parameters to create the item</param>
        /// <returns>The created item</returns>
        TFactoryItem CreateItem<TFactoryItem>(object[] parameters) where TFactoryItem : IFactoryItem;
    }
}
