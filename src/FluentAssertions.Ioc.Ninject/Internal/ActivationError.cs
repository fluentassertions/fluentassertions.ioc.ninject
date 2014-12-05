using System;
using Ninject;

namespace FluentAssertions.Ioc.Ninject.Internal
{
    class ActivationError
    {
        public Type Type { get; set; }
        public ActivationException Exception { get; set; }
    }
}