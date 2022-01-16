using System;
using System.IO;
using Fibonacci.Services;
using IOServices;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Fibonacci.Test.Services
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
            Assert.Equal("6", str);
            Assert.Equal("5", str2);

            Assert.Equal("6\n\r\n5\n\r\n", stringWriter.ToString());

        }

    }
}