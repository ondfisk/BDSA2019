using System;
using System.IO;
using System.Text;
using Xunit;

namespace BDSA2019.Lecture01.App.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Main_prints_Hello_World()
        {
            // Arrange
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);

            Console.SetOut(writer);

            // Act
            Program.Main(new string[0]);

            var output = builder.ToString().Trim();

            // Assert
            Assert.Equal("Hello World!", output);
        }

        [Fact]
        public void Main_given_arg_prints_Hello_arg()
        {
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);
            Console.SetOut(writer);
            string[] args = { "Peter" };

            Program.Main(args);
            var output = builder.ToString().Trim();

            Assert.Equal("Hello Peter!", output);
        }
    }
}
