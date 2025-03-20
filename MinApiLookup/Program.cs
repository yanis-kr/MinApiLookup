using FastEndpoints;
using MinApiLookup.Common;
using MinApiLookup.Features.Countries;
using MinApiLookup.Features.Products;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddScoped<IRepository<Product>>(_ => new Repository<Product>(connectionString, "Products"));
builder.Services.AddScoped<IRepository<Country>>(_ => new Repository<Country>(connectionString, "Countries"));

// FastEndpoints middleware
builder.Services.AddFastEndpoints();

var app = builder.Build();

app.UseFastEndpoints();

app.Run();
