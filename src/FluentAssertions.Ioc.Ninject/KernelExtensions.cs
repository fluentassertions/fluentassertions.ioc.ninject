using Ninject;

namespace FluentAssertions.Ioc.Ninject
{
    public static class KernelExtensions
    {
        public static KernelAssertions Should(this IKernel kernel)
        {
            return new KernelAssertions(kernel);
        }
    }
}
