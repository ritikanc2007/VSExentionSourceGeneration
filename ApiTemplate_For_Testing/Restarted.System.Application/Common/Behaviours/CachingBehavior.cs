using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Restarted.System.Application.Common.Behaviours.Caching;
using Restarted.System.Contracts.Interfaces.Services;

namespace Restarted.System.Application.Common.Behaviours;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
{
    //private readonly ICache cache;
    private readonly ICacheService _cache;
    private readonly ILogger<TRequest> log;

    public CachingBehavior(ICacheService cache, ILogger<TRequest> log)
    {
        this._cache = cache;
        this.log = log;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        var attributes = request.GetType().GetCustomAttributes(false);

        CacheAttribute cachingAttribute = null;
        CacheInvalidationAttribute invalidationAttribute = null;
        foreach (Attribute attr in attributes)
        {
            if (attr.GetType() == typeof(CacheAttribute))
            {
                cachingAttribute = (CacheAttribute)attr;
                break;
            }
            else if (attr.GetType() == typeof(CacheInvalidationAttribute))
            {
                invalidationAttribute =(CacheInvalidationAttribute)attr;
                break;
            }

        }



        if (cachingAttribute != null)
        {

            if (string.IsNullOrEmpty(cachingAttribute.Key))
            {
                throw new ArgumentNullException(nameof(cachingAttribute.Key));
            }


            var data = await _cache.GetAsync<TResponse>(cachingAttribute.Key, cancellationToken);

            if (data != null)
            {
                log.LogDebug($"Returned value from cache for {typeof(TRequest).Name} with key {cachingAttribute.Key}");
                return data;
            }
            else
            {
                var response = await next();

                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12));
                await _cache.SetAsync(cachingAttribute.Key, response, options, cancellationToken);
                log.LogDebug($"Added to cache for  {typeof(TRequest).Name} with key {cachingAttribute.Key}");
                return response;
            }
        }
        else if (invalidationAttribute != null)
        {
            if (string.IsNullOrEmpty(invalidationAttribute.Key))
            {
                throw new ArgumentNullException(nameof(invalidationAttribute.Key));
            }
            log.LogDebug($"Removed cache for  {typeof(TRequest).Name} with key {invalidationAttribute.Key}");

            await _cache.RemoveAsync(invalidationAttribute.Key);
        }
        return await next();
    }


}
