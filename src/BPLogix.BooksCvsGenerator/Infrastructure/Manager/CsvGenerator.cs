using BPLogix.BooksCvsGenerator.Domain.Responses;
using BPLogix.BooksCvsGenerator.Domain.Shared;
using BPLogix.BooksCvsGenerator.Infrastructure.Abstracts;
using System.Text;

namespace BPLogix.BooksCvsGenerator.Infrastructure.Manager
{
    public class CsvGenerator : ICsvManager
    {
        private readonly IFileManager _fileManager;
        public CsvGenerator(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public async Task<(bool, byte[], string)> GenerateCvsAsync(List<ProcessBookResponse> processBooks)
        {
            try
            {
                if (processBooks is null || processBooks.Count <= 0)
                {
                    throw new ArgumentNullException(nameof(processBooks));
                }

                var content = ProcessCsvContent(processBooks);

                string fileName = $"{Guid.NewGuid().ToString("N")}_{DateTime.Now.ToString("ddMMyyyy")}.csv";

                var result = await _fileManager.WriteFileAsync(fileName, content);

                return (result.Item1, result.Item2, fileName);
            }
            catch (Exception)
            {
                throw;
                //new FileNotFoundException("The data was not saved successfully to the CSV file.")
            }
        }

        private string ProcessCsvContent(List<ProcessBookResponse> processBooks)
        {
            string separator = ",";
            var output = new StringBuilder();
            string[] headings = { "Row Number", "Data Retrieval Type", "ISBN", "Title", "Subtitle", "Author Name(s)", "Number of Pages", "Publish Date" };
            output.AppendLine(string.Join(separator, headings));

            for (int rowNumber = 0; rowNumber < processBooks.Count; rowNumber++)
            {
                var processBook = processBooks[rowNumber];

                string isbn = processBook.bib_key.Contains(':') ? processBook.bib_key.Split(':')[1] : string.Empty;
                string authors = processBook.details?.authors?.Length > 1 ? string.Join('-', processBook.details.authors.Select(a => a.name)) : processBook?.details?.authors?.FirstOrDefault()?.name ?? string.Empty;

                string newLine = $"{rowNumber + 1},{processBook?.DataType.ToString() ?? string.Empty},{isbn},{processBook?.details?.title ?? string.Empty},{processBook?.details?.subtitle ?? string.Empty},{authors},{processBook?.details?.number_of_pages.ToString() ?? string.Empty},{processBook?.details?.publish_date ?? string.Empty}";

                output.AppendLine(newLine);
            }

            return output.ToString();
        }
    }
}
