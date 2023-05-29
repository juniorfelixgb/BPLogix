using BPLogix.BooksCvsGenerator.Infrastructure.Abstracts;
using BPLogix.BooksCvsGenerator.Infrastructure.Sections;

namespace BPLogix.BooksCvsGenerator.Infrastructure
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;
        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public BookApiConfiguration BookApiConfiguration
        {
            get
            {
                return new()
                {
                    BaseUrl = new Uri(_configuration["BookApiConfiguration:BaseUrl"] ?? string.Empty),
                };
            }
        }
    }
}
