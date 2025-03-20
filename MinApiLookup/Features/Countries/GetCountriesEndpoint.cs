namespace MinApiLookup.Features.Countries;

using FastEndpoints;
using MinApiLookup.Common;

public class GetCountriesEndpoint : Endpoint<Country, IEnumerable<Country>>
{
    private readonly IRepository<Country> _repository;

    public GetCountriesEndpoint(IRepository<Country> repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Get("/countries");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Country req, CancellationToken ct)
    {
        var results = await _repository.GetFilteredAsync(req);
        await SendAsync(results, cancellation: ct);
    }
}

