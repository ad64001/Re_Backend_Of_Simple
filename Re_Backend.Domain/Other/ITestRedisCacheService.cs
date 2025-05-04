using Re_Backend.Common.Attributes;
using Re_Backend.Common.Cache;

namespace Re_Backend.Domain.Other
{
    public interface ITestRedisCacheService
    {
        Task<string> UseCacheAsync();
    }

    [Injectable(IsSingleton = true)]
    public class TestRedisCacheService : ITestRedisCacheService
    {
        private readonly ICacheService _cacheService;

        public TestRedisCacheService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<string> UseCacheAsync()
        {
            await _cacheService.SetAsync("key", "value");
            var value = await _cacheService.GetAsync<string>("key");
            Console.WriteLine($"Cached value: {value}");
            return value.GetType().ToString();
        }
    }
}
