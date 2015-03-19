using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentAssertions.Ioc.Ninject
{
    public static class TypeExtensions
    {
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
    }
}
