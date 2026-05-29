using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace XForge.Scheduling.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddXForgeScheduling_RegistersInMemoryJobScheduler()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddXForgeScheduling();
        var provider = services.BuildServiceProvider();

        var scheduler = provider.GetService<IJobScheduler>();

        scheduler.Should().NotBeNull();
        scheduler.Should().BeOfType<InMemoryJobScheduler>();
    }

    [Fact]
    public void AddXForgeScheduling_DoesNotDuplicate_Registrations()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddXForgeScheduling();
        services.AddXForgeScheduling();

        var provider = services.BuildServiceProvider();
        var schedulers = provider.GetServices<IJobScheduler>();

        schedulers.Should().HaveCount(1);
    }

    [Fact]
    public void AddXForgeScheduling_Returns_ServiceCollection()
    {
        var services = new ServiceCollection();

        var result = services.AddXForgeScheduling();

        result.Should().BeSameAs(services);
    }

    [Fact]
    public void AddXForgeScheduling_RegistersSingletonLifetime()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddXForgeScheduling();
        var provider = services.BuildServiceProvider();

        var scheduler1 = provider.GetService<IJobScheduler>();
        var scheduler2 = provider.GetService<IJobScheduler>();

        scheduler1.Should().BeSameAs(scheduler2);
    }
}
