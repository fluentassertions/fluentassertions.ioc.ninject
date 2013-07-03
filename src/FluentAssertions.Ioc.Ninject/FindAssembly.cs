using System.Reflection;

namespace FluentAssertions.Ioc.Ninject
{
    public static class FindAssembly
    {
        public static Assembly Containing<T>()
        {
            return typeof(T).Assembly;
        }
    }
}
