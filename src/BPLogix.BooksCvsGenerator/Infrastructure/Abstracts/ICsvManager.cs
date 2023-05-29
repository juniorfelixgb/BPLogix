using BPLogix.BooksCvsGenerator.Domain.Responses;

namespace BPLogix.BooksCvsGenerator.Infrastructure.Abstracts
{
    public interface ICsvManager
    {
        Task<(bool, byte[], string)> GenerateCvsAsync(List<ProcessBookResponse> processBooks);
    }
}
