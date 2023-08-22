using Microsoft.Extensions.Caching.Distributed;

namespace Restarted.System.Contracts.Interfaces.Services
{
    public interface ICacheService
    {



        Task<T?> GetAsync<T>(string key, CancellationToken token = default);

        Task SetAsync<T>(string key, T value, CancellationToken token = default);

        Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default);

        Task RemoveAsync(string key, CancellationToken token = default);

        Task RemoveByPrefixAsync(string key, CancellationToken token = default);

    }
}
