using System;
using System.Collections.Generic;

namespace SimpleDependencyProvider
{
    public static class DependencyProvider
    {
        private const string dependencyNotProvidedExceptionString = "the Dependencyprovider does not have a method of providing an instance of {0}";

        static Dictionary<Type, Func<object>> dependencies = new();

        public static void newProvide<T>(Func<T> providingFunction) where T : class
        {
            dependencies.Add(typeof(T), providingFunction);
        }
        public static T Get<T>()
        {
            if (dependencies.ContainsKey(typeof(T)))
            {
                return (T)dependencies[typeof(T)].Invoke();
            }
            else
            {
                throw new KeyNotFoundException(string.Format(dependencyNotProvidedExceptionString, typeof(T).Name));
            }
        }
    }
}
