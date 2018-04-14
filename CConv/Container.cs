using System.Collections.Generic;
using System.Linq;

namespace CConv
{
    /// <summary>
    /// Poor man's DI - extremely naive IoC container.
    /// </summary>
    public static class Container
    {
        private static readonly IList<object> Dependencies = new List<object>();

        public static void Register<T>(T implementation) where T : class
        {
            Dependencies.Add(implementation);
        }

        public static T Resolve<T>() where T : class
        {
            return Dependencies.FirstOrDefault(x => x is T) as T;
        }
    }
}
