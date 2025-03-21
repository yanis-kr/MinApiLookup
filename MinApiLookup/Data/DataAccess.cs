using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using MinApiLookup.Features.Countries.Models;
using MinApiLookup.Features.Products.Models;

namespace MinApiLookup.Data;

public static class DataAccess
{
    public static async Task SeedInitalData(this WebApplication app, string connectionString)
    {
        using var conn = new SqlConnection(connectionString);
        var sqlFilePath = Path.Combine(app.Environment.ContentRootPath, "Data", "SeedData.sql");
        var sqlScript = await File.ReadAllTextAsync(sqlFilePath);
        await conn.OpenAsync();
        await conn.ExecuteAsync(sqlScript);
    }

    public static async Task PushDataToCache(IMemoryCache cache, string connectionString)
    {
        //populate cache
        using var conn = new SqlConnection(connectionString);
        conn.Open();
        // Code_L NVARCHAR(50),
        // Code_T NVARCHAR(50),
        // Category NVARCHAR(50),
        // AccountType NVARCHAR(50)
        var products = await conn.QueryAsync<Product>(
            "SELECT Code_L AS codeL, Code_T AS codeT, Category AS category, AccountType AS type FROM Products");

        var countries = await conn.QueryAsync<Country>(
            "SELECT code, name FROM Countries");

        cache.Set("Products", products.ToList());
        cache.Set("Countries", countries.ToList());
    }
}
