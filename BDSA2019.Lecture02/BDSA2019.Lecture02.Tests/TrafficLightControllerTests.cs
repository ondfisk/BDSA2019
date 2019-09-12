using Xunit;
using static BDSA2019.Lecture02.TrafficLightColor;

namespace BDSA2019.Lecture02.Tests
{
    public class TrafficLightControllerTests
    {
        [Fact]
        public void CanIGo_given_Green_returns_True()
        {
            var ctrl = new TrafficLightController();

            var go = ctrl.MayIGo(Green);

            Assert.True(go);
        }

        [Fact]
        public void CanIGo_given_Yellow_returns_False()
        {
            var ctrl = new TrafficLightController();

            var go = ctrl.MayIGo(Yellow);

            Assert.False(go);
        }

        [Fact]
        public void CanIGo_given_Red_returns_False()
        {
            var ctrl = new TrafficLightController();

            var go = ctrl.MayIGo(Red);

            Assert.False(go);
        }

        [Theory]
        [InlineData(Green, true)]
        [InlineData(Yellow, false)]
        [InlineData(Red, false)]
        public void CanIGo_given_color_returns_expected(TrafficLightColor color, bool expected)
        {
            var ctrl = new TrafficLightController();

            var go = ctrl.MayIGo(color);

            Assert.Equal(expected, go);
        }
    }
}
