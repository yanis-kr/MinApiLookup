namespace MinApiLookup.Features.Products;

using FastEndpoints;
using Microsoft.Extensions.Caching.Memory;
using MinApiLookup.Features.Products.Models;

public class GetProductsEndpoint : Endpoint<Product, IEnumerable<Product>>
{
    private readonly IMemoryCache _cache;

    public GetProductsEndpoint(IMemoryCache cache)
    {
        _cache = cache;
    }

    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override Task HandleAsync(Product req, CancellationToken ct)
    {
        var products = _cache.Get<List<Product>>("Products");

        var filtered = products.AsQueryable();

        if (!string.IsNullOrEmpty(req.Code1))
            filtered = filtered.Where(p => p.Code1 == req.Code1);

        if (!string.IsNullOrEmpty(req.Code2))
            filtered = filtered.Where(p => p.Code2 == req.Code2);

        if (!string.IsNullOrEmpty(req.Category))
            filtered = filtered.Where(p => p.Category == req.Category);

        if (!string.IsNullOrEmpty(req.Type))
            filtered = filtered.Where(p => p.Type == req.Type);

        return SendAsync(filtered, cancellation: ct);
    }

    private readonly IMemoryCache _cache;
    public GetProductsEndpoint(IMemoryCache cache) => _cache = cache;
}
