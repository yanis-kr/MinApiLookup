using Dapper;
using Microsoft.Data.SqlClient;
using MinApiLookup.Attributes;
using System.Reflection;

namespace MinApiLookup.Common;

public interface IRepository<T> where T : class, new()
{
    Task<IEnumerable<T>> GetFilteredAsync(T filter);
}

public class Repository<T>(string connectionString, string tableName) : IRepository<T>
    where T : class, new()
{
    public async Task<IEnumerable<T>> GetFilteredAsync(T filter)
    {
        var props = typeof(T).GetProperties()
            .Where(p => p.GetValue(filter) != null)
            .Select(p => new
            {
                Column = p.GetCustomAttribute<DbColumnAttribute>()?.Name ?? p.Name,
                Value = p.GetValue(filter)
            });

        var query = $"SELECT * FROM {tableName}";
        var parameters = new DynamicParameters();

        if (props.Any())
        {
            var conditions = props.Select((p, idx) =>
            $"{p.Column} = @p{idx}");
            var conditionStr = string.Join(" AND ", conditions);
            query += $" WHERE {conditionStr}";

            var dynamicParams = new DynamicParameters();
            foreach (var (p, idx) in props.Select((p, idx) => (p, idx)))
            {
                dynamicParams.Add($"@p{idx}", p.Value);
            }

            parameters = dynamicParams;
        }

        using var conn = new SqlConnection(connectionString);
        return await conn.QueryAsync<T>(query, parameters);
    }
}
