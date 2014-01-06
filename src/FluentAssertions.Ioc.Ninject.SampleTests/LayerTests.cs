using FluentAssertions.Ioc.Samples.Core;
using FluentAssertions.Ioc.Samples.Data;
using FluentAssertions.Ioc.Samples.Entities;
using NUnit.Framework;

namespace FluentAssertions.Ioc.Ninject.SampleTests
{
    [TestFixture]
    public class LayerTests
    {
        [Test]
        public void Core_should_not_reference_data()
        {
            // Arrange
            var core = FindAssembly.Containing<SampleService>();
            var data = FindAssembly.Containing<SampleRepository>();

            // Assert
            core.Should().NotReference(data);
        }

        [Test]
        public void Core_should_reference_entities()
        {
            // Arrange
            var core = FindAssembly.Containing<SampleService>();
            var entities = FindAssembly.Containing<Entity>();

            // Assert
            core.Should().Reference(entities);
        }
    }
}
