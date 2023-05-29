using BPLogix.BooksCvsGenerator.Infrastructure.Abstracts;
using Microsoft.Extensions.Caching.Memory;

namespace BPLogix.BooksCvsGenerator.Infrastructure.Providers
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _cache;

        public CacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool TrySetValue<T>(string key, T value, TimeSpan time = default)
        {
            var result = _cache.Set(key, value, time);
            return result != null;
        }

        public async Task<T> TryGetValue<T>(string key)
        {
            if (_cache.TryGetValue(key, out T value))
            {
                return value;
            }

            return default;
        }
    }

}
