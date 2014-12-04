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
        /// Gets the types to be resolved.
        /// </summary>
        private IEnumerable<Type> Types { get; set; }

        /// <summary>
        /// Specifies the types to try resolving from the kernel.
        /// </summary>
        /// <param name="types">The types to resolve.</param>
        public KernelAssertions Resolve(IEnumerable<Type> types)
        {
            Types = types;
            return this;
        }

        /// <summary>
        /// Specifies the type to try resolving from the kernel.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns></returns>
        public KernelAssertions Resolve<T>()
        {
            Types = new List<Type>() { typeof(T) };
            return this;
        }

        /// <summary>
        /// Asserts that the selected type(s) can be resolved with a single instance.
        /// </summary>
        public void WithSingleInstance()
        {
            var failed = new List<ActivationError>();

            // Try to activate each type in the list
            foreach (var type in Types)
            {
                try
                {
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
            foreach (var type in Types)
            {
                try
                {
                    var instances = Subject.GetAll(type);
                    if (!instances.Any())
                    {
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

        /// <summary>
        /// Asserts that the specified interface resolves to a concrete type.
        /// </summary>
        /// <typeparam name="T">The expected concrete type.</typeparam>
        public void To<T>()
        {
            foreach (var type in Types)
            {
                var actual = Subject.Get(type).GetType();
                var expected = typeof (T);

                Execute.Assertion.ForCondition(actual == expected)
                                 .FailWith("Expected interface {0} to resolve to type {1}, but found {2} instead", type.Name, expected.Name, actual.Name);
                
            }
        }

        private string BuildFailureMessage(List<ActivationError> failed)
        {
            // Build the failure message
            var builder = new StringBuilder();
            builder.AppendLine("The following types could not be resolved:").AppendLine();
            
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
