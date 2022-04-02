using DefaultAPI.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Services
{
    public sealed class RedisService : IRedisService
    {
        private readonly IDistributedCache _cache;

        public RedisService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task AddDataString(string redisKey, string redisData)
        {
            await _cache.SetStringAsync(redisKey, redisData, SetTimeToExpire());
        }

        public async Task<string> GetDataString(string redisKey)
        {
            var result = await _cache.GetStringAsync(redisKey);
            return result;
        }

        public async Task AddDataObject<T>(string redisKey, T redisData) where T : class
        {
            await _cache.SetStringAsync(redisKey, JsonConvert.SerializeObject(redisData), SetTimeToExpire());
        }

        public async Task<T> GetDataObject<T>(string redisKey) where T : class
        {
            var result = await _cache.GetStringAsync(redisKey);
            return JsonConvert.DeserializeObject<T>(result);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private DistributedCacheEntryOptions SetTimeToExpire()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();
            cacheEntryOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(60));
            return cacheEntryOptions;
        }

    }
}
