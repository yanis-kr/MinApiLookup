using Microsoft.Extensions.DependencyInjection;
using MinApiLookup.Common;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MinApiLookup.Extensions;


[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register UseCase() for all classes inherited from
    /// IStartupRegistrations to add Use Case specific services to DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterUseCaseServices(
        this IServiceCollection services, Assembly assembly)
    {
        var registrations = assembly.GetTypes()
            .Where(t => typeof(IStartupRegistrations).IsAssignableFrom(t)
                        && t.IsClass
                        && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IStartupRegistrations>();

        foreach (var registration in registrations)
        {
            registration.AddUsecaseServices(services);
        }

        return services;
    }
}
