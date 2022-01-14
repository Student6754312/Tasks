using Xunit;
using Labyrinth.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Domain.Tests
{
    public class LabyrinthTests
    {
        [Fact]
        public void LabyrinthTest()
        {

            // Arrange
            int l = 2;
            int r = 2;
            int c = 2;

            // Act
            var labyrinth = new Labyrinth(l, c, r);

            //Assert
            Assert.Equal(labyrinth.L, l);
            Assert.Equal(labyrinth.R, r);
            Assert.Equal(labyrinth.C, c);

            Assert.Equal(labyrinth.LabyrinthArray.Rank, 3);
            Assert.Equal(labyrinth.LabyrinthArray.GetLength(0), 2);
            Assert.Equal(labyrinth.LabyrinthArray.GetLength(1), 2);
            Assert.Equal(labyrinth.LabyrinthArray.GetLength(1), 2);

        }
    }
}