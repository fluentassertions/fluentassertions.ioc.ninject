using System.Linq;
using System.Reflection;
using FluentAssertions.Execution;

namespace FluentAssertions.Ioc.Ninject
{
    public class AssemblyAssertions
    {
        protected internal AssemblyAssertions(Assembly assembly)
        {
            Subject = assembly;
        }

        public Assembly Subject { get; private set; }

        public void NotReference(Assembly assembly)
        {
            var subjectName = Subject.GetName().Name;
            var assemblyName = assembly.GetName().Name;

            var references = Subject.GetReferencedAssemblies().Select(x => x.Name);

            Execute.Assertion
                .ForCondition(!references.Any(x => x == assemblyName))
                .FailWith("Assembly {0} should not reference assembly {1}", subjectName, assemblyName);
        }
    }
}
