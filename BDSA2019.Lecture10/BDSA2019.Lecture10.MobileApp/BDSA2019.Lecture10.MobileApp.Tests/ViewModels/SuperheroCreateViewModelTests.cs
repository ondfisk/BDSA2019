using BDSA2019.Lecture10.MobileApp.Models;
using BDSA2019.Lecture10.MobileApp.Services;
using BDSA2019.Lecture10.MobileApp.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xunit;

namespace BDSA2019.Lecture10.MobileApp.Tests.ViewModels
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
            client.Setup(s => s.PostAsync("superheroes", It.IsAny<SuperheroCreateDTO>())).ReturnsAsync(location);

            var vm = new SuperheroCreateViewModel(navigation.Object, messaging.Object, client.Object);

            vm.Name = "name";
            vm.AlterEgo = "alterEgo";
            vm.Occupation = "occupation";
            vm.CityName = "cityName";
            vm.PortraitUrl = "https://image.com/portrait.jpg";
            vm.BackgroundUrl = "https://image.com/background.jpg";
            vm.FirstAppearance = 2000;
            vm.Gender = Gender.Male;
            vm.Powers = $"power1{Environment.NewLine}power2";

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
        }

        [Fact]
        public void SaveCommand_pushes_listDTO_to_messageCenter()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var location = new Uri("https://api.com/superheroes/42");
            client.Setup(s => s.PostAsync("superheroes", It.IsAny<SuperheroCreateDTO>())).ReturnsAsync(location);

            var vm = new SuperheroCreateViewModel(navigation.Object, messaging.Object, client.Object);

            vm.Name = "name";
            vm.AlterEgo = "alterEgo";
            vm.PortraitUrl = "https://image.com/portrait.jpg";

            vm.SaveCommand.Execute(null);

            messaging.Verify(m => m.Send(vm, "AddSuperhero", It.Is<SuperheroListDTO>(h =>
                h.Id == 42 &&
                h.Name == "name" &&
                h.AlterEgo == "alterEgo" && 
                h.PortraitUrl == "https://image.com/portrait.jpg"
            )));
        }

        [Fact]
        public void SaveCommand_navigates_CancelAsync()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var location = new Uri("https://api.com/superheroes/42");
            client.Setup(s => s.PostAsync("superheroes", It.IsAny<SuperheroCreateDTO>())).ReturnsAsync(location);

            var vm = new SuperheroCreateViewModel(navigation.Object, messaging.Object, client.Object);

            vm.Name = "name";
            vm.AlterEgo = "alterEgo";
            vm.PortraitUrl = "https://image.com/portrait.jpg";

            vm.SaveCommand.Execute(null);

            navigation.Verify(s => s.CancelAsync());
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
        }
    }
}
