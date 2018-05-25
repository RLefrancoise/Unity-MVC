using System;
using System.Reflection;

namespace Mvc.Unity
{
    public abstract class AbstractFactory : IFactory
    {
        public TFactoryItem CreateItem<TFactoryItem>(object[] parameters) where TFactoryItem : IFactoryItem
        {
            TFactoryItem item = (TFactoryItem) Activator.CreateInstance(typeof(TFactoryItem), parameters);
            return item;
        }
    }
}
