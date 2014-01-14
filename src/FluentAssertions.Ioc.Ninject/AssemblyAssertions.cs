using System;
using System.Linq;
using System.Reflection;
using FluentAssertions.Execution;

namespace FluentAssertions.Ioc.Ninject
{
    /// <summary>
    /// Contains a number of methods to assert that a <see cref="Assembly"/> is in the expected state.
    /// </summary>
    public class AssemblyAssertions
    {
        protected internal AssemblyAssertions(Assembly assembly)
        {
            Subject = assembly;
        }

        /// <summary>
        /// Gets the assembly which is being asserted.
        /// </summary>
        public Assembly Subject { get; private set; }

        /// <summary>
        /// Asserts that an assembly does not reference the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly which should not be referenced.</param>
        public void NotReference(Assembly assembly)
        {
            NotReference(assembly, String.Empty);
        }

        /// <summary>
        /// Asserts that an assembly does not reference the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly which should not be referenced.</param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public void NotReference(Assembly assembly, string reason, params string[] reasonArgs)
        {
            var subjectName = Subject.GetName().Name;
            var assemblyName = assembly.GetName().Name;

            var references = Subject.GetReferencedAssemblies().Select(x => x.Name);

            Execute.Assertion
                   .BecauseOf(reason, reasonArgs)
                   .ForCondition(!references.Any(x => x == assemblyName))
                   .FailWith("Assembly {0} should not reference assembly {1}{reason}", subjectName, assemblyName);
        }

        /// <summary>
        /// Asserts that an assembly references the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly which should be referenced.</param>
        public void Reference(Assembly assembly)
        {
           Reference(assembly, String.Empty); 
        }

        /// <summary>
        /// Asserts that an assembly references the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly which should be referenced.</param>
        /// <param name="reason">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        /// Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public void Reference(Assembly assembly, string reason, params string[] reasonArgs)
        {
            var subjectName = Subject.GetName().Name;
            var assemblyName = assembly.GetName().Name;

            var references = Subject.GetReferencedAssemblies().Select(x => x.Name);

            Execute.Assertion
                   .BecauseOf(reason, reasonArgs)
                   .ForCondition(references.Any(x => x == assemblyName))
                   .FailWith("Assembly {0} should reference assembly {1}{reason}, but it does not", subjectName, assemblyName);
        }
    }
}
