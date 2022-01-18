using System;
using LabyrinthTask.Domain;
using Xunit;

namespace LabyrinthTask.Test.Domain
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
        public void Quader_ThrowIncorrect_QuaderSymbolTest()
        {
            // Arrange
            var location = new QuaderLocation(0, 0, 1);

            // Act
            Action act = () => new Quader('R', location);

            //Assert
            Assert.Throws<FormatException>(act);
        }
    }
}