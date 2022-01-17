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
        
        private IServiceProvider _serviceProvider;
        private ILabyrinthService _labyrinthService;

        public LabyrinthServiceTests()
        {
            _serviceProvider = DependencyContainer.GetContainer("appsettings.console.test.json");
            _labyrinthService = _serviceProvider.GetRequiredService<ILabyrinthService>();
        }

        [Fact]
        public void CreateLabyrinth_ThrowWrongNumberOfQuadersTest()
        {
            var inputString = "S\n#.\n##\nE.";
            var stringReader = new StringReader(inputString);
            Console.SetIn(stringReader);

            var labyrinthArray = new IQuader[l, r, c];
            var labyrinthMock = CreateILabyrinthMock(labyrinthArray);

            // Act
            Action act = () => _labyrinthService.CreateLabyrinth(labyrinthMock.Object);

            // Assert
            Assert.Throws<FormatException>(act);
        }


        [Fact]
        public void CreateLabyrinth_ThrowInputStringIsNullTest()
        {
            // Arrange
            var inputString = "";
            var stringReader = new StringReader(inputString);
            Console.SetIn(stringReader);

            var labyrinthArray = new IQuader[l, r, c];
            var labyrinthMock = CreateILabyrinthMock(labyrinthArray);

            // Act
            Action act = () => _labyrinthService.CreateLabyrinth(labyrinthMock.Object);

            // Assert
           Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void CreateLabyrinthTest()
        {
            // Arrange
            var inputString = "S.\n#.\n##\nE.";
            var stringReader = new StringReader(inputString);
            Console.SetIn(stringReader);

            var labyrinthArray = new IQuader[l, r, c];
            var labyrinthMock = CreateILabyrinthMock(labyrinthArray);

            // Act
            _labyrinthService.CreateLabyrinth(labyrinthMock.Object);

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

            var labyrinthArray = CreateLabyrinthArray("S.\n#.\n##\nE.");
            var labyrinthMock = CreateILabyrinthMock(labyrinthArray);

            // Act
            var adjacencyList = _labyrinthService.CreateAdjacencyList(quader, labyrinthMock.Object);

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
            var labyrinthArray = CreateLabyrinthArray("S.\n#.\n##\nE.");
            var labyrinthMock = CreateILabyrinthMock(labyrinthArray);

            // Act
            var result = _labyrinthService.BreadthFirstSearch(labyrinthMock.Object, out List<IQuader> shortestPathlist);

            //Assert
            
            //Success
            Assert.True(result);
            
            //Graph
            Assert.Equal(1, labyrinthArray[0, 0, 0].Value);
            Assert.Equal(2, labyrinthArray[0, 0, 1].Value);
            Assert.Equal(3, labyrinthArray[0, 1, 1].Value);
            Assert.Equal(-2, labyrinthArray[1, 1, 0].Value);
            Assert.Equal(-1, labyrinthArray[1, 0, 1].Value);
            Assert.Equal(4, labyrinthArray[1, 1, 1].Value);
            
            //Number of Quaders
            Assert.Equal( 5, shortestPathlist.Count);
            
            //Time
            Assert.Equal(4, shortestPathlist[1].Value);
            
        }

        [Fact]
        public void BreadthFirstSearch_FalseResultTest()
        {
            // Arrange
            var labyrinthArray = CreateLabyrinthArray("S.\n##\n##\nE.");
            var labyrinthMock = CreateILabyrinthMock(labyrinthArray);

            // Act
            var result = _labyrinthService.BreadthFirstSearch(labyrinthMock.Object, out List<IQuader>shortestPathlist);

            //Assert
            Assert.False(result);
            
        }

        [Fact]
        public void FindQuaderNotFoundTest()
        {
            // Arrange
            var labyrinthArray = CreateLabyrinthArray("#.\n#.\n##\nE.");

            var labyrinthMock = CreateILabyrinthMock(labyrinthArray);

            // Act
            var quadr = _labyrinthService.FindQuader(QuaderTypes.Start, labyrinthMock.Object);

            //Assert
            Assert.Null(quadr);
        }

        [Fact]
        public void FindQuaderTest()
        {
            // Arrange
            var labyrinthArray = CreateLabyrinthArray("S#.\n#.\n##\nE.");

            var labyrinthMock = CreateILabyrinthMock(labyrinthArray);

            // Act
            var quadr = _labyrinthService.FindQuader(QuaderTypes.Exit, labyrinthMock.Object);

            //Assert
            Assert.NotNull(quadr);
        }


        [Fact]
        public void PrintLabyrinthTest()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var labyrinthArray = CreateLabyrinthArray("S.\n#.\n##\nE.");
            var labyrinthMock = CreateILabyrinthMock(labyrinthArray);

            // Act
            _labyrinthService.PrintLabyrinth(labyrinthMock.Object);

            //Assert
            Assert.Equal("S.\r\n#.\r\n\r\n##\r\nE.", stringWriter.ToString().Trim());
        }

        private Mock<ILabyrinth> CreateILabyrinthMock(IQuader[,,] labyrinthArray)
        {
            var labyrinthMock = new Mock<ILabyrinth>();
            labyrinthMock.Setup(lab => lab.L).Returns(2);
            labyrinthMock.Setup(lab => lab.R).Returns(2);
            labyrinthMock.Setup(lab => lab.C).Returns(2);
            labyrinthMock.Setup(lab => lab.LabyrinthArray).Returns(labyrinthArray);
            return labyrinthMock;
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