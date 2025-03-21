using FastEndpoints;
using Microsoft.Extensions.Caching.Memory;
using MinApiLookup.Extensions;
using MinApiLookup.Features.Countries.Models;

namespace MinApiLookup.Features.Countries;

public class GetCountriesEndpoint : Endpoint<Country, IEnumerable<Country>>
{
    private readonly IMemoryCache _cache;

    public GetCountriesEndpoint(IMemoryCache cache)
    {
        _cache = cache;
    }

    public override void Configure()
    {
        Get("/countries");
        AllowAnonymous();
    }

    public override Task HandleAsync(Country req, CancellationToken ct)
    {
        var countries = _cache.Get<List<Country>>("Countries");
        if (countries == null)
            throw new Exception("Countries cache is empty");

        var filtered = QueryFilterBuilder<Country>.ApplyFilters(countries.AsQueryable(), req);

        return SendAsync(filtered, cancellation: ct);
    }
}

