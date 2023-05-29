using BPLogix.BooksCvsGenerator.Domain.Exceptions;

namespace BPLogix.BooksCvsGenerator.Tests.UnitTests.Domain.Exceptions
{
    public class BookNotProcessedExceptionTests
    {
        [Fact]
        public void ShouldBe_ThrowBookNotProcessedException_Successful()
        {
            // Arrange
            string message = "Test";
            // Act
            var instance = new BookNotProcessedException(message);
            // Assert
            Assert.Equal(message, instance.Message);
        }
    }
}
