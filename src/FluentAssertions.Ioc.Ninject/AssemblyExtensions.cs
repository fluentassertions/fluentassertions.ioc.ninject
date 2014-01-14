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

        /// <summary>
        /// Filters for just the types in the specified namespace.
        /// </summary>
        /// <typeparam name="T">The namespace of this type.</typeparam>
        /// <param name="types">The types to filter.</param>
        public static IEnumerable<Type> InNamespaceOf<T>(this IEnumerable<Type> types)
        {
            return types.Where(x => x.Namespace == typeof(T).Namespace);
        }

        /// <summary>
        /// Filters to exclude the specified type.
        /// </summary>
        /// <typeparam name="T">The type to exclude.</typeparam>
        /// <param name="types">The types to filter.</param>
        public static IEnumerable<Type> Excluding<T>(this IEnumerable<Type> types)
        {
            return types.Where(x => x.Name != typeof(T).Name);
        }

        /// <summary>
        /// Filters to only include types where the type name ends with the specified string.
        /// </summary>
        /// <param name="types">The types to filter.</param>
        /// <param name="endingWith">The ending with string.</param>
        public static IEnumerable<Type> EndingWith(this IEnumerable<Type> types, string endingWith)
        {
            return types.Where(x => x.Name.EndsWith(endingWith));
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
