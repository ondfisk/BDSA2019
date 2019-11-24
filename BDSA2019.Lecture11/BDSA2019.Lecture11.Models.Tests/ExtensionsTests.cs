using Xunit;

namespace BDSA2019.Lecture11.Models.Tests
{
    public class ExtensionsTests
    {
        [Theory]
        [InlineData(Shared.Gender.Female, Entities.Gender.Female)]
        [InlineData(Shared.Gender.Male, Entities.Gender.Male)]
        public void Convert_given_Shared_Gender_input_returns_expected(Shared.Gender input, Entities.Gender expected)
        {
            var actual = input.Convert();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(Entities.Gender.Female, Shared.Gender.Female)]
        [InlineData(Entities.Gender.Male, Shared.Gender.Male)]
        public void Convert_given_Entities_Gender_input_returns_expected(Entities.Gender input, Shared.Gender expected)
        {
            var actual = input.Convert();

            Assert.Equal(expected, actual);
        }
    }
}
