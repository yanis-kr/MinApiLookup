using FastEndpoints;

namespace MinApiLookup.Features.ProductAccounts;

public class GetProductAccountRequest
{
    [QueryParam] public string? CodeL { get; set; }
    [QueryParam] public string? CodeT { get; set; }
    [QueryParam] public string? AccountNumber { get; set; }
}

public class GetProductAccountEndpoint(ProductAccountCacheService cacheService, IConfiguration configuration)
    : Endpoint<GetProductAccountRequest, IEnumerable<ProductAccount>>
{
    private readonly int _maxRecords = configuration.GetValue("ProductAccountSettings:MaxRecords", 100);

    public override void Configure()
    {
        Get("/productaccount");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductAccountRequest req, CancellationToken ct)
    {
        var result = await cacheService.GetCachedAsync(req.CodeL, req.CodeT, req.AccountNumber, _maxRecords);
        await SendAsync(result, cancellation: ct);
    }
}

