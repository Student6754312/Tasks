using System;
using System.IO;
using Labyrinth.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Labyrinth.Test.Services
{
    public class OutputStringServiceTests
    {
        [Fact]
        public void ConsoleOutuptLineTest()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IOutputStringService, OutputStringService>()
                .BuildServiceProvider();

            var outputStringService = serviceProvider.GetService<IOutputStringService>();

            // Act
            outputStringService.ConsoleOutuptLine("S.\n#.\n##\nE.");

            //Assert
            Assert.Equal("S.\n#.\n##\nE.\r\n", stringWriter.ToString());
        }
    }
}