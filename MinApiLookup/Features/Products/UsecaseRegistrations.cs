using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MinApiLookup.Common;
using MinApiLookup.Features.Products.Models;

namespace MinApiLookup.Features.Products;

public class UsecaseRegistrations : IStartupRegistrations
{
    public void AddUsecaseServices(IServiceCollection services)
    {
        //TODO
    }
}