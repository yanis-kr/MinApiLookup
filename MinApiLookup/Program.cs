using FastEndpoints;
using MinApiLookup.Common;
using MinApiLookup.Features.Countries;
using MinApiLookup.Features.Products;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    .Replace("%CONTENTROOTPATH%", builder.Environment.ContentRootPath);

builder.Services.AddScoped<IRepository<Product>>(_ => new Repository<Product>(connectionString, "Products"));
builder.Services.AddScoped<IRepository<Country>>(_ => new Repository<Country>(connectionString, "Countries"));

builder.Services.AddFastEndpoints();

var app = builder.Build();

app.UseFastEndpoints();

// 🚀 Minimal strongly-typed API Endpoints:
app.MapGet("/products", async (HttpContext ctx, IRepository<Product> repo) =>
{
    var filter = ctx.BindQueryParams<Product>();
    var products = await repo.GetFilteredAsync(filter);
    return Results.Ok(products);
})
.WithName("GetProducts");

app.MapGet("/countries", async (HttpContext ctx, IRepository<Country> repo) =>
{
    var filter = ctx.BindQueryParams<Country>();
    var countries = await repo.GetFilteredAsync(filter);
    return Results.Ok(countries);
});

app.Run();
