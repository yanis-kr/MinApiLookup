using Dapper;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using MinApiLookup.Common;
using MinApiLookup.Features.Countries;
using MinApiLookup.Features.Products.Models;

var builder = WebApplication.CreateBuilder(args);

// Add your services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!
    .Replace("%CONTENTROOTPATH%", builder.Environment.ContentRootPath);
builder.Services.AddScoped<IRepository<Product>>(_ => new Repository<Product>(connectionString, "Products"));
builder.Services.AddScoped<IRepository<Country>>(_ => new Repository<Country>(connectionString, "Countries"));

builder.Services.AddMemoryCache();

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument();

var app = builder.Build();

using (var conn = new SqlConnection(connectionString))
{
    var sqlFilePath = Path.Combine(builder.Environment.ContentRootPath, "Data", "SeedData.sql");
    var sqlScript = await File.ReadAllTextAsync(sqlFilePath);

    await conn.OpenAsync();
    await conn.ExecuteAsync(sqlScript);
}

//populate cache
using (var conn = new SqlConnection(connectionString))
{
    conn.Open();
    var products = await conn.QueryAsync<Product>(
        "SELECT Code_L AS codeL, Code_T AS codeT, Category AS category, AccountType AS type FROM Products");

    var countries = await conn.QueryAsync<Country>(
        "SELECT code, name FROM Countries");

    var cache = app.Services.GetRequiredService<IMemoryCache>();

    cache.Set("Products", products.ToList());
    cache.Set("Countries", countries.ToList());
}

app.UseFastEndpoints()
   .UseSwaggerGen(); 

app.Run();
