using BDSA2019.Lecture11.Shared;
using BDSA2019.Lecture11.MobileApp.Models;
using BDSA2019.Lecture11.MobileApp.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xunit;
using System.Net;
using Microsoft.Identity.Client;

namespace BDSA2019.Lecture11.MobileApp.Tests.ViewModels
{
    public class SuperheroCreateViewModelTests
    {
        [Fact]
        public void Ctor_sets_Title_to_New_Superhero()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var vm = new SuperheroCreateViewModel(navigation.Object, messaging.Object, client.Object);

            Assert.Equal("New Superhero", vm.Title);
        }

        [Fact]
        public void SaveCommand_saves_to_api()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var location = new Uri("https://api.com/superheroes/42");
            client.Setup(s => s.PostAsync("superheroes", It.IsAny<SuperheroCreateDTO>())).ReturnsAsync((HttpStatusCode.Created, location));

            var vm = new SuperheroCreateViewModel(navigation.Object, messaging.Object, client.Object)
            {
                Name = "name",
                AlterEgo = "alterEgo",
                Occupation = "occupation",
                CityName = "cityName",
                PortraitUrl = "https://image.com/portrait.jpg",
                BackgroundUrl = "https://image.com/background.jpg",
                FirstAppearance = 2000,
                Gender = Gender.Male,
                Powers = $"power1{Environment.NewLine}power2"
            };

            vm.SaveCommand.Execute(null);

            client.Verify(s => s.PostAsync("superheroes", It.Is<SuperheroCreateDTO>(h =>
                h.Name == "name" &&
                h.AlterEgo == "alterEgo" &&
                h.Occupation == "occupation" &&
                h.CityName == "cityName" &&
                h.PortraitUrl == "https://image.com/portrait.jpg" &&
                h.BackgroundUrl == "https://image.com/background.jpg" &&
                h.FirstAppearance == 2000 &&
                h.Gender == Gender.Male &&
                new HashSet<string> { "power1", "power2" }.SetEquals(h.Powers)
            )));

            // Ensure not busy when command finished
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public void SaveCommand_pushes_listDTO_to_messageCenter()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var location = new Uri("https://api.com/superheroes/42");
            client.Setup(s => s.PostAsync("superheroes", It.IsAny<SuperheroCreateDTO>())).ReturnsAsync((HttpStatusCode.Created, location));

            var vm = new SuperheroCreateViewModel(navigation.Object, messaging.Object, client.Object)
            {
                Name = "name",
                AlterEgo = "alterEgo",
                PortraitUrl = "https://image.com/portrait.jpg"
            };

            vm.SaveCommand.Execute(null);

            messaging.Verify(m => m.Send(vm, "AddSuperhero", It.Is<SuperheroListDTO>(h =>
                h.Id == 42 &&
                h.Name == "name" &&
                h.AlterEgo == "alterEgo" && 
                h.PortraitUrl == "https://image.com/portrait.jpg"
            )));

            // Ensure not busy when command finished
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public void SaveCommand_navigates_CancelAsync()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var location = new Uri("https://api.com/superheroes/42");
            client.Setup(s => s.PostAsync("superheroes", It.IsAny<SuperheroCreateDTO>())).ReturnsAsync((HttpStatusCode.Created, location));

            var vm = new SuperheroCreateViewModel(navigation.Object, messaging.Object, client.Object)
            {
                Name = "name",
                AlterEgo = "alterEgo",
                PortraitUrl = "https://image.com/portrait.jpg"
            };

            vm.SaveCommand.Execute(null);

            navigation.Verify(s => s.CancelAsync());

            // Ensure not busy when command finished
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public void CancelCommand_navigates_CancelAsync()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var vm = new SuperheroCreateViewModel(navigation.Object, messaging.Object, client.Object);

            vm.CancelCommand.Execute(null);

            navigation.Verify(s => s.CancelAsync());

            // Ensure not busy when command finished
            Assert.False(vm.IsBusy);
        }
    }
}
