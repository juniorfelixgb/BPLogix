using BPLogix.BooksCvsGenerator.Infrastructure.Sections;

namespace BPLogix.BooksCvsGenerator.Infrastructure.Abstracts
{
    public interface IAppSettings
    {
        public BookApiConfiguration BookApiConfiguration { get; }
    }
}
