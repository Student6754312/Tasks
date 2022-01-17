using System;
using System.IO;
using IOServices.Base;
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
            var str =  inputService.Input();
            var str1 = inputService.Input();
            var str2 = inputService.Input();
            var str3 = inputService.Input();
            var str4 = inputService.Input();

            //Assert
            Assert.Equal("2 2 2", str);
            Assert.Equal("S.", str1);
            Assert.Equal("#.", str2);
            Assert.Equal("##", str3);
            Assert.Equal("E.", str4);
            
            Assert.Equal("2 2 2\n\r\nS.\n\r\n#.\n\r\n##\n\r\nE.", stringWriter.ToString().Trim());


        }
    }
}