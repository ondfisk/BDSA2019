using Xunit;

namespace BDSA2019.Lecture02.Tests
{
    public class CollectionUtilitiesTests
    {
        [Fact]
        public void GetEven_given_1_2_3_4_5_returns_2_4()
        {
            // Arrange
            int[] input = { 1, 2, 3, 4, 5 };

            // Act
            var output = CollectionUtilities.GetEven(input);

            // Assert
            int[] expected = { 2, 4 };
            Assert.Equal(expected, output);
        }

        [Fact]
        public void GetEven_given_1_2_42_4_5_returns_2_42()
        {
            // Arrange
            int[] input = { 1, 2, 42, 4, 5 };

            // Act
            var output = CollectionUtilities.GetEven(input);

            // Assert
            int[] expected = { 2, 42 };
            Assert.Equal(expected, output);
        }

        [Fact]
        public void Unique_given_1_2_3_2_2_2_4_returns_1_2_3_4()
        {
            // Arrange
            int[] input = { 1, 2, 3, 2, 2, 2, 4 };

            // Act
            var output = CollectionUtilities.Unique(input);

            // Assert
            int[] expected = { 1, 2, 3, 4 };
            Assert.Equal(expected, output);
        }
    }
}
