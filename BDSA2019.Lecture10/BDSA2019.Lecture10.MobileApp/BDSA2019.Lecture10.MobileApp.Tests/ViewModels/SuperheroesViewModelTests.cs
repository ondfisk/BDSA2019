using BDSA2019.Lecture10.MobileApp.Models;
using BDSA2019.Lecture10.MobileApp.Services;
using BDSA2019.Lecture10.MobileApp.ViewModels;
using BDSA2019.Lecture10.MobileApp.Views;
using Moq;
using System;
using Xamarin.Forms;
using Xunit;

namespace BDSA2019.Lecture10.MobileApp.Tests.ViewModels
{
    public class SuperheroesViewModelTests
    {
        [Fact]
        public void Ctor_sets_Title_to_Browse()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object);

            Assert.Equal("Browse", vm.Title);
        }

        [Fact]
        public void Ctor_subscribes_to_NewSuperheroPage()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object);

            messaging.Verify(m => m.Subscribe(vm, "AddSuperhero", It.IsAny<Action<NewSuperheroPage, SuperheroListDTO>>(), default));
        }

        [Fact]
        public void Ctor_subscribes_to_EditSuperheroPage()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object);

            messaging.Verify(m => m.Subscribe(vm, "UpdateSuperhero", It.IsAny<Action<EditSuperheroPage, SuperheroListDTO>>(), default));
        }

        [Fact]
        public void Ctor_subscribes_to_SuperheroDetailsPage()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object);

            messaging.Verify(m => m.Subscribe(vm, "DeleteSuperhero", It.IsAny<Action<SuperheroDetailsPage, int>>(), default));
        }

        [Fact]
        public void LoadCommand_populates_Items_from_api()
        {
            var batman = new SuperheroListDTO();
            var superman = new SuperheroListDTO();
            var heroes = new[] { batman, superman };

            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();
            client.Setup(s => s.GetAllAsync<SuperheroListDTO>("superheroes")).ReturnsAsync(heroes);

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object);

            vm.LoadCommand.Execute(null);

            Assert.Collection(vm.Items,
                a => Assert.Equal(a, batman),
                a => Assert.Equal(a, superman)
            );
        }

        [Fact]
        public void NewCommand_opens_NewAsync()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object);

            vm.NewCommand.Execute(null);

            navigation.Verify(s => s.NewAsync());
        }

        [Fact]
        public void ViewCommand_opens_ViewAsync_with_superhero()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object);

            var superhero = new SuperheroListDTO();
            vm.ViewCommand.Execute(superhero);

            navigation.Verify(s => s.ViewAsync(superhero));
        }
    }
}
