using Microsoft.AspNetCore.Mvc;

namespace BPLogix.BooksCvsGenerator.Domain.Requests
{
    public class ProcessBookRequest
    {
        [FromForm]
        public IFormFile BibKeys { get; set; }
    }
}
