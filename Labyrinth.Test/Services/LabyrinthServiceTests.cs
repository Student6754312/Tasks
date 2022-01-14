using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Labyrinth.Domain;
using Labyrinth.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Labyrinth.Test.Services
{
    public class LabyrinthServiceTests
    {
        int l = 2;
        int r = 2;
        int c = 2;
        private ServiceProvider _serviceProvider;

        public LabyrinthServiceTests()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IInputStringService, InputStringService>()
                .AddSingleton<ILabyrinthService, LabyrinthService>()
                .AddSingleton<IOutputStringService, OutputStringService>()
                .BuildServiceProvider();
        }

        [Fact]
        public void CreateLabyrinth_ThrowWrongNumberOfQuadersTest()
        {
            // Arrange
            var inputString = "S\n#.\n##\nE.";
            var stringReader = new StringReader(inputString);
            Console.SetIn(stringReader);

            var labyrinthService = _serviceProvider.GetService<ILabyrinthService>();

            var labyrinthArray = new IQuader[l, r, c];
            var mockLabyrinth = CreateILabyrinthMock(labyrinthArray);

            // Act
            Action act = () => labyrinthService.CreateLabyrinth(mockLabyrinth.Object);

            // Assert
            FormatException exception = Assert.Throws<FormatException>(act);
            Assert.Equal("Wrong Number of Quaders in a row 'S'", exception.Message);
        }


        [Fact]
        public void CreateLabyrinth_ThrowInputStringIsNullTest()
        {
            // Arrange
            var inputString = "";
            var stringReader = new StringReader(inputString);
            Console.SetIn(stringReader);

            var labyrinthService = _serviceProvider.GetService<ILabyrinthService>();

            var labyrinthArray = new IQuader[l, r, c];
            var mockLabyrinth = CreateILabyrinthMock(labyrinthArray);

            // Act
            Action act = () => labyrinthService.CreateLabyrinth(mockLabyrinth.Object);

            // Assert
            FormatException exception = Assert.Throws<FormatException>(act);
            Assert.Equal("Wrong Number of Quaders in a row ''", exception.Message);
        }

        [Fact]
        public void TestCreateLabyrinth()
        {
            // Arrange
            var inputString = "S.\n#.\n##\nE.";
            var stringReader = new StringReader(inputString);
            Console.SetIn(stringReader);

            var labyrinthService = _serviceProvider.GetService<ILabyrinthService>();

            var labyrinthArray = new IQuader[l, r, c];
            var mockLabyrinth = CreateILabyrinthMock(labyrinthArray);

            // Act
            labyrinthService.CreateLabyrinth(mockLabyrinth.Object);

            //Assert
            Assert.Equal('S', labyrinthArray[0, 0, 0].View);
            Assert.Equal('.', labyrinthArray[0, 0, 1].View);
            Assert.Equal('E', labyrinthArray[1, 1, 0].View);
            Assert.Equal('#', labyrinthArray[1, 0, 1].View);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        public void CreateAdjacencyListTest(int x, int y, int z)
        {
            // Arrange
            IQuader quader = new Quader('S', new QuaderLocation(x, y, z));

            var labyrinthService = _serviceProvider.GetService<ILabyrinthService>();

            var labyrinthArray = CreateLabyrinthArray("S.\n#.\n##\nE.");
            var mockLabyrinth = CreateILabyrinthMock(labyrinthArray);

            // Act
            var adjacencyList = labyrinthService.CreateAdjacencyList(quader, mockLabyrinth.Object);

            //Assert
            Assert.Equal(3, adjacencyList.Count);
            Assert.Equal(1, adjacencyList[0].Location.X);
            Assert.Equal(z, adjacencyList[0].Location.Y);
            Assert.Equal(z, adjacencyList[0].Location.Z);
        }

        [Fact]
        public void BreadthFirstSearchTest()
        {
            // Arrange
            var labyrinthService = _serviceProvider.GetService<ILabyrinthService>();
            var labyrinthArray = CreateLabyrinthArray("S.\n#.\n##\nE.");
            var mockLabyrinth = CreateILabyrinthMock(labyrinthArray);

            // Act
            var result = labyrinthService.BreadthFirstSearch(mockLabyrinth.Object, out List<IQuader> shortestPathlist);

            //Assert
            Assert.True(result);
            Assert.Equal(1, labyrinthArray[0, 0, 0].Value);
            Assert.Equal(2, labyrinthArray[0, 0, 1].Value);
            Assert.Equal(3, labyrinthArray[0, 1, 1].Value);
            Assert.Equal(-2, labyrinthArray[1, 1, 0].Value);
            Assert.Equal(-1, labyrinthArray[1, 0, 1].Value);
            Assert.Equal(4, labyrinthArray[1, 1, 1].Value);
            
            Assert.Equal( 5, shortestPathlist.Count);
            Assert.Equal(4, shortestPathlist[1].Value);
            
        }

        [Fact]
        public void BreadthFirstSearch_FalseResultTest()
        {
            // Arrange
            var labyrinthService = _serviceProvider.GetService<ILabyrinthService>();
            var labyrinthArray = CreateLabyrinthArray("S.\n##\n##\nE.");
            var mockLabyrinth = CreateILabyrinthMock(labyrinthArray);

            // Act
            var result = labyrinthService.BreadthFirstSearch(mockLabyrinth.Object, out List<IQuader>shortestPathlist);

            //Assert
            Assert.False(result);
            
        }
     
        [Fact]
        public void FindQuaderTest()
        {
            // Arrange
            var labyrinthService = _serviceProvider.GetService<ILabyrinthService>();

            var labyrinthArray = CreateLabyrinthArray("#.\n#.\n##\nE.");

            var mockLabyrinth = CreateILabyrinthMock(labyrinthArray);

            // Act
            var quadr = labyrinthService.FindQuader(QuaderTypes.Start, mockLabyrinth.Object);

            //Assert
            Assert.Null(quadr);
        }

        [Fact]
        public void PrintLabyrinthTest()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var labyrinthService = _serviceProvider.GetService<ILabyrinthService>();

            var labyrinthArray = CreateLabyrinthArray("S.\n#.\n##\nE.");
            var mockLabyrinth = CreateILabyrinthMock(labyrinthArray);

            // Act
            labyrinthService.PrintLabyrinth(mockLabyrinth.Object);

            //Assert
            Assert.Equal("\r\nS.\r\n#.\r\n\r\n##\r\nE.\r\n", stringWriter.ToString());
        }

        private Mock<ILabyrinth> CreateILabyrinthMock(IQuader[,,] labyrinthArray)
        {
            var mockLabyrinth = new Mock<ILabyrinth>();
            mockLabyrinth.Setup(lab => lab.L).Returns(2);
            mockLabyrinth.Setup(lab => lab.R).Returns(2);
            mockLabyrinth.Setup(lab => lab.C).Returns(2);
            mockLabyrinth.Setup(lab => lab.LabyrinthArray).Returns(labyrinthArray);
            return mockLabyrinth;
        }

        private IQuader[,,] CreateLabyrinthArray(string quadersString)
        {
            var inputString = quadersString.Split('\n').ToList();
            var labyrinthArray = new IQuader[l, r, c];
            var x = 0;
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    var str = inputString[x++];
                    for (int k = 0; k < c; k++)
                    {
                        labyrinthArray[i, j, k] = new Quader(str[k], new QuaderLocation(i, j, k));
                    }
                }
            }
            return labyrinthArray;
        }
    }
}