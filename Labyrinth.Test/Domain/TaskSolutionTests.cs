using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Labyrinth.Domain;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Labyrinth.Test.Domain
{
    public class TaskSolutionTests
    {
        private readonly ITaskSolution _taskSolution;

        public TaskSolutionTests()
        {
            var serviceProvider = DependencyContainer.GetContainer("appsettings.file.test.json");
            _taskSolution = serviceProvider.GetRequiredService<ITaskSolution>();
        }

        [Fact]
        public void InputTest()
        {
            // Arrange
            List<ILabyrinth> labyrinthList = new();

            // Act
            _taskSolution.Input(labyrinthList);

            //Assert
            Assert.Equal(1, labyrinthList.Count);
            Assert.Equal(2, labyrinthList[0].L);
            Assert.Equal(2, labyrinthList[0].R);
            Assert.Equal(2, labyrinthList[0].C);
            Assert.Equal(8, labyrinthList[0].LabyrinthArray.Length);
            Assert.Equal('S', labyrinthList[0].LabyrinthArray[0, 0, 0].View);
            Assert.Equal('.', labyrinthList[0].LabyrinthArray[0, 1, 1].View);
            Assert.Equal('E', labyrinthList[0].LabyrinthArray[1, 1, 0].View);
            Assert.Equal('#', labyrinthList[0].LabyrinthArray[1, 0, 0].View);

        }

        [Theory]
        [InlineData("0 0 1")]
        [InlineData("0 1")]
        //[InlineData("-01")]
        public void InputTestWrongLabyrinthParameters(string s)
        {
            // Arrange
            var serviceProvider = DependencyContainer.GetContainer("appsettings.console.test.json");
            var taskSolution = serviceProvider.GetRequiredService<ITaskSolution>();

            List<ILabyrinth> labyrinthList = new();

            var stringReader = new StringReader(s);
            Console.SetIn(stringReader);

            // Act
            Action act = () => taskSolution.Input(labyrinthList);

            //Assert

            Assert.Throws<FormatException>(act);

        }

        [Fact]
        public void OutputTestEntkommen()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var labyrinth = GetLabyrinth2x2x2("S.\n#.\n##\nE.");
            var labyrinthList = new List<ILabyrinth>();
            labyrinthList.Add(labyrinth);
            
            // Act
            _taskSolution.Output(labyrinthList);

            //Assert
            Assert.Equal("Ausgabe:\n\r\nEntkommen in 4 Minute(n)!)", stringWriter.ToString().Trim());
        }

        [Fact]
        public void OutputTestGefangen()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var labyrinth = GetLabyrinth2x2x2("S.\n##\n##\nE.");
            var labyrinthList = new List<ILabyrinth>();
            labyrinthList.Add(labyrinth);

            // Act
            _taskSolution.Output(labyrinthList);

            //Assert
            Assert.Equal("Ausgabe:\n\r\nGefangen :-(", stringWriter.ToString().Trim());
        }

        [Fact]
        public void OutputLabyrinthEmptyTest()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var labyrinthList = new List<ILabyrinth>();

            // Act
            _taskSolution.Output(labyrinthList);

            //Assert
            Assert.Equal("Labyrinth List ist Leer", stringWriter.ToString().Trim());
        }

        private ILabyrinth GetLabyrinth2x2x2(string quadersString)
        {
            var inputString = quadersString.Split('\n').ToList();
            
            var labyrinth = new Labyrinth.Domain.Labyrinth(2, 2, 2);

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