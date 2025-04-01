using Microsoft.Extensions.Caching.Memory;
using Re_Backend.Common.Attributes;


namespace Re_Backend.Common.Cache
{
    [Injectable(IsSingleton = true)]
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T value);
            return await Task.FromResult(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var options = new MemoryCacheEntryOptions();
            if (expiry.HasValue)
            {
                options.SetAbsoluteExpiration(expiry.Value);
            }
            _memoryCache.Set(key, value, options);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            await Task.CompletedTask;
        }
    }
}
