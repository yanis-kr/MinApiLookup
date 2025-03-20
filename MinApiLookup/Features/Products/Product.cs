using MinApiLookup.Attributes;

namespace MinApiLookup.Features.Products;

public class Product
{
    [DbColumn("Code_L"), QueryParam("codeL")]
    public string? Code1 { get; set; }

    [DbColumn("Code_T"), QueryParam("codeT")]
    public string? Code2 { get; set; }

    [DbColumn("category"), QueryParam("category")]
    public string? Category { get; set; }

    [DbColumn("AccountType"),QueryParam("accountType")]
    public string? AccountType { get; set; }
}
