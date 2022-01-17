using System;
using System.IO;
using IOServices.Base;
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
            iOutputService.Output("S.#.##E.");

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
            iOutputService.Output("S.");
            iOutputService.Output("S.");

            //Assert
            Assert.Equal("S.\r\nS.", stringWriter.ToString().Trim());
        }
    }
}