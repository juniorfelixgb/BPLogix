using BPLogix.BooksCvsGenerator.Domain.Shared;

namespace BPLogix.BooksCvsGenerator.Tests.UnitTests.Domain.Shared
{
    public class ResultTests
    {
        [Fact]
        public void ShouldBe_GetInstance_Result_Successful()
        {
            // Arrange
            int expected = 1;
            bool isSuccess = true;
            string errors = null;
            ResultDetails result = new ResultDetails { Message = string.Empty };
            var instance = new Result<int>(expected, isSuccess, errors, result);

            // Act

            // Assert
            Assert.NotNull(instance);
        }
    }
}
