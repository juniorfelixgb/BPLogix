using BPLogix.BooksCvsGenerator.Domain.Requests;
using BPLogix.BooksCvsGenerator.Domain.Responses;
using BPLogix.BooksCvsGenerator.Domain.Shared;

namespace BPLogix.BooksCvsGenerator.Infrastructure.Abstracts
{
    public interface IBooksApi
    {
        Task<Result<List<ProcessBookResponse>>> ProcessBooksAsync(ProcessBookRequest request, CancellationToken cancellation = default);
    }
}
