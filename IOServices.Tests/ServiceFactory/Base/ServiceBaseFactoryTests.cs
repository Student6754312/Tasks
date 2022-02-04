using System;
using System.Collections.Generic;
using IOServices.Interfaces;
using IOServices.ServiceFactory;
using IOServices.ServiceFactory.Base;
using Moq;
using Xunit;

namespace IOServices.Tests.ServiceFactory.Base
{

    public class InputOutputSettings : IInputOutputSettings
    {
        public InputOutputSettings(string defaultInputService)
        {
            DefaultInputService = defaultInputService;
        }

        public string DefaultInputService { get; set; }
        public string InputFilePath { get; set; } = "input.txt";
        public string OutputFilePath { get; set; } = "output.txt";
    }

    public class TestAbstractClass <TS> : ServiceBaseFactory<TS> where TS: class
    {
        public TestAbstractClass(IEnumerable<TS> services, IInputOutputSettings inputOutputSettings) : base(services, inputOutputSettings)
        {
        }
    }
    public class ServiceBaseFactoryTests
    {
        private readonly Mock<InputFromConsoleService> _inputFromConsoleServiceMock;
        private readonly Mock<OutputToConsoleService> _outputToConsoleServiceMock;
        private IInputOutputSettings _inputOutputSettings;

        public ServiceBaseFactoryTests()
        {
            _inputFromConsoleServiceMock = new Mock<InputFromConsoleService>();
            _outputToConsoleServiceMock = new Mock<OutputToConsoleService>();
        }

        [Fact]
        public void GetInputServiceFromConsoleTest()
        {
            // Arrange
            _inputOutputSettings = new InputOutputSettings("Console");
            var servicesMock = GetInputServicesMock();

            // Act
            var inputServiceFactory = new TestAbstractClass<IInputService>(servicesMock.Object, _inputOutputSettings);
            var service = inputServiceFactory.GetService();
            var isTrue = service.GetType().Name.StartsWith("InputFromConsoleService");

            //Assert
            Assert.True(isTrue);
        }

        [Fact]
        public void GetInputServiceFromFileTest()
        {
            // Arrange
            _inputOutputSettings = new InputOutputSettings("File");
            var servicesMock = GetInputServicesMock();

            // Act
            var inputServiceFactory = new TestAbstractClass<IInputService>(servicesMock.Object, _inputOutputSettings);
            var service = inputServiceFactory.GetService();
            var isTrue = service.GetType().Name.StartsWith("InputFromFileService");

            //Assert
            Assert.True(isTrue);
        }
        [Fact]
        public void GetOutputServiceFromConsoleTest()
        {
            // Arrange
            _inputOutputSettings = new InputOutputSettings("Console");
            var servicesMock = GetOutputServicesMock();

            // Act
            var inputServiceFactory = new TestAbstractClass<IOutputService>(servicesMock.Object, _inputOutputSettings);
            var service = inputServiceFactory.GetService();
            var isTrue = service.GetType().Name.StartsWith("OutputToConsoleService");

            //Assert
            Assert.True(isTrue);
        }

        [Fact]
        public void GetOutputServiceFromFileTest()
        {
            // Arrange
            _inputOutputSettings = new InputOutputSettings("File");
            var servicesMock = GetOutputServicesMock();

            // Act
            var outputServiceFactory = new TestAbstractClass<IOutputService>(servicesMock.Object, _inputOutputSettings);
            var service = outputServiceFactory.GetService();
            var isTrue = service.GetType().Name.StartsWith("OutputToFileService");

            //Assert
            Assert.True(isTrue);
        }

        [Fact]
        public void GetOutputServiceFromFileDefaultInputServiceNotDefinedTest()
        {
            // Arrange
            _inputOutputSettings = new InputOutputSettings(null);
            var servicesMock = GetOutputServicesMock();

            // Act
            var outputServiceFactory = new TestAbstractClass<IOutputService>(servicesMock.Object, _inputOutputSettings);
            Action act = () => outputServiceFactory.GetService();

            //Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void GetOutputServiceFromFileWrongDefaultInputServiceTest()
        {
            // Arrange
            _inputOutputSettings = new InputOutputSettings("FFile");
            var servicesMock = GetOutputServicesMock();

            // Act
            var outputServiceFactory = new TestAbstractClass<IOutputService>(servicesMock.Object, _inputOutputSettings);
            Action act = () => outputServiceFactory.GetService();

            //Assert
            Assert.Throws<FormatException>(act);
        }

        private Mock<IEnumerable<IInputService>> GetInputServicesMock()
        {
            var outputServiceFactoryMock =
                new Mock<OutputServiceFactory>(GetOutputServicesMock().Object, _inputOutputSettings);
            
            var inputFromFileServiceMock =
                new Mock<InputFromFileService>(_inputOutputSettings, outputServiceFactoryMock.Object);

            var serviceList = new List<IInputService>
            {
                _inputFromConsoleServiceMock.Object,
                inputFromFileServiceMock.Object,
            };

            var servicesMock = new Mock<IEnumerable<IInputService>>();
            servicesMock.Setup(s => s.GetEnumerator()).Returns(() => serviceList.GetEnumerator());

            return servicesMock;
        }


        private Mock<IEnumerable<IOutputService>> GetOutputServicesMock()
        {

            var outputToFileServiceMock =
                new Mock<OutputToFileService>(_inputOutputSettings, _outputToConsoleServiceMock.Object);
            
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