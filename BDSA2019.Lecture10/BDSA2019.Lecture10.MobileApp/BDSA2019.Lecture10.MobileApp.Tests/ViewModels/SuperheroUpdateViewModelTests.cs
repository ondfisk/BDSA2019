using BDSA2019.Lecture10.MobileApp.Services;
using BDSA2019.Lecture10.MobileApp.ViewModels;
using Moq;
using Xamarin.Forms;
using Xunit;

namespace BDSA2019.Lecture10.MobileApp.Tests.ViewModels
{
    public class SuperheroUpdateViewModelTests
    {
        [Fact]
        public void CancelCommand_navigates_CancelAsync()
        {
            var navigation = new Mock<INavigationService>();
            var messaging = new Mock<IMessagingCenter>();
            var client = new Mock<IRestClient>();

            var vm = new SuperheroUpdateViewModel(navigation.Object, messaging.Object, client.Object);

            vm.CancelCommand.Execute(null);

            navigation.Verify(s => s.CancelAsync());
        }
    }
}
