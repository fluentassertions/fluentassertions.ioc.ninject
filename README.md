FluentAssertions.Ioc.Ninject
============================

Extensions methods for [Fluent Assertions](https://github.com/dennisdoomen/fluentassertions) for testing Ninject mappings.

Why?
====

To test Ninject mappings using the [conventions extension](https://github.com/ninject/ninject.extensions.conventions) (or setup manually) are resolving.  Instead of finding out at runtime that your app won't run, you'll have a failing unit test instead.

Tests are written like this:

```` c#
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
````

Installing
==========

Please install the NuGet package:

````
PM> Install-Package FluentAssertions.Ioc.Ninject
````

Documentation
=============

Please see the [sample tests](https://github.com/kevinkuszyk/FluentAssertions.Ioc.Ninject/blob/master/src/FluentAssertions.Ioc.Ninject.SampleTests/IocTests.cs)

Continuous Integration
======================

[![Build status](https://ci.appveyor.com/api/projects/status/kexyhqbklr5eea02)](https://ci.appveyor.com/project/kevinkuszyk/fluentassertions-ioc-ninject)

