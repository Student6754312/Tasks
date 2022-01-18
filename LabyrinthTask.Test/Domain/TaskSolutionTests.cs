using System;
using System.Collections.Generic;
using System.Linq;
using IOServices.Interfaces;
using IOServices.ServiceFactory;
using LabyrinthTask.Domain;
using LabyrinthTask.Services;
using Moq;
using Xunit;

namespace LabyrinthTask.Test.Domain
{
    public class TaskSolutionTests
    {
        private Mock<IOutputServiceFactory> _outputServiceFactoryMock;
        private Mock<IInputServiceFactory> _inputServiceFactoryMock;
        private Mock<IOutputService> _outputToFileServiceMock;
        private Mock<IInputService> _inputFromFileServiceMock;
        private Mock<ILabyrinthService> _labyrinthServiceMock;

        public TaskSolutionTests()
        {
           //Create Input/OutputServiceFactoriesMocks
            _outputServiceFactoryMock = new Mock<IOutputServiceFactory>();
            _inputServiceFactoryMock = new Mock<IInputServiceFactory>();

            //Create LabyrinthServiceMock
            _labyrinthServiceMock = new Mock<ILabyrinthService>();

            //Create Input/OutputServicesMocks
            _outputToFileServiceMock = new Mock<IOutputService>();
            _inputFromFileServiceMock = new Mock<IInputService>();

            //Setup Input/OutputServiceFactoriesMocks
            _inputServiceFactoryMock.Setup(s => s.GetService()).Returns(_inputFromFileServiceMock.Object);
            _outputServiceFactoryMock.Setup(s => s.GetService()).Returns(_outputToFileServiceMock.Object);
        }

        [Theory]
        [InlineData("1 0 1")]
        [InlineData("1")]
        [InlineData("1 2")]
        [InlineData("a b")]
        public void InputWrongInputLabyrinthParametrsTest(string s)
        {
            // Arrange
            //Setup Input/OutputServicesMocks
            _inputFromFileServiceMock.Setup(s => s.Input()).Returns(s);
            _outputToFileServiceMock.Setup(s => s.Output(" "));

            var taskSolution = new TaskSolution(_outputServiceFactoryMock.Object, _inputServiceFactoryMock.Object,
                            _labyrinthServiceMock.Object);
            // Act
            Action act = () => taskSolution.Input(new List<ILabyrinth>());

            //Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void InputExitTest()
        {
            // Arrange
            //Setup Input/OutputServicesMocks
            _inputFromFileServiceMock.Setup(s => s.Input()).Returns("0 0 0");


            var taskSolution = new TaskSolution(_outputServiceFactoryMock.Object, _inputServiceFactoryMock.Object,
                            _labyrinthServiceMock.Object);
            // Act
            var exception = Record.Exception(() => taskSolution.Input(new List<ILabyrinth>()));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void OutputTestEmptyLabyrinthList()
        {
            // Arrange
            var labyrinthList = new List<ILabyrinth>();
            var taskSolution = new TaskSolution(_outputServiceFactoryMock.Object, _inputServiceFactoryMock.Object,
                            _labyrinthServiceMock.Object);
            // Act
            var exception = Record.Exception(() => taskSolution.Output(labyrinthList));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void OutputTestEscape()
        {
            // Arrange
            var quader1 = new Quader('S', new QuaderLocation(0, 0, 0));
            quader1.Value = 1;

            List<IQuader> shortestPath = new() { quader1 };

            var labyrinthList = new List<ILabyrinth>();
            labyrinthList.Add(GetLabyrinth2x2x2("S.\n##\n##\nE."));

            _labyrinthServiceMock.Setup(s => s.BreadthFirstSearch(It.IsAny<ILabyrinth>(), out shortestPath)).Returns(false);

            var taskSolution = new TaskSolution(_outputServiceFactoryMock.Object, _inputServiceFactoryMock.Object,
                _labyrinthServiceMock.Object);

            //Act
            var exception = Record.Exception(() => taskSolution.Output(labyrinthList));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void OutputTestCaught()
        {
            // Arrange
            var quader1 = new Quader('S', new QuaderLocation(0, 0, 0));
            quader1.Value = 1;
            var quader2 = new Quader('.', new QuaderLocation(1, 1, 1));
            quader2.Value = 4;

            List<IQuader> shortestPath = new() { quader1, quader2 };

            var labyrinthList = new List<ILabyrinth>();
            labyrinthList.Add(GetLabyrinth2x2x2("S.\n##\n##\nE."));

            _labyrinthServiceMock.Setup(s => s.BreadthFirstSearch(It.IsAny<ILabyrinth>(), out shortestPath)).Returns(true);

            var taskSolution = new TaskSolution(_outputServiceFactoryMock.Object, _inputServiceFactoryMock.Object,
                _labyrinthServiceMock.Object);

            //Act
            var exception = Record.Exception(() => taskSolution.Output(labyrinthList));

            //Assert
            Assert.Null(exception);
        }

        private ILabyrinth GetLabyrinth2x2x2(string quadersString)
        {
            var inputString = quadersString.Split('\n').ToList();

            var labyrinth = new Labyrinth(2, 2, 2);

            var x = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    string str = inputString[x++];

                    for (int k = 0; k < 2; k++)
                    {
                        labyrinth.LabyrinthArray[i, j, k] = new Quader(str[k], new QuaderLocation(i, j, k));
                    }
                }
            }

            return labyrinth;
        }
    }
}