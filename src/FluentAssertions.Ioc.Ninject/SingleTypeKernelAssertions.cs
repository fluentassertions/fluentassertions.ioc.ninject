using System;
using System.Text;
using FluentAssertions.Execution;
using FluentAssertions.Formatting;
using FluentAssertions.Ioc.Ninject.Internal;
using Ninject;

namespace FluentAssertions.Ioc.Ninject
{
    public class SingleTypeKernelAssertions
    {
        private readonly IKernel kernel;
        private readonly Type type;

        protected internal SingleTypeKernelAssertions(IKernel kernel, Type type)
        {
            this.kernel = kernel;
            this.type = type;
        }
        
        /// <summary>
        /// Asserts that the selected type can be resolved with a single instance.
        /// </summary>
        public void WithSingleInstance()
        {
            var error = kernel.GetSingleInstanceActivationError(type);
            
            // Assert
            Execute.Assertion.ForCondition(error == null)
                             .FailWith(BuildFailureMessage(error));
        }

        /// <summary>
        /// Asserts that the selected interface can be resolved with at least one instance.
        /// </summary>
        public void WithAtLeastOneInstance()
        {
            var error = kernel.GetAllInstancesActivationError(type);
            
            // Assert
            Execute.Assertion.ForCondition(error == null)
                             .FailWith(BuildFailureMessage(error));
        }

        /// <summary>
        /// Asserts that the specified interface resolves to a concrete type.
        /// </summary>
        /// <typeparam name="T">The expected concrete type.</typeparam>
        public void To<T>()
        {
            var actual = kernel.Get(type).GetType();
            var expected = typeof (T);

            Execute.Assertion.ForCondition(actual == expected)
                                .FailWith("Expected interface {0} to resolve to type {1}, but found {2} instead", type.Name, expected.Name, actual.Name);
        }

        private string BuildFailureMessage(ActivationError error)
        {
            if (error == null)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.AppendFormat("Unable to resolve type {0}.", error.Type.FullName);

            if (error.Exception != null)
            {
                builder.AppendLine();
                builder.AppendLine();
                builder.Append(Formatter.ToString(error.Exception));    
            }
            
            return builder.ToString();
        }
    }
}
