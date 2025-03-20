using MinApiLookup.Attributes;

namespace MinApiLookup.Features.Countries;

public class Country
{
    [DbColumn("code")]
    [QueryParam("code")]
    public string? Code { get; set; }

    [DbColumn("name")]
    [QueryParam("name")]
    public string? Name { get; set; }
}
