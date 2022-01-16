using System;
using System.IO;
using Xunit;

namespace IOServices.Tests
{
    public class OutputServiceTests
    {
        [Fact]
        public void ConsoleOutputLineTest()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOutputService iOutputService = new OutputToConsoleService();

            // Act
            iOutputService.ConsoleOutputLine("S.#.##E.");

            //Assert
            Assert.Equal("S.#.##E.\r\n", stringWriter.ToString());
        }

        [Fact]
        public void ConsoleOutputTest()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOutputService iOutputService = new OutputToConsoleService();

            // Act
            iOutputService.ConsoleOutput("S.");
            iOutputService.ConsoleOutput("S.");

            //Assert
            Assert.Equal("S.S.", stringWriter.ToString());
        }
    }
}