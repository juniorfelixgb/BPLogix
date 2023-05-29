using BPLogix.BooksCvsGenerator.Domain.Requests;
using BPLogix.BooksCvsGenerator.Domain.Responses;
using MediatR;

namespace BPLogix.BooksCvsGenerator.Application.UsesCases.Book.ProcessBookFileCommand
{
    internal sealed class ProcessBookFileCommand : IRequest<(byte[], string)>
    {
        public ProcessBookFileCommand(ProcessBookRequest request)
        {
            Request = request;
        }

        public ProcessBookRequest Request { get; private set; }
    }
}
