using BPLogix.BooksCvsGenerator.Domain.Shared.Enums;

namespace BPLogix.BooksCvsGenerator.Domain.Responses
{

    public class ProcessBookResponse
    {
        public string bib_key { get; set; } = "";
        public Details details { get; set; }
        public DataRetrievalType DataType { get; set; } = DataRetrievalType.None;
    }

    public class Details
    {
        public int number_of_pages { get; set; } = 0;
        public string subtitle { get; set; } = "";
        public Author[] authors { get; set; } = Array.Empty<Author>();
        public string title { get; set; } = "";
        public string publish_date { get; set; } = "";
    }

    public class Author
    {
        public string name { get; set; } = "";
    }
}
