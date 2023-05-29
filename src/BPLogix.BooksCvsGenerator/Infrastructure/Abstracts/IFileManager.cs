using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPLogix.BooksCvsGenerator.Infrastructure.Abstracts
{
    public interface IFileManager
    {
        Task<(bool, byte[])> WriteFileAsync(string fileName, string content, CancellationToken cancellationToken = default);
        Task<byte[]> ReadFileAsync(IFormFile file, CancellationToken cancellationToken = default);
    }
}
