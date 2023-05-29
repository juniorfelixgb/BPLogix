namespace BPLogix.BooksCvsGenerator.Domain.Exceptions
{
    public class BookNotProcessedException : Exception
    {
        public BookNotProcessedException(string message) : base(message) { }
    }
}
