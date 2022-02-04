using System;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using IOServices.Base;
using IOServices.Interfaces;
using Xunit;

namespace IOServices.Tests.Base
{
    public class InputFromFileBaseServiceTests
    {
        private readonly MockFileSystem _mockFileSystem;
        
        public InputFromFileBaseServiceTests()
        {
            _mockFileSystem = new MockFileSystem();
        }

        [Fact]
        public void InputServiceTest()
        {
            // Arrange
            var mockInputFile = new MockFileData("line1\nline2\nline3");
            _mockFileSystem.AddFile(@"input.txt", mockInputFile);
            var inputService =  new TestAbstractClass(_mockFileSystem, new InputOutputSettings());

            // Act
            var str = inputService.Input();
            var str1 = inputService.Input();
            var str2 = inputService.Input();
            
            //Assert
            Assert.Equal("line1", str);
            Assert.Equal("line2", str1);
            Assert.Equal("line3", str2);
        }

        [Fact]
        public void InputServiceEmptyFileTest()
        {
            // Arrange
            var mockInputFile = new MockFileData("");
            _mockFileSystem.AddFile(@"input.txt", mockInputFile);
            
            var inputService = new TestAbstractClass(_mockFileSystem, new InputOutputSettings());

            // Act
            Action act = () => inputService.Input();

            //Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void InputServiceIfFileEndGoTopTest()
        {
            // Arrange
            var mockInputFile = new MockFileData("line1\n");
            _mockFileSystem.AddFile(@"input.txt", mockInputFile);
            
            var inputService = new TestAbstractClass(_mockFileSystem, new InputOutputSettings());

            // Act
            var str = inputService.Input();
            var str1 = inputService.Input();
            var str2 = inputService.Input();

            Assert.Equal("line1", str);
            Assert.Equal("line1", str1);
            Assert.Equal("line1", str2);
        }


        [Fact]
        public void InputServiceThrowFileNotFoundTest()
        {
            // Arrange
            _mockFileSystem.RemoveFile(@"input.txt");
            
            // Act
            var inputService = new TestAbstractClass(_mockFileSystem, new InputOutputSettings());
            Action act = () => inputService.Input();

            //Assert
            Assert.Throws<FileNotFoundException>(act);

        }

        private class TestAbstractClass : InputFromFileBaseService
        {
            public TestAbstractClass(IFileSystem fileSystem, IInputOutputSettings inputOutputSettings) : base(inputOutputSettings, fileSystem)
            {
            }
        }
    }
}