using Xunit;
using Labyrinth.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Domain.Tests
{
    public class QuaderTests
    {
        [Theory]
        [InlineData('#', -1)]
        [InlineData('.', 0)]
        [InlineData('S', 1)]
        [InlineData('E', -2)]
        public void QuaderTest(char type, int value)
        {
            // Arrange

            var location = new QuaderLocation (0, 0, 1);

            // Act
            var quadr = new Quader(type, location);

            //Assert
            Assert.Equal(quadr.Value, value);
            Assert.Equal(quadr.Type, type);
            Assert.Equal(quadr.Location, location);
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