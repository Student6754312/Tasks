using Xunit;

namespace Labyrinth.Test.Domain
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
            var labyrinth = new Labyrinth.Domain.Labyrinth(l, c, r);

            //Assert
            Assert.Equal(labyrinth.L, l);
            Assert.Equal(labyrinth.R, r);
            Assert.Equal(labyrinth.C, c);

            Assert.Equal( 3, labyrinth.LabyrinthArray.Rank);
            Assert.Equal( 2, labyrinth.LabyrinthArray.GetLength(0));
            Assert.Equal( 2, labyrinth.LabyrinthArray.GetLength(1));
            Assert.Equal( 2, labyrinth.LabyrinthArray.GetLength(1));

        }
    }
}