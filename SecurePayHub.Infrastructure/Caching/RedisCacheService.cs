using Microsoft.Extensions.Caching.Distributed;
using SecurePayHub.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace SecurePayHub.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string?> GetCacheAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task SetCacheAsync(string key, string value, TimeSpan expirationTime)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            };
            await _cache.SetStringAsync(key, value, options);
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}