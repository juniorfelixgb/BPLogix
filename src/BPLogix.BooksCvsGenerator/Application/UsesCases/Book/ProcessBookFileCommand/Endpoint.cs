using BPLogix.BooksCvsGenerator.Domain.Requests;
using FastEndpoints;
using MediatR;

namespace BPLogix.BooksCvsGenerator.Application.UsesCases.Book.ProcessBookFileCommand
{
    public class Endpoint : Endpoint<ProcessBookRequest>
    {
        private readonly ISender _sender;
        public Endpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Post("/books/process-file");
            AllowFileUploads();
            AllowAnonymous();
        }

        public override async Task HandleAsync(ProcessBookRequest req, CancellationToken ct)
        {
            var result = await _sender.Send(new ProcessBookFileCommand(req), ct);

            var stream = new MemoryStream(result.Item1);

            await SendStreamAsync(
                stream,
                result.Item2,
                result.Item1.Length,
                "text/csv",
                DateTime.Now,
                false,
                ct
            );
        }
    }
}
