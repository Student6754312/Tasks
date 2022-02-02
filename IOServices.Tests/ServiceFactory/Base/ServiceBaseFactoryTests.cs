using System;
using System.Collections.Generic;
using System.IO;
using IOServices.Interfaces;
using IOServices.ServiceFactory.Base;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace IOServices.Tests.ServiceFactory.Base
{

    public class TestApplicationSettings
    {
        public TestApplicationSettings(string defaultInputService)
        {
            DefaultInputService = defaultInputService;
        }

        public string DefaultInputService { get; }
        public string InputFilePath { get; set; } = "input.txt";
        public string OutputFilePath { get; set; } = "output.txt";
    }

    public class TestAbstractClass <TS> : ServiceBaseFactory<TS, TestApplicationSettings> where TS: class
    {
        public TestAbstractClass(IEnumerable<TS> services, IOptions<TestApplicationSettings> options) : base(services, options)
        {
        }
    }
    public class ServiceBaseFactoryTests
    {
        private readonly Mock<IOptions<TestApplicationSettings>> _optionsMock;
        private readonly Mock<InputFromConsoleService> _inputFromConsoleServiceMock;
        private readonly Mock<OutputToConsoleService> _outputToConsoleServiceMock;

        public ServiceBaseFactoryTests()
        {
            _optionsMock = new Mock<IOptions<TestApplicationSettings>>();
            _inputFromConsoleServiceMock = new Mock<InputFromConsoleService>();
            _outputToConsoleServiceMock = new Mock<OutputToConsoleService>();
        }

        [Fact]
        public void GetInputServiceFromConsoleTest()
        {
            // Arrange
            var servicesMock = getInputServicesMock("Console");

            // Act
            var inputServiceFactory = new TestAbstractClass<IInputService>(servicesMock.Object, _optionsMock.Object);
            var service = inputServiceFactory.GetService();
            var isTrue = service.GetType().Name.StartsWith("InputFromConsoleService");

            //Assert
            Assert.True(isTrue);
        }

        [Fact]
        public void GetInputServiceFromFileTest()
        {
            // Arrange

            var servicesMock = getInputServicesMock("File");

            // Act
            var inputServiceFactory = new TestAbstractClass<IInputService>(servicesMock.Object, _optionsMock.Object);
            var service = inputServiceFactory.GetService();
            var isTrue = service.GetType().Name.StartsWith("InputFromFileService");

            //Assert
            Assert.True(isTrue);
        }
        [Fact]
        public void GetOutputServiceFromConsoleTest()
        {
            // Arrange
            var servicesMock = getOutputServicesMock("Console");

            // Act
            var inputServiceFactory = new TestAbstractClass<IOutputService>(servicesMock.Object, _optionsMock.Object);
            var service = inputServiceFactory.GetService();
            var isTrue = service.GetType().Name.StartsWith("OutputToConsoleService");

            //Assert
            Assert.True(isTrue);
        }

        [Fact]
        public void GetOutputServiceFromFileTest()
        {
            // Arrange
            var servicesMock = getOutputServicesMock("File");

            // Act
            var outputServiceFactory = new TestAbstractClass<IOutputService>(servicesMock.Object, _optionsMock.Object);
            var service = outputServiceFactory.GetService();
            var isTrue = service.GetType().Name.StartsWith("OutputToFileService");

            //Assert
            Assert.True(isTrue);
        }

        [Fact]
        public void GetOutputServiceFromFileDefaultInputServiceNotDefinedTest()
        {
            // Arrange
            var servicesMock = getOutputServicesMock(null);

            // Act
            var outputServiceFactory = new TestAbstractClass<IOutputService>(servicesMock.Object, _optionsMock.Object);
            Action act = () => outputServiceFactory.GetService();

            //Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void GetOutputServiceFromFileWrongDefaultInputServiceTest()
        {
            // Arrange
            var servicesMock = getOutputServicesMock("FFile");

            // Act
            var outputServiceFactory = new TestAbstractClass<IOutputService>(servicesMock.Object, _optionsMock.Object);
            Action act = () => outputServiceFactory.GetService();

            //Assert
            Assert.Throws<FormatException>(act);
        }

        private Mock<IEnumerable<IInputService>> getInputServicesMock(string type)
        {
            _optionsMock.Setup(o => o.Value).Returns(new TestApplicationSettings(type));
            var outputToFileServiceMock =
                new Mock<OutputToFileService<TestApplicationSettings>>(_outputToConsoleServiceMock.Object, _optionsMock.Object);
            var inputFromFileServiceMock =
                new Mock<InputFromFileService<TestApplicationSettings>>(_optionsMock.Object, outputToFileServiceMock.Object);

            var serviceList = new List<IInputService>
            {
                _inputFromConsoleServiceMock.Object,
                inputFromFileServiceMock.Object,
            };

            var servicesMock = new Mock<IEnumerable<IInputService>>();
            servicesMock.Setup(s => s.GetEnumerator()).Returns(() => serviceList.GetEnumerator());

            return servicesMock;
        }


        private Mock<IEnumerable<IOutputService>> getOutputServicesMock(string type)
        {
            _optionsMock.Setup(o => o.Value).Returns(new TestApplicationSettings(type));
            var outputToFileServiceMock =
                new Mock<OutputToFileService<TestApplicationSettings>>(_outputToConsoleServiceMock.Object, _optionsMock.Object);
            var serviceList = new List<IOutputService>
            {
                _outputToConsoleServiceMock.Object,
                outputToFileServiceMock.Object,
            };
            
            var servicesMock = new Mock<IEnumerable<IOutputService>>();
            servicesMock.Setup(s => s.GetEnumerator()).Returns(() => serviceList.GetEnumerator());

            return servicesMock;
        }
    }
}