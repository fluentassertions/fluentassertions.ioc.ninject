using FluentAssertions.Ioc.Samples.Entities;
using FluentAssertions.Ioc.Samples.Interfaces;

namespace FluentAssertions.Ioc.Samples.Core
{
    public class SampleService : ISampleService
    {
        public void SomeMethod()
        {
            var entity = new Entity();
        }
    }
}
