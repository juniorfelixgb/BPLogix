namespace BPLogix.BooksCvsGenerator.Infrastructure.Abstracts
{
    public interface ICacheProvider
    {
        bool TrySetValue<T>(string key, T value, TimeSpan time = default);
        Task<T> TryGetValue<T>(string key);
    }
}
