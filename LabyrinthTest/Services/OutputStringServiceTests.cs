using Xunit;
using Labyrinth.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Labyrinth.Services.Tests
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