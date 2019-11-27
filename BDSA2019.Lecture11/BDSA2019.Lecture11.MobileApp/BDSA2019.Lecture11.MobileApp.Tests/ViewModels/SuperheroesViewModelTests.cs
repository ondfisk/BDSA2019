using BDSA2019.Lecture11.Shared;
using BDSA2019.Lecture11.MobileApp.Models;
using BDSA2019.Lecture11.MobileApp.ViewModels;
using Moq;
using System;
using Xamarin.Forms;
using Xunit;
using System.Net;

namespace BDSA2019.Lecture11.MobileApp.Tests.ViewModels
{
    public class SuperheroesViewModelTests
    {
        [Fact]
        public void Ctor_sets_Title_to_Browse()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();
            var service = new Mock<IAuthenticationService>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object, service.Object);

            Assert.Equal("Browse", vm.Title);
        }

        [Fact]
        public void Ctor_subscribes_to_NewSuperheroPage()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();
            var service = new Mock<IAuthenticationService>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object, service.Object);

            messaging.Verify(m => m.Subscribe(vm, "AddSuperhero", It.IsAny<Action<SuperheroCreateViewModel, SuperheroListDTO>>(), default));
        }

        [Fact]
        public void Ctor_subscribes_to_EditSuperheroPage()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();
            var service = new Mock<IAuthenticationService>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object, service.Object);

            messaging.Verify(m => m.Subscribe(vm, "UpdateSuperhero", It.IsAny<Action<SuperheroUpdateViewModel, SuperheroListDTO>>(), default));
        }

        [Fact]
        public void Ctor_subscribes_to_SuperheroDetailsPage()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();
            var service = new Mock<IAuthenticationService>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object, service.Object);

            messaging.Verify(m => m.Subscribe(vm, "DeleteSuperhero", It.IsAny<Action<SuperheroDetailsViewModel, int>>(), default));
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
            client.Setup(s => s.GetAllAsync<SuperheroListDTO>("superheroes")).ReturnsAsync((HttpStatusCode.OK, heroes));
            var service = new Mock<IAuthenticationService>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object, service.Object);

            vm.LoadCommand.Execute(null);

            Assert.Collection(vm.Items,
                a => Assert.Equal(a, batman),
                a => Assert.Equal(a, superman)
            );

            // Ensure not busy when command finished
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public void NewCommand_opens_NewAsync()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();
            var service = new Mock<IAuthenticationService>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object, service.Object);

            vm.NewCommand.Execute(null);

            navigation.Verify(s => s.NewAsync());

            // Ensure not busy when command finished
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public void ViewCommand_opens_ViewAsync_with_superhero()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();
            var service = new Mock<IAuthenticationService>();

            var vm = new SuperheroesViewModel(navigation.Object, messaging.Object, client.Object, service.Object)
            {
                SelectedItem = new SuperheroListDTO()
            };

            vm.ViewCommand.Execute(null);

            navigation.Verify(s => s.ViewAsync(vm.SelectedItem));

            // Ensure not busy when command finished
            Assert.False(vm.IsBusy);
        }
    }
}
