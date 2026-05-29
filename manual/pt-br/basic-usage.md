# XForge.Scheduling — Manual Enterprise

> ✅ **Estável:** v0.4.0

---

## 09 — Uso Básico

### Agendando um Job

`csharp
var schedule = new JobSchedule(
    Id: "daily-cleanup",
    Name: "Limpeza Diária",
    CronExpression: "0 2 * * *",
    HandlerType: typeof(CleanupHandler),
    CreatedUtc: DateTimeOffset.UtcNow);

await scheduler.ScheduleAsync(schedule);
`

### Executando um Job imediatamente

`csharp
var result = await scheduler.ExecuteAsync("daily-cleanup");

if (result.Success)
    Console.WriteLine($"Executado em {result.ExecutedUtc}");
else
    Console.WriteLine($"Falha: {result.ErrorMessage}");
`

### Listando Jobs agendados

`csharp
var jobs = await scheduler.GetAllAsync();
foreach (var job in jobs)
{
    Console.WriteLine($"{job.Name} - {job.CronExpression}");
}
`

### Removendo um Job

`csharp
await scheduler.UnscheduleAsync("daily-cleanup");
`

### Criando um IJobHandler

`csharp
public class EmailNotificationHandler : IJobHandler<EmailContext>
{
    private readonly IEmailService _emailService;

    public EmailNotificationHandler(IEmailService emailService)
        => _emailService = emailService;

    public async Task HandleAsync(EmailContext context, CancellationToken ct)
    {
        await _emailService.SendDailyDigestAsync(context.UserId, ct);
    }
}
`

---

**Navegação:** ← [Arquitetura](./architecture.md) | → [Uso Intermediário](./intermediate-usage.md)
