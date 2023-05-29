using BPLogix.BooksCvsGenerator.Infrastructure.Abstracts;
using System.Text;

namespace BPLogix.BooksCvsGenerator.Infrastructure.Manager
{
    public class FileManager : IFileManager
    {
        private readonly string _folderName = "Files";
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileManager(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<byte[]> ReadFileAsync(IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream, cancellationToken);
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(bool, byte[])> WriteFileAsync(string fileName, string content, CancellationToken cancellationToken)
        {
            try
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, _folderName, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                byte[] proccesedContent = Encoding.UTF8.GetBytes(content);
                await File.WriteAllBytesAsync(filePath, proccesedContent, cancellationToken);

                return (true, proccesedContent);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
