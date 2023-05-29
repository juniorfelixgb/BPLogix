using BPLogix.BooksCvsGenerator.Domain.Responses;
using BPLogix.BooksCvsGenerator.Domain.Shared.Enums;

namespace BPLogix.BooksCvsGenerator.Tests.UnitTests.Domain.Responses
{
    public class ProcessBookResponseTests
    {
        [Fact]
        public void ShouldBe_GetInstance_ProcessBookResponse_Successful()
        {
            // Arrange
            var instance = new ProcessBookResponse
            {
                bib_key = string.Empty,
                DataType = DataRetrievalType.None,
                details = new Details(),
            };

            // Act

            // Assert
            Assert.NotNull(instance);
        }
    }
}
