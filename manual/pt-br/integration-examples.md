> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 13 — Exemplos de Integração

## ASP.NET Core com Background Service

`csharp
public class SchedulingBackgroundService : BackgroundService
{
    private readonly IJobScheduler _scheduler;

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await _scheduler.ScheduleAsync(new JobSchedule(
            "health", "Health Check", "*/5 * * * *",
            typeof(HealthCheckHandler), DateTimeOffset.UtcNow), ct);

        while (!ct.IsCancellationRequested)
        {
            var jobs = await _scheduler.GetAllAsync(ct);
            foreach (var job in jobs)
            {
                await _scheduler.ExecuteAsync(job.Id, ct);
            }
            await Task.Delay(TimeSpan.FromMinutes(1), ct);
        }
    }
}
`

## Worker Service

`csharp
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddXForgeScheduling();
builder.Services.AddHostedService<SchedulingBackgroundService>();

var host = builder.Build();
await host.RunAsync();
`

## Integração com XForge.MediatR

`csharp
public class MediatrJobHandler : IJobHandler<MediatrJobContext>
{
    private readonly IXForgeMediator _mediator;

    public async Task HandleAsync(MediatrJobContext context, CancellationToken ct)
    {
        await _mediator.Send(context.Command, ct);
    }
}
`

---

**Navegação:** ← [Boas Práticas Enterprise](./enterprise-best-practices.md) | → [Testing](./testing.md)
