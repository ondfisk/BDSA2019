using Xunit;

namespace BDSA2019.Lecture02.Tests
{
    public class TrafficLightControllerTests
    {
        [Fact]
        public void CanIGo_given_Green_returns_True()
        {
            Assert.True(false);
        }

        [Fact]
        public void CanIGo_given_Yellow_returns_False()
        {
            Assert.True(false);
        }

        [Fact]
        public void CanIGo_given_Red_returns_False()
        {
            Assert.True(false);
        }

        [Fact]
        public void CanIGo_given_InvalidColor_throws_ArgumentException()
        {
            Assert.True(false);
        }

        //[Theory]
        //[InlineData("Green", true)]
        //[InlineData("Yellow", false)]
        //[InlineData("Red", false)]
        public void CanIGo_given_color_returns_expected(string color, bool expected)
        {
            Assert.True(false);
        }
    }
}
