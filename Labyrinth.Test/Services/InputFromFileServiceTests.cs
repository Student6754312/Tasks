using System;
using System.IO;
using IOServices;
using Labyrinth.Services;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace Labyrinth.Test.Services
{
    public class InputFromFileServiceTests
    {
        [Fact]
        public void InputFromFileServiceTest()
        {
            // Arrange
            var serviceProvider = DependencyContainer.GetContainer("appsettings.file.test.json");

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var inputService = serviceProvider.GetService<IInputService>();

            // Act
            var str = inputService.Input();
            var str2 = inputService.Input();

            //Assert
            Assert.Equal("S.", str);
            Assert.Equal("#.", str2);
            
            Assert.Equal("S.\n\r\n#.\n\r\n", stringWriter.ToString());


        }
    }
}