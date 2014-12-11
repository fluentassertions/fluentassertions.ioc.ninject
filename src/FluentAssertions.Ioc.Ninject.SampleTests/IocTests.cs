using FluentAssertions.Ioc.Ninject.Sample;
using FluentAssertions.Ioc.Samples.Core;
using FluentAssertions.Ioc.Samples.Interfaces;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;

namespace FluentAssertions.Ioc.Ninject.SampleTests
{
    [TestFixture]
    public class IocTests
    {
        [Test]
        public void Services_can_be_resolved_with_a_single_instance()
        {
            // Arrange
            var kernel = GetKernel();
            var interfaces = FindAssembly.Containing<ISampleService>().GetPublicInterfaces()
                                                                      .EndingWith("Service");

            // Assert
            kernel.Should().Resolve(interfaces).WithSingleInstance();
        }

        [Test]
        public void Providers_can_be_resolved_with_at_least_one_instance()
        {
            // Arrange
            var kernel = GetKernel();
            var interfaces = FindAssembly.Containing<ISomeProvider>().GetPublicInterfaces()
                                                                     .EndingWith("Provider");

            // Assert
            kernel.Should().Resolve(interfaces).WithAtLeastOneInstance();
        }

        [Test]
        public void SampleService_can_be_resolved_with_a_single_instance()
        {
            // Arrange
            var kernel = GetKernel();
          
            // Assert
            kernel.Should().Resolve<ISampleService>().WithSingleInstance();
        }

        [Test]
        public void Provider_can_be_resolved_with_at_least_one_instance()
        {
            // Arrange
            var kernel = GetKernel();
            
            // Assert
            kernel.Should().Resolve<ISomeProvider>().WithAtLeastOneInstance();
        }

        [Test]
        public void SampleService_resolves_to_expected_implementation()
        {
            // Arrange
            var kernel = GetKernel();
            
            // Assert
            kernel.Should().Resolve<ISampleService>().To<SampleService>();
        }
        
        private IKernel GetKernel()
        {
            var modules = new INinjectModule[]
            {
                new SampleModule()
            };

            return new StandardKernel(modules);
        }
                
    }
}
