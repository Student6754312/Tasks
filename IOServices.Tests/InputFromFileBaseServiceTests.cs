using System;
using System.IO;
using Xunit;
using System.IO.Abstractions.TestingHelpers;
using IOServices.Base;

namespace IOServices.Tests
{
    public class InputFromFileBaseServiceTests
    {
        [Fact]
        public void InputServiceTest()
        {
            // Arrange
            var mockFileSystem = new MockFileSystem();

            var mockInputFile = new MockFileData("line1\nline2\nline3");

            mockFileSystem.AddFile(@"input.txt", mockInputFile);

            InputFromFileBaseService inputFromFileBaseService = new InputFromFileBaseService<>(mockFileSystem);
            IInputService inputService = new InputFromFileBaseService(mockFileSystem);

            // Act
            var str = inputService.Input();
            
            var list = inputFromFileBaseService.LoadInputFile();
            
            Assert.Equal("line1", str);

            Assert.Equal("line1", list[0]);
            Assert.Equal("line2", list[1]);
            Assert.Equal("line3", list[2]);

        }

        [Fact]
        public void InputServiceThrowFileNotFoundTest()
        {
            // Arrange
            var mockFileSystem = new MockFileSystem();

            var mockInputFile = new MockFileData("line1\nline2\nline3");

            mockFileSystem.AddFile(@"input1.txt", mockInputFile);

            // Act
            Action act = () => new InputFromFileBaseService(mockFileSystem);

            //Assert
            Assert.Throws<FileNotFoundException>(act);

        }
    }
}