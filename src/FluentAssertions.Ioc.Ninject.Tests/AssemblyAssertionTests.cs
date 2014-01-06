using System;
using FluentAssertions.Ioc.Samples.Core;
using FluentAssertions.Ioc.Samples.Data;
using FluentAssertions.Ioc.Samples.Entities;
using NUnit.Framework;

namespace FluentAssertions.Ioc.Ninject.Tests
{
    [TestFixture]
    public class AssemblyAssertionTests
    {
        [Test]
        public void Should_succeed_when_asserting_an_assembly_is_not_referenced()
        {
            // Arrange
            var coreAssembly = FindAssembly.Containing<SampleService>();
            var dataAssembly = FindAssembly.Containing<SampleRepository>();

            // Act
            Action act = () => coreAssembly.Should().NotReference(dataAssembly);

            // Assert
            act.ShouldNotThrow();
        }

        [Test]
        public void Should_fail_when_asserting_an_assembly_is_not_referenced()
        {
            // Arrange
            var dataAssembly = FindAssembly.Containing<SampleRepository>();
            var coreAssembly = FindAssembly.Containing<SampleService>();
            
            // Act
            Action act = () => dataAssembly.Should().NotReference(coreAssembly);

            // Assert
            act.ShouldThrow<AssertionException>();
        }

        [Test]
        public void Should_succeed_when_asserting_an_assembly_is_referenced()
        {
            // Arrange
            var coreAssembly = FindAssembly.Containing<SampleService>();
            var entityAssembly = FindAssembly.Containing<Entity>();

            // Act
            Action act = () => coreAssembly.Should().Reference(entityAssembly);

            // Assert
            act.ShouldNotThrow();
        }

        [Test]
        public void Should_fail_when_asserting_an_assembly_is_referenced()
        {
            // Arrange
            var dataAssembly = FindAssembly.Containing<SampleRepository>();
            var entityAssembly = FindAssembly.Containing<Entity>();

            // Act
            Action act = () => dataAssembly.Should().Reference(entityAssembly);

            // Assert
            act.ShouldThrow<AssertionException>();
        }
    }
}
