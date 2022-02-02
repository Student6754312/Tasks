using System;
using System.IO;
using System.IO.Abstractions;
using Xunit;
using System.IO.Abstractions.TestingHelpers;
using IOServices.Base;
using Microsoft.Extensions.Options;
using Moq;

namespace IOServices.Tests
{
    public class TestApplicationSettings
    {
        public string DefaultInputService { get; set; } = "File";
        public string InputFilePath { get; set; } = "input.txt";
        public string OutputFilePath { get; set; } = "output.txt";
    }

    public class TestAbstractClass : InputFromFileBaseService<TestApplicationSettings>
    {
        public TestAbstractClass(IFileSystem fileSystem, IOptions<TestApplicationSettings> options) : base(options, fileSystem)
        {
        }
    }

    public class InputFromFileBaseServiceTests
    {
        private readonly MockFileSystem _mockFileSystem;
        private readonly Mock<IOptions<TestApplicationSettings>> _optionsMock;

        public InputFromFileBaseServiceTests()
        {
            _mockFileSystem = new MockFileSystem();
            _optionsMock = new Mock<IOptions<TestApplicationSettings>>();
            _optionsMock.Setup(o => o.Value).Returns(new TestApplicationSettings());
        }

        [Fact]
        public void InputServiceTest()
        {
            // Arrange
            var mockInputFile = new MockFileData("line1\nline2\nline3");
            _mockFileSystem.AddFile(@"input.txt", mockInputFile);
            var inputService =  new TestAbstractClass(_mockFileSystem, _optionsMock.Object);

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
            
            var inputService = new TestAbstractClass(_mockFileSystem, _optionsMock.Object);

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
            
            var inputService = new TestAbstractClass(_mockFileSystem, _optionsMock.Object);

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
            var inputService = new TestAbstractClass(_mockFileSystem, _optionsMock.Object);
            Action act = () => inputService.Input();

            //Assert
            Assert.Throws<FileNotFoundException>(act);

        }
    }
}