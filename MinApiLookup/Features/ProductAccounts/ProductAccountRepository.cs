using Dapper;
using System.Data.SQLite;

namespace MinApiLookup.Features.ProductAccounts;

public interface IProductAccountRepository
{
    Task<IEnumerable<ProductAccount>> GetProductAccountsAsync(string? codeL, string? codeT, string? accountNumber, int maxRecords);
}

public class ProductAccountRepository(string connectionString) : IProductAccountRepository
{
    public async Task<IEnumerable<ProductAccount>> GetProductAccountsAsync(string? codeL, string? codeT, string? accountNumber, int maxRecords)
    {
        using var conn = new SQLiteConnection(connectionString);
        // T-SQL
        //var sql = @"
        //    SELECT TOP(@MaxRecords) Code_L AS CodeL, Code_T AS CodeT, AccountNumber 
        //    FROM ProductAccounts
        //    WHERE (@CodeL IS NULL OR Code_L = @CodeL)
        //      AND (@CodeT IS NULL OR Code_T = @CodeT)
        //      AND (@AccountNumber IS NULL OR AccountNumber = @AccountNumber)";

        // SQLite
        var sql = @"
            SELECT Code_L AS CodeL,
                   Code_T AS CodeT, 
                   AccountNumber 
            FROM ProductAccounts
            WHERE (@CodeL IS NULL OR Code_L = @CodeL)
              AND (@CodeT IS NULL OR Code_T = @CodeT)
              AND (@AccountNumber IS NULL OR AccountNumber = @AccountNumber)
            LIMIT @MaxRecords";

        return await conn.QueryAsync<ProductAccount>(sql, new
        {
            CodeL = codeL,
            CodeT = codeT,
            AccountNumber = accountNumber,
            MaxRecords = maxRecords
        });
    }
}
