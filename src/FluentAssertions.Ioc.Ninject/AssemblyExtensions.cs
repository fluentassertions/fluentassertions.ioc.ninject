using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentAssertions.Ioc.Ninject
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets all the public interfaces in an assembly.
        /// </summary>
        /// <param name="assembly">The assembly to search</param>
        public static IEnumerable<Type> GetPublicInterfaces(this Assembly assembly)
        {
            return GetAllInterfaces(assembly).Where(x => x.IsPublic);
        }

        /// <summary>
        /// Gets all the interfaces in an assembly.
        /// </summary>
        /// <param name="assembly">The assembly to search</param>
        public static IEnumerable<Type> GetAllInterfaces(this Assembly assembly)
        {
            return assembly.GetTypes().Where(x => x.IsInterface);
        }
    }
}
