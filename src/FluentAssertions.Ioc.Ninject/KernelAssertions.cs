using System;
using System.Collections.Generic;
using Ninject;

namespace FluentAssertions.Ioc.Ninject
{
    /// <summary>
    /// Contains a number of methods to assert that a <see cref="IKernel"/> is in the expected state.
    /// </summary>
    public class KernelAssertions
    {
        protected internal KernelAssertions(IKernel kernel)
        {
            Subject = kernel;
        }

        /// <summary>
        /// Gets the kernel which is being asserted.
        /// </summary>
        public IKernel Subject { get; private set; }
        
        /// <summary>
        /// Specifies the types to try resolving from the kernel.
        /// </summary>
        /// <param name="types">The types to resolve.</param>
        public CollectionOfTypesKernelAssertions Resolve(IEnumerable<Type> types)
        {
            return new CollectionOfTypesKernelAssertions(Subject, types);
        }

        /// <summary>
        /// Specifies the type to try resolving from the kernel.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns></returns>
        public SingleTypeKernelAssertions Resolve<T>()
        {
            return new SingleTypeKernelAssertions(Subject, typeof (T));
        }
    }
}
