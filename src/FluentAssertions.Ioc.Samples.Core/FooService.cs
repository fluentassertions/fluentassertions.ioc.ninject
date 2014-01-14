using FluentAssertions.Ioc.Samples.Interfaces;

namespace FluentAssertions.Ioc.Samples.Core
{
    public class FooService : IFooService
    {
        private readonly ISampleRepository sampleRepository;

        public FooService(ISampleRepository sampleRepository)
        {
            this.sampleRepository = sampleRepository;
        }

        public void DoWork()
        {
            sampleRepository.SomeMethod();
        }
    }
}
