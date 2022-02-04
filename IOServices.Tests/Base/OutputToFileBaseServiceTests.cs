using System;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using IOServices.Base;
using IOServices.Interfaces;
using Xunit;

namespace IOServices.Tests.Base
{
    public class OutputToFileBaseServiceTests
    {
        private readonly MockFileSystem _mockFileSystem;
        
        public OutputToFileBaseServiceTests()
        {
            _mockFileSystem = new MockFileSystem();
        }

        [Fact]
        public void OutputServiceWrongOutputFilePathTest()
        {
            // Arrange
            var testAppSettings = new InputOutputSettings(outputFilePath: @"c\output.txt");

            // Act
            var outputService = new TestAbstractClass(_mockFileSystem, testAppSettings);
            Action act = () => outputService.Output("Bla");

            //Assert
            Assert.Throws<FileNotFoundException>(act);
        }

        [Fact]
        public void OutputServiceTest()
        {
            // Arrange
            var testAppSettings = new InputOutputSettings(outputFilePath: @"output.txt");

            // Act
            var outputService = new TestAbstractClass(_mockFileSystem, testAppSettings);
            outputService.Output("Bla");

            //Assert
            Assert.Equal("Bla", _mockFileSystem.GetFile("output.txt").TextContents.Trim());
        }

        private class TestAbstractClass : OutputToFileBaseService
        {
            public TestAbstractClass(IFileSystem fileSystem, IInputOutputSettings inputOutputSettings) : base(inputOutputSettings, fileSystem)
            {
            }
        }

    }
}