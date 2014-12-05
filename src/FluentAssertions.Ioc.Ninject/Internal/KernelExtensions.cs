using System;
using System.Linq;
using Ninject;

namespace FluentAssertions.Ioc.Ninject.Internal
{
    internal static class KernelExtensions
    {
        public static ActivationError GetSingleInstanceActivationError(this IKernel kernel, Type type)
        {
            ActivationError error = null;

            try
            {
                kernel.Get(type);
            }
            catch (ActivationException ex)
            {
                error = new ActivationError { Type = type, Exception = ex };
            }

            return error;
        }

        public static ActivationError GetAllInstancesActivationError(this IKernel kernel, Type type)
        {
            ActivationError error = null;

            try
            {
                var instances = kernel.GetAll(type);
                if (!instances.Any())
                {
                    error = new ActivationError { Type = type };
                }
            }
            catch (ActivationException ex)
            {
                error = new ActivationError { Type = type, Exception = ex };
            }

            return error;
        }
    }
}
