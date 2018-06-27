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
        //public List<IController> Controllers { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// Contruct a new factory
        /// </summary>

        public IFactoryItem CreateItem(Type type, object[] parameters)
        {
            IFactoryItem item = (IFactoryItem) Activator.CreateInstance(type, parameters);
            return item;
            //Controllers.Add(item);
        }

        public TFactoryItem CreateItem<TFactoryItem>(object[] parameters) where TFactoryItem : IFactoryItem
        {
            TFactoryItem item = (TFactoryItem) Activator.CreateInstance(typeof(TFactoryItem), parameters);
            //Controllers.Add((IController) item);
            return item;
        }
    }
}
