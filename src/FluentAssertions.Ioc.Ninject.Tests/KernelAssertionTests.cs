using System;
using FluentAssertions.Ioc.Ninject.Sample;
using FluentAssertions.Ioc.Samples.Interfaces;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;

namespace FluentAssertions.Ioc.Ninject.Tests
{
    [TestFixture]
    public class KernelAssertionTests
    {
        [Test]
        public void Should_succeed_when_asserting_interfaces_can_be_resolved_with_a_single_instance()
        {
            // Arrange
            var kernel = GetKernel();
            var interfaces = FindAssembly.Containing<ISampleService>().GetPublicInterfaces()
                                                                      .EndingWith("Service");

            // Act
            Action act = () => kernel.Should().Resolve(interfaces).WithSingleInstance();

            // Assert
            act.ShouldNotThrow<AssertionException>();
        }

        [Test]
        public void Should_fail_when_asserting_interfaces_can_be_resolved_with_a_single_instance()
        {
            // Arrange
            var kernel = GetKernel();
            var interfaces = FindAssembly.Containing<ISomeFactory>().GetPublicInterfaces()
                                                                    .EndingWith("Factory");

            // Act
            Action act = () => kernel.Should().Resolve(interfaces).WithSingleInstance();

            // Assert
            act.ShouldThrow<AssertionException>();
        }

        [Test]
        public void Should_succeed_when_asserting_an_interface_can_be_resolved_with_a_single_instance()
        {
            // Arrange
            var kernel = GetKernel();
            
            // Act
            Action act = () => kernel.Should().Resolve<ISampleService>().WithSingleInstance();

            // Assert
            act.ShouldNotThrow<AssertionException>();
        }

        [Test]
        public void Should_fail_when_asserting_an_interface_can_be_resolved_with_a_single_instance()
        {
            // Arrange
            var kernel = GetKernel();
           
            // Act
            Action act = () => kernel.Should().Resolve<ISomeFactory>().WithSingleInstance();

            // Assert
            act.ShouldThrow<AssertionException>();
        }

        [Test]
        public void Should_succeed_when_asserting_interfaces_can_be_resolved_with_at_least_instance()
        {
            // Arrange
            var kernel = GetKernel();
            var interfaces = FindAssembly.Containing<ISomeProvider>().GetPublicInterfaces()
                                                                     .EndingWith("Provider");

            // Act
            Action act = () => kernel.Should().Resolve(interfaces).WithAtLeastOneInstance();

            // Assert
            act.ShouldNotThrow<AssertionException>();
        }

        [Test]
        public void Should_fail_when_asserting_interfaces_can_be_resolved_with_at_least_instance()
        {
            // Arrange
            var kernel = GetKernel();
            var interfaces = FindAssembly.Containing<ISomeFactory>().GetPublicInterfaces()
                                                                    .EndingWith("Factory");

            // Act
            Action act = () => kernel.Should().Resolve(interfaces).WithAtLeastOneInstance();

            // Assert
            act.ShouldThrow<AssertionException>();
        }

        [Test]
        public void Should_succeed_when_asserting_interfaces_can_be_resolved_when_implementation_has_dependencies()
        {
            // Arrange
            var kernel = GetKernel();
            var interfaces = new [] { typeof (IFooService) };

            // Act
            Action act = () => kernel.Should().Resolve(interfaces).WithSingleInstance();

            // Assert
            act.ShouldNotThrow<AssertionException>();
        }

        [Test]
        public void Should_fail_when_asserting_interfaces_can_be_resolved_when_implementation_has_dependencies()
        {
            // Arrange
            var kernel = GetKernel();
            var interfaces = new[] { typeof(IFooService) };

            kernel.Unbind<ISampleRepository>();

            // Act
            Action act = () => kernel.Should().Resolve(interfaces).WithSingleInstance();

            // Assert
            act.ShouldThrow<AssertionException>();
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
