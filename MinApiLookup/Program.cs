using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Extensions.Caching.Memory;
using MinApiLookup.Data;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!
    .Replace("%CONTENTROOTPATH%", builder.Environment.ContentRootPath);

builder.Services
    .AddMemoryCache()
    .AddFastEndpoints()
    .SwaggerDocument();

var app = builder.Build();

await app.SeedInitalData(connectionString);
var cache = app.Services.GetRequiredService<IMemoryCache>();
await DataAccess.PushDataToCache(cache, connectionString);

app.UseFastEndpoints()
    .UseSwaggerGen()
    .UseStatusCodePages();

app.Run();
