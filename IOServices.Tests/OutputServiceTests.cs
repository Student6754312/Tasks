using System;
using System.IO;
using IOServices;
using Xunit;

namespace IOServicesTests
{
    public class OutputServiceTests
    {
        [Fact]
        public void ConsoleOutputLineTest()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            IOutputService iOutputService = new OutputService();

            // Act
            iOutputService.ConsoleOutputLine("S.\n#.\n##\nE.");

            //Assert
            Assert.Equal("S.\n#.\n##\nE.\r\n", stringWriter.ToString());
        }
    }
}