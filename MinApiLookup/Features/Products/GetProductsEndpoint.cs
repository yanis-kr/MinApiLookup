using FastEndpoints;
using Microsoft.Extensions.Caching.Memory;
using MinApiLookup.Extensions;
using MinApiLookup.Features.Products.Models;

namespace MinApiLookup.Features.Products;

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
        if (products == null)
            throw new Exception("Products cache is empty");

        var filtered = QueryFilterBuilder<Product>.ApplyFilters(products.AsQueryable(), req);

        return SendAsync(filtered, cancellation: ct);
    }
}
