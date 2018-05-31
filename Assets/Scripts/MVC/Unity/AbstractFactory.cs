using System;
using System.Collections.Generic;

namespace Mvc.Unity
{
    /// <inheritdoc cref="IFactory"/>
    /// <summary>
    /// Base factory for unity factories
    /// </summary>
    public abstract class AbstractFactory : IFactory
    {
        public List<IController> Controllers { get; private set; }

        /// <summary>
        /// Contruct a new factory
        /// </summary>
        protected AbstractFactory()
        {
            Controllers = new List<IController>();
        }

        public TFactoryItem CreateItem<TFactoryItem>(object[] parameters) where TFactoryItem : IFactoryItem
        {
            TFactoryItem item = (TFactoryItem) Activator.CreateInstance(typeof(TFactoryItem), parameters);
            Controllers.Add((IController) item);
            return item;
        }
    }
}
