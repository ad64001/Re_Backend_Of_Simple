using Autofac;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Re_Backend.Common.Cache;

namespace Re_Backend.Infrastructure.CacheConfig
{
    public static class CacheConfiguration
    {
        public static void ConfigureCache(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            var redisConfig = configuration.GetSection("Redis");
            bool enableRedis = redisConfig.GetValue<bool>("Enable");

            if (enableRedis)
            {
                // 注册 Redis 缓存
                containerBuilder.Register(c =>
                {
                    var options = new RedisCacheOptions
                    {
                        Configuration = redisConfig["ConnectionString"],
                        InstanceName = redisConfig["InstanceName"]
                    };
                    return new RedisCache(options);
                }).As<IDistributedCache>().SingleInstance();

                containerBuilder.RegisterType<RedisCacheService>().As<ICacheService>().SingleInstance();
            }
            else
            {
                // 注册内存缓存
                containerBuilder.Register(c => new MemoryCache(Options.Create(new MemoryCacheOptions())))
                                .As<IMemoryCache>()
                                .SingleInstance();
                containerBuilder.RegisterType<MemoryCacheService>().As<ICacheService>().SingleInstance();
            }
        }
    }
}
