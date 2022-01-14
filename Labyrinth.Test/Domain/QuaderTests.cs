using System;
using Labyrinth.Domain;
using Xunit;

namespace Labyrinth.Test.Domain
{
    public class QuaderTests
    {
        [Theory]
        [InlineData('#', -1)]
        [InlineData('.', 0)]
        [InlineData('S', 1)]
        [InlineData('E', -2)]
        public void QuaderTest(char view, int value)
        {
            // Arrange

            var location = new QuaderLocation (0, 0, 1);

            // Act
            var quadr = new Quader(view, location);

            //Assert
            Assert.Equal( value, quadr.Value);
            Assert.Equal( view, quadr.View);
            Assert.Equal( location, quadr.Location);
        }
        [Fact]
        public void QuaderTest_ThrowIncorrectQuaderSymbol()
        {
            // Arrange
            var location = new QuaderLocation(0, 0, 1);

            // Act
            Action act = () => new Quader('R', location);

            //Assert
            FormatException exception = Assert.Throws<FormatException>(act);
            Assert.Equal($"Incorrect Quader Symbol - 'R'", exception.Message);
        }
    }
}