# XForge.Scheduling — Manual Enterprise

> ✅ **Estável:** v0.4.0

---

## 10 — Uso Intermediário

### Handlers com dependências

`csharp
public class OrderReportHandler : IJobHandler<ReportContext>
{
    private readonly IReadRepository<Order> _repository;
    private readonly IPdfGenerator _pdfGenerator;
    private readonly ILogger<OrderReportHandler> _logger;

    public OrderReportHandler(
        IReadRepository<Order> repository,
        IPdfGenerator pdfGenerator,
        ILogger<OrderReportHandler> logger)
    {
        _repository = repository;
        _pdfGenerator = pdfGenerator;
        _logger = logger;
    }

    public async Task HandleAsync(ReportContext context, CancellationToken ct)
    {
        _logger.LogInformation("Gerando relatório para {Date}", context.ReportDate);

        var spec = new OrdersByDateSpec(context.ReportDate);
        var orders = await _repository.FindAsync(spec, ct);

        await _pdfGenerator.GenerateAsync(orders, context.Format, ct);
    }
}
`

### Registro de handlers no InMemoryJobScheduler

`csharp
var scheduler = serviceProvider.GetRequiredService<IJobScheduler>() as InMemoryJobScheduler;

scheduler.RegisterHandler("order-report", async ct =>
{
    using var scope = scopeFactory.CreateScope();
    var handler = scope.ServiceProvider.GetRequiredService<OrderReportHandler>();
    await handler.HandleAsync(new ReportContext { ReportDate = DateTime.Today }, ct);
});
`

### Múltiplos jobs

`csharp
await scheduler.ScheduleAsync(new JobSchedule(
    "cleanup", "Limpeza", "0 2 * * *", typeof(CleanupHandler), DateTimeOffset.UtcNow));

await scheduler.ScheduleAsync(new JobSchedule(
    "report", "Relatório", "0 8 * * MON-FRI", typeof(ReportHandler), DateTimeOffset.UtcNow));

await scheduler.ScheduleAsync(new JobSchedule(
    "health-check", "Health Check", "*/5 * * * *", typeof(HealthCheckHandler), DateTimeOffset.UtcNow));
`

---

**Navegação:** ← [Uso Básico](./basic-usage.md) | → [Uso Avançado](./advanced-usage.md)
