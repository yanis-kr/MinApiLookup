using Microsoft.Extensions.Caching.Memory;

namespace MinApiLookup.Features.ProductAccounts;

public class ProductAccountCacheService(IProductAccountRepository repo, IMemoryCache cache, IConfiguration configuration)
{
    private readonly MemoryCacheEntryOptions _cacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(configuration.GetValue("CacheSettings:EntryExpirationHours", 2)),
        Size = 1 // each cached entry counts as 1 towards the size limit
    };

    public async Task<IEnumerable<ProductAccount>> GetCachedAsync(string? codeL, string? codeT, string? accountNumber, int maxRecords)
    {
        var cacheKey = $"PA-{codeL}-{codeT}-{accountNumber}-{maxRecords}";

        if (!cache.TryGetValue(cacheKey, out IEnumerable<ProductAccount>? cachedResult))
        {
            cachedResult = await repo.GetProductAccountsAsync(codeL, codeT, accountNumber, maxRecords);

            cache.Set(cacheKey, cachedResult, _cacheOptions);
        }

        return cachedResult!;
    }
}

