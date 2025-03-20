namespace MinApiLookup.Features.Products;

using FastEndpoints;
using MinApiLookup.Common;

public class GetProductsEndpoint : Endpoint<Product, IEnumerable<Product>>
{
    private readonly IRepository<Product> _repository;

    public GetProductsEndpoint(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Product req, CancellationToken ct)
    {
        var results = await _repository.GetFilteredAsync(req);
        await SendAsync(results, cancellation: ct);
    }
}
