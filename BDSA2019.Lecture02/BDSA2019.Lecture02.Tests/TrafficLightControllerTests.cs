using System;
using Xunit;

namespace BDSA2019.Lecture02.Tests
{
    using static TrafficLightColor;

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
        [InlineData(TrafficLightColor.Green, true)]
        [InlineData(TrafficLightColor.Yellow, false)]
        [InlineData(TrafficLightColor.Red, false)]
        public void CanIGo_given_color_returns_expected(TrafficLightColor color, bool expected)
        {
            var ctrl = new TrafficLightController();

            var go = ctrl.MayIGo(color);

            Assert.Equal(expected, go);
        }
    }
}
