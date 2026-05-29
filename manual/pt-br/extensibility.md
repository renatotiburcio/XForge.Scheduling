# 26 — Extensibilidade

## Implementação custom de IJobScheduler

`csharp
public class RedisJobScheduler : IJobScheduler
{
    private readonly IConnectionMultiplexer _redis;

    public async Task ScheduleAsync(JobSchedule schedule, CancellationToken ct = default)
    {
        var db = _redis.GetDatabase();
        await db.HashSetAsync($"jobs:{schedule.Id}", new[]
        {
            new HashEntry("name", schedule.Name),
            new HashEntry("cron", schedule.CronExpression),
            new HashEntry("handler", schedule.HandlerType.AssemblyQualifiedName)
        });
    }
    // ...
}
`

---

**Navegação:** ← [Notas Finais](./final-notes.md) | → [Expressões Cron](./cron-expressions.md)
