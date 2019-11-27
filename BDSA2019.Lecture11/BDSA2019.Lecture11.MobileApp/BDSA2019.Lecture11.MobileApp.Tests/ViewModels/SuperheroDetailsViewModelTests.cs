using BDSA2019.Lecture11.Shared;
using BDSA2019.Lecture11.MobileApp.Models;
using BDSA2019.Lecture11.MobileApp.ViewModels;
using Moq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xunit;
using System.Net;

namespace BDSA2019.Lecture11.MobileApp.Tests.ViewModels
{
    public class SuperheroDetailsViewModelTests
    {
        [Fact]
        public void LoadCommand_populates_Superhero()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();

            var detailsDTO = new SuperheroDetailsDTO
            {
                Id = 42,
                Name = "name",
                AlterEgo = "alterEgo",
                Occupation = "occupation",
                CityName = "cityName",
                PortraitUrl = "https://image.com/portrait.jpg",
                BackgroundUrl = "https://image.com/background.jpg",
                FirstAppearance = 2000,
                Gender = Gender.Male,
                Powers = new[] { "power1", "power2" }
            };

            var client = new Mock<IRestClient>();
            client.Setup(s => s.GetAsync<SuperheroDetailsDTO>("superheroes/42")).ReturnsAsync((HttpStatusCode.OK, detailsDTO));

            var vm = new SuperheroDetailsViewModel(navigation.Object, messaging.Object, client.Object);

            var listDTO = new SuperheroListDTO
            {
                Id = 42,
                AlterEgo = "alterEgo",
                Name = "name",
                PortraitUrl = "https://image.com/image.jpg"
            };

            vm.LoadCommand.Execute(listDTO);

            Assert.Equal(42, vm.Id);
            Assert.Equal("alterEgo", vm.AlterEgo);
            Assert.Equal("name", vm.Name);
            Assert.Equal("occupation", vm.Occupation);
            Assert.Equal("cityName", vm.CityName);
            Assert.Equal("https://image.com/portrait.jpg", vm.PortraitUrl);
            Assert.Equal("https://image.com/background.jpg", vm.BackgroundUrl);
            Assert.Equal(2000, vm.FirstAppearance);
            Assert.Equal(Gender.Male, vm.Gender);
            Assert.Collection(vm.Powers, 
                s => Assert.Equal("power1", s),
                s => Assert.Equal("power2", s)
            );

            // Ensure not busy when command finished
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public void EditCommand_calls_EditAsync_with_DTO()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();

            var detailsDTO = new SuperheroDetailsDTO
            {
                Id = 42,
                Name = "name",
                AlterEgo = "alterEgo",
                Occupation = "occupation",
                CityName = "cityName",
                PortraitUrl = "https://image.com/portrait.jpg",
                BackgroundUrl = "https://image.com/background.jpg",
                FirstAppearance = 2000,
                Gender = Gender.Male,
                Powers = new[] { "power1", "power2" }
            };

            var client = new Mock<IRestClient>();
            client.Setup(s => s.GetAsync<SuperheroDetailsDTO>("superheroes/42")).ReturnsAsync((HttpStatusCode.OK, detailsDTO));

            var vm = new SuperheroDetailsViewModel(navigation.Object, messaging.Object, client.Object);

            var listDTO = new SuperheroListDTO
            {
                Id = 42,
                AlterEgo = "alterEgo",
                Name = "name",
                PortraitUrl = "https://image.com/image.jpg"
            };

            vm.LoadCommand.Execute(listDTO);

            vm.EditCommand.Execute(null);

            navigation.Verify(s => s.EditAsync(It.Is<SuperheroDetailsDTO>(h =>
                h.Id == 42 &&
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
    }
}
