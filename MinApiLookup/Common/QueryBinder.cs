using MinApiLookup.Attributes;
using System.Reflection;

namespace MinApiLookup.Common;

public static class QueryBinder
{
    public static T BindQueryParams<T>(this HttpContext ctx) where T : class, new()
    {
        var query = ctx.Request.Query;
        var obj = new T();
        foreach (var prop in typeof(T).GetProperties())
        {
            var attr = prop.GetCustomAttribute<QueryParamAttribute>();
            if (attr != null && ctx.Request.Query.TryGetValue(attr.Name, out var val))
            {
                prop.SetValue(obj, val.ToString());
            }
        }
        return obj;
    }
}

