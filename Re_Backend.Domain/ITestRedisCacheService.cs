using Microsoft.Extensions.Caching.Distributed;
using Re_Backend.Common.Attributes;
using Re_Backend.Common.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain
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
