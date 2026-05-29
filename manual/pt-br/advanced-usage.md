> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 11 — Uso Avançado

## Implementação custom de IJobScheduler

Para produção, implemente IJobScheduler com persistência:

`csharp
public class SqlJobScheduler : IJobScheduler
{
    private readonly AppDbContext _context;

    public SqlJobScheduler(AppDbContext context) => _context = context;

    public async Task ScheduleAsync(JobSchedule schedule, CancellationToken ct = default)
    {
        _context.JobSchedules.Add(new JobScheduleEntity
        {
            Id = schedule.Id,
            Name = schedule.Name,
            CronExpression = schedule.CronExpression,
            HandlerType = schedule.HandlerType.AssemblyQualifiedName!,
            CreatedUtc = schedule.CreatedUtc
        });
        await _context.SaveChangesAsync(ct);
    }

    public async Task UnscheduleAsync(string jobId, CancellationToken ct = default)
    {
        var entity = await _context.JobSchedules.FindAsync(new object[] { jobId }, ct);
        if (entity is not null)
        {
            _context.JobSchedules.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<IReadOnlyList<JobSchedule>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.JobSchedules
            .Select(e => new JobSchedule(e.Id, e.Name, e.CronExpression,
                Type.GetType(e.HandlerType)!, e.CreatedUtc))
            .ToListAsync(ct);
    }

    public async Task<JobResult> ExecuteAsync(string jobId, CancellationToken ct = default)
    {
        // Resolver handler do DI e executar
        // ...
    }
}
`

## Job Chaining

`csharp
public class ChainedJobHandler : IJobHandler<object>
{
    private readonly IJobScheduler _scheduler;

    public async Task HandleAsync(object context, CancellationToken ct)
    {
        // Após completar, agenda o próximo job
        await _scheduler.ExecuteAsync("next-job-id", ct);
    }
}
`

---

**Navegação:** ← [Uso Intermediário](./intermediate-usage.md) | → [Boas Práticas Enterprise](./enterprise-best-practices.md)
