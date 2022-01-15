using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using IOServices;
using Moq;

namespace IOServices.Tests
{
    public class InputServiceTests
    {
        [Fact]
        public void GetFromFileTest()
        {
            // Arrange
            var mockFileSystem = new MockFileSystem();

            var mockInputFile = new MockFileData("line1\nline2\nline3");

            mockFileSystem.AddFile(@"C:\temp\in.txt", mockInputFile);

            IInputService inputService = new InputService(mockFileSystem);

            // Act
            var str = inputService.GetFromFile(@"C:\temp\in.txt");

            //Assert
            Assert.Equal("line1\r\nline2\r\nline3\r\n", str);
        }

        [Fact]
        public void GetFromFileThrowTest()
        {
            // Arrange
            IInputService inputService = new InputService();

            // Act
            Action act = () => inputService.GetFromFile("file");

            //Assert
            var exception = Assert.Throws<FileNotFoundException>(act);
            Assert.Equal($"File file not found", exception.Message);
        }
    }
}

namespace IOServicesTests
{
}