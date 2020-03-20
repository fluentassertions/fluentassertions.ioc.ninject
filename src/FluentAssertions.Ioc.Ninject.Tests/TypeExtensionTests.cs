using NUnit.Framework;

namespace FluentAssertions.Ioc.Ninject.Tests
{
    [TestFixture]
    public class TypeExtensionTests
    {
        [Test]
        public void ThatDerivesFrom_returns_expected_types()
        {
            // Arrange
            var expected = new [] { typeof(Bar) };
            
            // Act
            var actual = FindAssembly.Containing<Foo>().GetTypes().ThatDeriveFrom<Foo>();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }

    public class Foo { }

    public class Bar : Foo { }
}
