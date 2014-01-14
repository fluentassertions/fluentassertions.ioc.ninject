using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions.Execution;
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
        /// Gets the interfaces which trying to be resolved.
        /// </summary>
        private IEnumerable<Type> Interfaces { get; set; }

        /// <summary>
        /// Specifies the interfaces to try resolving from the kernel.
        /// </summary>
        /// <param name="interfaces">The interfaces to resolve.</param>
        public KernelAssertions ResolveInterfaces(IEnumerable<Type> interfaces)
        {
            Interfaces = interfaces;
            return this;
        }

        /// <summary>
        /// Specifies the interface to try resolving from the kernel.
        /// </summary>
        /// <typeparam name="TInterface">The interface to resolve.</typeparam>
        /// <returns></returns>
        public KernelAssertions ResolveInterface<TInterface>()
        {
            Interfaces = new List<Type>() { typeof(TInterface) };
            return this;
        }

        /// <summary>
        /// Asserts that the selected interface(s) can be resolved with a single instance.
        /// </summary>
        public void WithSingleInstance()
        {
            var failed = new List<ActivationError>();

            // Try to activate each interface in the list
            foreach (var type in Interfaces)
            {
                try
                {
                    if (type.IsGenericTypeDefinition)
                        // TODO: We need to deal with generic interfaces.
                        continue;
                    else
                        Subject.Get(type);
                }
                catch (ActivationException ex)
                {
                    failed.Add(new ActivationError() { Type = type, Exception = ex });
                }
            }

            // Assert
            Execute.Assertion.ForCondition(failed.Count() == 0)
                             .FailWith(BuildFailureMessage(failed));
        }

        /// <summary>
        /// Asserts that the selected interface(s) can be resolved with at least one instance.
        /// </summary>
        public void WithAtLeastOneInstance()
        {
            var failed = new List<ActivationError>();

            // Try to activate each interface in the list
            foreach (var type in Interfaces)
            {
                try
                {
                    if (type.IsGenericTypeDefinition)
                        // TODO: We need to deal with generic interfaces.
                        continue;
                    else
                    {
                        var instances = Subject.GetAll(type);
                        if (!instances.Any())
                            failed.Add(new ActivationError() { Type = type });
                    }
                    
                }
                catch (ActivationException ex)
                {
                    failed.Add(new ActivationError() { Type = type, Exception = ex });
                }
            }

            // Assert
            Execute.Assertion.ForCondition(failed.Count() == 0)
                             .FailWith(BuildFailureMessage(failed));
        }

        private string BuildFailureMessage(List<ActivationError> failed)
        {
            // Build the failure message
            var builder = new StringBuilder();
            builder.AppendLine("The following interfaces could not be resolved:").AppendLine();
            
            foreach (var error in failed)
                builder.AppendFormat(" - {0}", error.Type.FullName).AppendLine();
            
            return builder.ToString();
        }

        class ActivationError
        {
            public Type Type { get; set; }
            public ActivationException Exception { get; set; }
        }
    }
}
