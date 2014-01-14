using FluentAssertions.Ioc.Samples.Core;
using FluentAssertions.Ioc.Samples.Interfaces;

namespace FluentAssertions.Ioc.Samples.Data
{
    public class SampleRepository : ISampleRepository
    {
        public void SomeMethod()
        {
            var service = new SampleService();
        }
    }
}
