using BPLogix.BooksCvsGenerator.Domain.Requests;
using Microsoft.AspNetCore.Http;

namespace BPLogix.BooksCvsGenerator.Tests.UnitTests.Domain.Requests
{
    public class ProcessBookRequestTests
    {
        [Fact]
        public void ShouldBe_GetInstance_ProcessBookRequest_Successful()
        {
            // Arrange
            var request = new ProcessBookRequest();
            Stream stream = new MemoryStream();
            string name = "Test";
            string fileName = "Test.txt";

            // Act
            request.BibKeys = new FormFile(stream, 0, stream.Length, name, fileName);

            // Assert
            Assert.NotNull(request);
        }
    }
}
