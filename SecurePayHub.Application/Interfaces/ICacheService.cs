using System;
using System.Threading.Tasks;

namespace SecurePayHub.Application.Interfaces
{
    public interface ICacheService
    {
        Task<string?> GetCacheAsync(string key);
        Task SetCacheAsync(string key, string value, TimeSpan expirationTime);
        Task RemoveCacheAsync(string key);
    }
}