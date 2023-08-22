using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Restarted.System.Contracts.Interfaces.Services;
using Formatting = Newtonsoft.Json.Formatting;

namespace Restarted.System.Infrastructure.Services.CachingService
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IOptions<CacheSettings> cacheSettings;
        private DistributedCacheEntryOptions _cacheOptions;
        private readonly ILogger _logger;

        public CacheService(IDistributedCache cache, IOptions<CacheSettings> cacheSettings)
        {
            _cache = cache;
            this.cacheSettings=cacheSettings;
        }



        public async Task<T> GetAsync<T>(string key, CancellationToken token = default)
        {
            var data = await _cache.GetStringAsync(key, token);

            if (data != null)
                return JsonConvert.DeserializeObject<T>(data);

            return default;
        }

        public Task SetAsync<T>(string key, T value, CancellationToken token = default)
        {
            return this.SetAsync<T>(key, value, null, token);
        }
        public Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken token)
        {
            string data = JsonConvert.SerializeObject(value, Formatting.None,
                                             new JsonSerializerSettings()
                                             {
                                                 ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                                                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                 PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                             });


            return _cache.SetStringAsync(key, data, options, token);
        }
        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            return _cache.RemoveAsync(key, token);
        }



        public Task RemoveByPrefixAsync(string key, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }


    }
}
