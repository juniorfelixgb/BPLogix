using BPLogix.BooksCvsGenerator.Infrastructure;
using BPLogix.BooksCvsGenerator.Infrastructure.Abstracts;
using BPLogix.BooksCvsGenerator.Infrastructure.Sections;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BPLogix.BooksCvsGenerator.Tests.UnitTests.Infrastructure
{
    public class AppSettingsTests
    {
        [Fact]
        public void ShouldBe_LoadAppSettings_Successful()
        {
            // Arrange
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var bookApiConfiguration = new ConfigurationBuilder()
                .AddJsonFile(basePath)
                .Build()
                .GetValue<BookApiConfiguration>("BookApiConfiguration:BaseUrl");

            var appSettingsMock = new Mock<IAppSettings>();

            // Act
            appSettingsMock
                .Setup(a => a.BookApiConfiguration)
                .Returns(bookApiConfiguration);
            var appSettings = appSettingsMock.Object;

            // Assert
            Assert.NotNull(appSettings);
        }

        [Fact]
        public void ShouldBe_GetInstanceAppSettings_Successful()
        {
            var configuration = new Mock<IConfiguration>();
            var instance = new AppSettings(configuration.Object);
            Assert.NotNull(instance);
        }

        [Fact]
        public void ShouldBe_GetInstanceBookApiConfiguration_Successful()
        {
            var instance = new BookApiConfiguration
            {
                BaseUrl = new Uri("http://test.com")
            };
            var baseUrl = instance.BaseUrl;
            Assert.NotNull(baseUrl);
        }
    }
}
