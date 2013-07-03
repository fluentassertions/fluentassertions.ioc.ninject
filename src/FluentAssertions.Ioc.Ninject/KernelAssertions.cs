using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions.Execution;
using Ninject;

namespace FluentAssertions.Ioc.Ninject
{
    public class KernelAssertions
    {
        protected internal KernelAssertions(IKernel kernel)
        {
            Subject = kernel;
        }

        public IKernel Subject { get; private set; }
        private IEnumerable<Type> Interfaces { get; set; }

        public KernelAssertions ResolveInterfaces(IEnumerable<Type> interfaces)
        {
            Interfaces = interfaces;
            return this;
        }

        public KernelAssertions ResolveInterface<TInferface>()
        {
            Interfaces = new List<Type>() { typeof(TInferface) };
            return this;
        }

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
            Execute.Verification.ForCondition(failed.Count() == 0)
                                .FailWith(BuildFailureMessage(failed));
        }

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
            Execute.Verification.ForCondition(failed.Count() == 0)
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
