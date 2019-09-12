using System;
using Xunit;

namespace BDSA2019.Lecture02.Tests
{
    public class PostalCodeValidatorTests
    {
        [Fact]
        public void IsValid_given_2000_returns_true()
        {
            var actual = PostalCodeValidator.IsValid("2000");

            Assert.True(actual);
        }
        
        [Fact]
        public void IsValid_given_0200_returns_true()
        {
            var actual = PostalCodeValidator.IsValid("0200");

            Assert.True(actual);
        }

        [Fact]
        public void IsValid_given_200_returns_true()
        {
            var actual = PostalCodeValidator.IsValid("200");

            Assert.True(actual);
        }

        [Fact]
        public void IsValid_given_20000_returns_false()
        {
            var actual = PostalCodeValidator.IsValid("20000");

            Assert.False(actual);
        }

        [Fact]
        public void TryParse_given_2000_Frederiksberg_returns_true_and_outs_values()
        {
            var input = "2000 Frederiksberg";
            string postalCode;
            string locality;

            var actual = PostalCodeValidator.TryParse(input, out postalCode, out locality);

            Assert.True(actual);
            Assert.Equal("2000", postalCode);
            Assert.Equal("Frederiksberg", locality);
        }
    }
}
