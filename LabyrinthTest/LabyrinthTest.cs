using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using Moq;
using Labyrinth;
using Xunit.Sdk;

namespace LabyrinthTest
{
    public class LabyrinthTest
    {
        int l = 2;
        int r = 2;
        int c = 2;
        //private IQuader[,,] labyrinthArray;
        //private Type type;
        private object labyrinthInstance;

        public LabyrinthTest()
        {
            var type = typeof(Labyrinth.Labyrinth);
            labyrinthInstance = Activator.CreateInstance(type, l, r, c);
            //FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            //foreach (var field in fields)
            //{
            //    if (field.FieldType.IsArray)
            //    {
            //        labyrinthArray = (IQuader[,,])fields[3].GetValue(labyrinthInstance);

            //    }
            //}
        }

        [Fact]
        public void TestCreateLabyrinthThrowWrongNumberOfQuaders()
        {
            // Arrange
            var inputString = "S\n#.\n##\nE.";
            var labyrinth = new Labyrinth.Labyrinth(l, r, c);
            var stringReader = new StringReader(inputString);
            Console.SetIn(stringReader);

            // Act
            Action act = () => labyrinth.CreateLabyrinth();

            // Assert
            FormatException exception = Assert.Throws<FormatException>(act);
            Assert.Equal("Wrong Number of Quaders in a row 'S'", exception.Message);

        }

        [Fact]
        public void TestCreateLabyrinthThrowInputStringIsNull()
        {
            // Arrange
            var inputString = "";
            var labyrinth = new Labyrinth.Labyrinth(l, r, c);
            var stringReader = new StringReader(inputString);
            Console.SetIn(stringReader);

            // Act
            Action act = () => labyrinth.CreateLabyrinth();

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
            var labyrinth = new Labyrinth.Labyrinth(l, r, c);
            
            // Act
            labyrinth.CreateLabyrinth();
            var labyrinthArray = labyrinth.LabyrinthArray;

            int _l = labyrinthArray!.GetLength(0);
            int _r = labyrinthArray!.GetLength(1);
            int _c = labyrinthArray!.GetLength(2);

            //Assert
            Assert.Equal(2, _l);
            Assert.Equal(2, _r);
            Assert.Equal(2, _c);
            Assert.Equal('S', labyrinthArray[0, 0, 0].Type);
            Assert.Equal('.', labyrinthArray[0, 0, 1].Type);
            Assert.Equal('E', labyrinthArray[1, 1, 0].Type);
            Assert.Equal('#', labyrinthArray[1, 0, 1].Type);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        
        public void TestCreateAdjacencyList(int x, int y, int z)
        {
            // Arrange
            IQuader quader = new Quader('S', new QuaderLocation(x, y, z));
            MethodInfo method = GetPrivateMethod("CreateAdjacencyList");
            var inputString = "S.\n#.\n##\nE.";
            var stringReader = new StringReader(inputString);
            Console.SetIn(stringReader);

            // Act
            var adjacencyList = (List<QuaderLocation>)method.Invoke(labyrinthInstance, new object[] { quader });

            //Assert
            Assert.Equal(3, adjacencyList.Count);
            Assert.Equal(1, adjacencyList[0].X);
            Assert.Equal(z, adjacencyList[0].Y);
            Assert.Equal(z, adjacencyList[0].Z);
        }

       // [Fact]
        //public void BreadthFirstSearch()
        //{
            //// Arrange
            ////var method = GetMethod("BreadthFirstSearch");
            //IQuader quader = new Quader('S', new QuaderLocation(0, 0, 0));
            //var inputString = "S.\n#.\n##\nE.";
            //var stringReader = new StringReader(inputString);
            //Console.SetIn(stringReader);
            ////var method = GetPublicMethod("BreadthFirstSearch");
            ////Action act = () => method.Invoke(labyrinthInstance, new object[] { QuaderTypes.Start });

            ////var mockFindQuader = new Mock<Labyrinth.Labyrinth>();
            ////mockFindQuader.Setup(l => l.FindQuader(QuaderTypes.Start)).Returns(quader);

            ////var mockCreateLabyrinth = new Mock<Labyrinth.Labyrinth>();
            ////mockCreateLabyrinth.Setup(l => l.).Returns(quader);

            //var labyrinth = new Labyrinth.Labyrinth(l, r, c);
            //var labyrinthArray = labyrinth.LabyrinthArray;

            //// Act
            ////method.Invoke(labyrinthInstance, new object[] {});
            //labyrinth.BreadthFirstSearch();

            ////Assert
            //Assert.Equal(1, labyrinthArray[0, 0, 0].Value);
            //Assert.Equal(2, labyrinthArray[0, 0, 1].Value);
            //Assert.Equal(-2, labyrinthArray[1, 1, 0].Type);
            //Assert.Equal(-1, labyrinthArray[1, 0, 1].Type);
       // }


        private MethodInfo GetPrivateMethod(string methodName)
        {
            Type type = typeof(Labyrinth.Labyrinth);
            var method = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null)
            {
                throw new XunitException($"{methodName} method not found");
            }

            return method;
        }

        private MethodInfo GetPublicMethod(string methodName)
        {
            Type type = typeof(Labyrinth.Labyrinth);
            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            if (method == null)
            {
                throw new XunitException($"{methodName} method not found");
            }

            return method;
        }

    }
}
