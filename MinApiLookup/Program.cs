using Dapper;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Data.SqlClient;
using MinApiLookup.Common;
using MinApiLookup.Features.Countries;
using MinApiLookup.Features.Products;

var builder = WebApplication.CreateBuilder(args);

// Add your services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!
    .Replace("%CONTENTROOTPATH%", builder.Environment.ContentRootPath);
builder.Services.AddScoped<IRepository<Product>>(_ => new Repository<Product>(connectionString, "Products"));
builder.Services.AddScoped<IRepository<Country>>(_ => new Repository<Country>(connectionString, "Countries"));

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

app.UseFastEndpoints()
   .UseSwaggerGen(); 

app.Run();
