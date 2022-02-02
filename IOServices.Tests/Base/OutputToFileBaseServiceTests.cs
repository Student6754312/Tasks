using System;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using IOServices.Base;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace IOServices.Tests.Base
{
    public class OutputToFileBaseServiceTests
    {
        private readonly MockFileSystem _mockFileSystem;
        private readonly Mock<IOptions<TestApplicationSettings>> _optionsMock;

        public OutputToFileBaseServiceTests()
        {
            _mockFileSystem = new MockFileSystem();
            _optionsMock = new Mock<IOptions<TestApplicationSettings>>();
        }

        [Fact]
        public void OutputServiceWrongOutputFilePathTest()
        {
            // Arrange
            _optionsMock.Setup(o => o.Value).Returns(new TestApplicationSettings(outputFilePath: @"c\output.txt"));

            // Act
            var outputService = new TestAbstractClass(_mockFileSystem, _optionsMock.Object);
            Action act = () => outputService.Output("Bla");

            //Assert
            Assert.Throws<FileNotFoundException>(act);
        }

        [Fact]
        public void OutputServiceTest()
        {
            // Arrange
            _optionsMock.Setup(o => o.Value).Returns(new TestApplicationSettings(outputFilePath: @"output.txt"));

            // Act
            var outputService = new TestAbstractClass(_mockFileSystem, _optionsMock.Object);
            outputService.Output("Bla");

            //Assert
            Assert.Equal("Bla", _mockFileSystem.GetFile("output.txt").TextContents.Trim());
        }

        private class TestAbstractClass : OutputToFileBaseService<TestApplicationSettings>
        {
            public TestAbstractClass(IFileSystem fileSystem, IOptions<TestApplicationSettings> options) : base(options, fileSystem)
            {
            }
        }

    }
}