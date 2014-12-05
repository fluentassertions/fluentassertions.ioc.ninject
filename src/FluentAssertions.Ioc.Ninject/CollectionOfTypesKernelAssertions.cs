using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions.Execution;
using FluentAssertions.Ioc.Ninject.Internal;
using Ninject;

namespace FluentAssertions.Ioc.Ninject
{
    public class CollectionOfTypesKernelAssertions
    {
        private readonly IKernel kernel;
        private readonly IEnumerable<Type> types;

        protected internal CollectionOfTypesKernelAssertions(IKernel kernel, IEnumerable<Type> types)
        {
            this.kernel = kernel;
            this.types = types;
        }
        
        /// <summary>
        /// Asserts that the selected types can be resolved with a single instance.
        /// </summary>
        public void WithSingleInstance()
        {
            var failed = new List<ActivationError>();

            // Try to activate each type in the list
            foreach (var type in types)
            {
                var activationError = kernel.GetSingleInstanceActivationError(type);

                if (activationError != null)
                {
                    failed.Add(activationError);
                }
            }

            // Assert
            Execute.Assertion.ForCondition(!failed.Any())
                             .FailWith(BuildFailureMessage(failed));
        }

        /// <summary>
        /// Asserts that the selected interfaces can be resolved with at least one instance.
        /// </summary>
        public void WithAtLeastOneInstance()
        {
            var failed = new List<ActivationError>();

            // Try to activate each interface in the list
            foreach (var type in types)
            {
                var error = kernel.GetAllInstancesActivationError(type);

                if (error != null)
                {
                    failed.Add(error);    
                }
                
            }

            // Assert
            Execute.Assertion.ForCondition(!failed.Any())
                             .FailWith(BuildFailureMessage(failed));
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
    }
}
