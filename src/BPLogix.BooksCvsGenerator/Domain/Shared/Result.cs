namespace BPLogix.BooksCvsGenerator.Domain.Shared
{
    public class Result<T>
    {
        public ResultDetails ResultDetails { get; private set; }
        public string Errors { get; private set; }
        public bool IsSuccess { get; private set; }
        public T Data { get; private set; }
        public Result(T data, bool isSuccess, string errors, ResultDetails resultDetails = default)
        {
            Data = data;
            IsSuccess = isSuccess;
            Errors = errors;
            ResultDetails = resultDetails;
        }
    }
}
