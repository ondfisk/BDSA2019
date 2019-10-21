using BDSA2019.Lecture07.Models.Facade;
using BDSA2019.Lecture07.Models.IoCContainer;
using BDSA2019.Lecture07.Models.Singleton;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace BDSA2019.Lecture07.App.Tests
{
    public class IoCContainerTests
    {
        // [Fact]
        // public void Container_can_build_IAnimalService()
        // {
        //     var container = IoCContainer.Container;

        //     var config = container.GetService<IAnimalService>();

        //     Assert.IsType<AnimalService>(config);
        // }

        // [Theory]
        // [InlineData(typeof(IAnimalService), typeof(AnimalService))]
        // public void Instance_configures_intf_to_impl(Type intf, Type impl)
        // {
        //     var container = IoCContainer.Container;

        //     var instance = container.GetService(intf);

        //     Assert.IsType(impl, instance);
        // }
    }
}
