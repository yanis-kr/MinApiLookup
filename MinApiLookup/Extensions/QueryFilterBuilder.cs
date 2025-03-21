using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace MinApiLookup.Extensions;

/// <summary>
/// Provides generic and efficient filtering functionality for IQueryable collections,
/// using LINQ expression trees and compiled delegates.
/// Uses compiled expression delegates to quickly access properties of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of objects being filtered.</typeparam>
public static class QueryFilterBuilder<T>
{
    /// <summary>
    /// Caches compiled delegates to efficiently access property values without reflection.
    /// </summary>
    private static readonly ConcurrentDictionary<string, Func<T, string?>> PropertyAccessors = new();

    /// <summary>
    /// Static constructor that compiles delegates for all string properties of type <typeparamref name="T"/>.
    /// This ensures fast property access during filtering, compiled once at application startup.
    /// </summary>
    static QueryFilterBuilder()
    {
        foreach (var prop in typeof(T).GetProperties().Where(p => p.PropertyType == typeof(string)))
        {
            //Create a parameter x of generic type (T)
            var parameter = Expression.Parameter(typeof(T), "x");

            // accessing property x.PropertyName
            var property = Expression.Property(parameter, prop);

            // Explicitly cast property to string (safe handling)
            var convert = Expression.TypeAs(property, typeof(string));

            // Create a lambda expression: (T x) => x.Property
            var lambda = Expression.Lambda<Func<T, string?>>(convert, parameter);

            // Generate an optimized delegate that can directly access the property
            PropertyAccessors[prop.Name] = lambda.Compile();
        }
    }

    /// <summary>
    /// Applies dynamic filters to the provided IQueryable collection based on non-empty properties of the <paramref name="filter"/> object.
    /// </summary>
    /// <param name="query">The IQueryable collection to filter.</param>
    /// <param name="filter">The filter object whose non-empty properties determine filter conditions.</param>
    /// <returns>The filtered IQueryable collection.</returns>
    public static IQueryable<T> ApplyFilters(IQueryable<T> query, T filter)
    {
        // using tuple deconstruction to loop through the dictionary
        foreach (var (propertyName, accessor) in PropertyAccessors)
        {
            var filterValue = accessor(filter);
            if (!string.IsNullOrEmpty(filterValue))
            {
                query = query.Where(x => accessor(x) == filterValue);
            }
        }

        return query;
    }
}
