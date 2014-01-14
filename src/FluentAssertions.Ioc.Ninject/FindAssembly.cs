using System.Reflection;

namespace FluentAssertions.Ioc.Ninject
{
    public static class FindAssembly
    {
        /// <summary>
        /// Finds the assembly containing the specified type.
        /// </summary>
        /// <typeparam name="T">The type to find.</typeparam>
        /// <returns>The assembly.</returns>
        public static Assembly Containing<T>()
        {
            return typeof(T).Assembly;
        }
    }
}
