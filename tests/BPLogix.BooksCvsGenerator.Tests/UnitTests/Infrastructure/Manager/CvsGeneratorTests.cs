using BPLogix.BooksCvsGenerator.Domain.Responses;
using BPLogix.BooksCvsGenerator.Domain.Shared.Enums;
using BPLogix.BooksCvsGenerator.Infrastructure.Abstracts;
using BPLogix.BooksCvsGenerator.Infrastructure.Manager;
using Moq;

namespace BPLogix.BooksCvsGenerator.Tests.UnitTests.Infrastructure.Manager
{
    public class CvsGeneratorTests
    {
        private readonly ICsvManager _csvManager;
        public CvsGeneratorTests()
        {
            var fileManagerMock = new Mock<IFileManager>();
            fileManagerMock
                .Setup(c => c.WriteFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((true, new byte[0]));

            _csvManager = new CsvGenerator(fileManagerMock.Object);
        }

        [Fact]
        public async Task ShouldBe_GenerateCvsAsync_Successful()
        {
            // Arrange
            var request = new List<ProcessBookResponse>
            {
                new()
                {
                    bib_key = "048641714X",
                    DataType = DataRetrievalType.Server,
                    details = new()
                    {
                        authors = new Author[]
                        {
                            new Author
                            {
                                name = "Albert Einstein"
                            }
                        },
                        number_of_pages = 168,
                        publish_date = "2001",
                        subtitle = "the special and general theory",
                        title = "Relativity"
                    }
                }
            };

            // Act
            var result = await _csvManager.GenerateCvsAsync(request);

            // Assert
            Assert.True(result.Item1);
        }

        [Fact]
        public async Task ShouldBe_GenerateCvsAsync_ThrowArgumentNullException_WhenRequestIsNullOrRequestLenghtIsMinusZero()
        {
            // Arrange
            List<ProcessBookResponse> processBooks = null;
            var expected = new ArgumentNullException(nameof(processBooks));

            // Act
            var result = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _csvManager.GenerateCvsAsync(processBooks));

            // Assert
            Assert.Equal(expected.Message, result.Message);
        }
    }
}
