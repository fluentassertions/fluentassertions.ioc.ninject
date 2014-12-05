using System;
using Ninject;

namespace FluentAssertions.Ioc.Ninject
{
    public static class KernelExtensions
    {
        /// <summary>
        /// Returns an <see cref="KernelAssertions"/> object that can be used to assert the
        /// current <see cref="IKernel"/>.
        /// </summary>
        public static KernelAssertions Should(this IKernel kernel)
        {
            return new KernelAssertions(kernel);
        }
    }
}
