using BPLogix.BooksCvsGenerator.Infrastructure.Abstracts;
using BPLogix.BooksCvsGenerator.Infrastructure.Manager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;

namespace BPLogix.BooksCvsGenerator.Tests.UnitTests.Infrastructure.Manager
{
    public class FileManagerTests
    {
        private string webRootPath = "D:\\Data\\Projects\\BPLogix\\src\\BPLogix.BooksCvsGenerator\\wwwroot";
        private IFileManager _fileManager;
        public FileManagerTests()
        {
            var envMock = new Mock<IWebHostEnvironment>();

            envMock
                .Setup(e => e.WebRootPath)
                .Returns(webRootPath);

            _fileManager = new FileManager(envMock.Object);
        }

        [Fact]
        public async Task ShouldBe_ReadFileAsync_Successful()
        {
            // Arrange
            var stream = new MemoryStream();
            string name = "Test";
            string fileName = "Test.txt";
            var cancellationToken = new CancellationTokenSource().Token;
            var file = new FormFile(stream, 0, stream.Length, name, fileName);

            // Act
            var result = await _fileManager.ReadFileAsync(file, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldBe_ReadFileAsync_ThrowNullReferenceException_WhenFileToLoadIsNull()
        {
            // Arrange
            IFormFile file = null;
            var cancellationToken = new CancellationTokenSource().Token;
            var expected = new NullReferenceException();

            // Act
            var result = await Assert.ThrowsAsync<NullReferenceException>(async () => await _fileManager.ReadFileAsync(file, cancellationToken));

            // Assert
            Assert.Equal(expected.Message, result.Message);
        }

        [Fact]
        public async Task ShouldBe_WriteFileAsync_Successful()
        {
            // Arrange
            string content = "Test";
            string fileName = "Test.txt";
            var cancellationToken = new CancellationTokenSource().Token;

            // Act
            var result = await _fileManager.WriteFileAsync(fileName, content, cancellationToken);

            // Assert
            Assert.True(result.Item1);
        }

        [Fact]
        public async Task ShouldBe_WriteFileAsync_ThrowArgumentNullException_WhenContentIsNull()
        {
            // Arrange
            string content = null;
            string fileName = "Test.txt";
            var cancellationToken = new CancellationTokenSource().Token;
            var expected = new NullReferenceException("String reference not set to an instance of a String. (Parameter 's')");

            // Act
            var result = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _fileManager.WriteFileAsync(fileName, content, cancellationToken));

            // Assert
            Assert.Equal(expected.Message, result.Message);
        }
    }
}
