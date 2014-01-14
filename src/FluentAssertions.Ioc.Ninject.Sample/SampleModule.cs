using FluentAssertions.Ioc.Samples.Core;
using FluentAssertions.Ioc.Samples.Core.Providers;
using FluentAssertions.Ioc.Samples.Data;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace FluentAssertions.Ioc.Ninject.Sample
{
    public class SampleModule : NinjectModule
    {
        public override void Load()
        {
            // Services
            Kernel.Bind(x => x.FromAssemblyContaining<SampleService>().SelectAllClasses()
                                                                      .InNamespaceOf<SampleService>()
                                                                      .EndingWith("Service")
                                                                      .BindAllInterfaces());

            // Providers
            Kernel.Bind(x => x.FromAssemblyContaining<FooProvider>().SelectAllClasses()
                                                                    .InNamespaceOf<FooProvider>()
                                                                    .EndingWith("Provider")
                                                                    .BindAllInterfaces());

            // Repositories
            Kernel.Bind(x => x.FromAssemblyContaining<SampleRepository>().SelectAllClasses()
                                                                         .InNamespaceOf<SampleRepository>()
                                                                         .EndingWith("Repository")
                                                                         .BindAllInterfaces());
        }
    }
}
