using BPLogix.BooksCvsGenerator.Domain.Exceptions;
using BPLogix.BooksCvsGenerator.Infrastructure.Abstracts;
using MediatR;

namespace BPLogix.BooksCvsGenerator.Application.UsesCases.Book.ProcessBookFileCommand
{
    internal sealed class ProcessBookFileCommandHandler : IRequestHandler<ProcessBookFileCommand, (byte[], string)>
    {
        private readonly IBooksApi _booksApi;
        private readonly ICsvManager _cvsManager;
        public ProcessBookFileCommandHandler(
            IBooksApi booksApi,
            ICsvManager cvsManager)
        {
            _booksApi = booksApi;
            _cvsManager = cvsManager;
        }

        public async Task<(byte[], string)> Handle(ProcessBookFileCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var result = await _booksApi.ProcessBooksAsync(request.Request);

            if (!result.IsSuccess)
            {
                throw new BookNotProcessedException(result.Errors);
            }

            var isCvsCreated = await _cvsManager.GenerateCvsAsync(result.Data);

            if (!isCvsCreated.Item1)
            {
                throw new Exception("The cvs file was not created.");
            }

            return (isCvsCreated.Item2, isCvsCreated.Item3);
        }
    }
}
