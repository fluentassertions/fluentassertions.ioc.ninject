using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentAssertions.Ioc.Ninject
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetPublicInterfaces(this Assembly assembly)
        {
            return GetAllInterfaces(assembly).Where(x => x.IsPublic);
        }

        public static IEnumerable<Type> GetAllInterfaces(this Assembly assembly)
        {
            return assembly.GetTypes().Where(x => x.IsInterface);
        }

        public static IEnumerable<Type> InNamespaceOf<T>(this IEnumerable<Type> types)
        {
            return types.Where(x => x.Namespace == typeof(T).Namespace);
        }

        public static IEnumerable<Type> Excluding<T>(this IEnumerable<Type> types)
        {
            return types.Where(x => x.Name != typeof(T).Name);
        }

        /// <summary>
        /// Returns an <see cref="AssemblyAssertions"/> object that can be used to assert the
        /// current <see cref="Assembly"/>.
        /// </summary>
        public static AssemblyAssertions Should(this Assembly assembly)
        {
            return new AssemblyAssertions(assembly);
        }
    }
}
