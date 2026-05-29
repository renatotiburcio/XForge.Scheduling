using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace XForge.Scheduling.Extensions;

/// <summary>
/// Extension methods for registering XForge.Scheduling services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds XForge.Scheduling services to the service collection.
    /// Registers <see cref="IJobScheduler"/> as <see cref="InMemoryJobScheduler"/>.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddXForgeScheduling(this IServiceCollection services)
    {
        services.TryAddSingleton<IJobScheduler, InMemoryJobScheduler>();

        return services;
    }
}
