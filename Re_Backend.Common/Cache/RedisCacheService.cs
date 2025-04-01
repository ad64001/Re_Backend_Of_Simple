using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Re_Backend.Common.Attributes;
using System.Text;

namespace Re_Backend.Common.Cache
{
    [Injectable(IsSingleton = true)]
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var byteArray = await _distributedCache.GetAsync(key);
            if (byteArray == null)
            {
                return default(T);
            }
            var value = Deserialize<T>(byteArray);
            return value;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var byteArray = Serialize(value);
            var options = new DistributedCacheEntryOptions();
            if (expiry.HasValue)
            {
                options.SetAbsoluteExpiration(expiry.Value);
            }
            await _distributedCache.SetAsync(key, byteArray, options);
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        private byte[] Serialize<T>(T value)
        {
            var json = JsonConvert.SerializeObject(value);
            return Encoding.UTF8.GetBytes(json);
        }

        private T Deserialize<T>(byte[] byteArray)
        {
            var json = Encoding.UTF8.GetString(byteArray);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
