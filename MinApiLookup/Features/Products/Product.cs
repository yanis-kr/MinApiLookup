using MinApiLookup.Attributes;

namespace MinApiLookup.Features.Products;

public class Product
{
    [DbColumn("code_1"), QueryParam("code1")]
    public string? Code1 { get; set; }

    [DbColumn("code_2")]
    [QueryParam("code2")]
    public string? Code2 { get; set; }

    [DbColumn("category")]
    [QueryParam("category")]
    public string? Category { get; set; }
}
