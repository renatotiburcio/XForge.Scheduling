# 20 — Referência da API

## IJobScheduler

`csharp
public interface IJobScheduler
{
    Task ScheduleAsync(JobSchedule schedule, CancellationToken ct = default);
    Task UnscheduleAsync(string jobId, CancellationToken ct = default);
    Task<IReadOnlyList<JobSchedule>> GetAllAsync(CancellationToken ct = default);
    Task<JobResult> ExecuteAsync(string jobId, CancellationToken ct = default);
}
`

## IJobHandler<T>

`csharp
public interface IJobHandler<in T>
{
    Task HandleAsync(T context, CancellationToken ct = default);
}
`

## JobSchedule

| Propriedade | Tipo | Descrição |
|-------------|------|-----------|
| Id | string | Identificador único |
| Name | string | Nome legível |
| CronExpression | string | Expressão cron |
| HandlerType | Type | Tipo do handler |
| CreatedUtc | DateTimeOffset | Data de criação |

## JobResult

| Propriedade | Tipo | Descrição |
|-------------|------|-----------|
| Success | ool | Se completou com sucesso |
| ErrorMessage | string? | Mensagem de erro (se falhou) |
| ExecutedUtc | DateTimeOffset | Momento da execução |

## JobStatus

| Valor | Descrição |
|-------|-----------|
| Scheduled | Agendado, aguardando execução |
| Running | Em execução |
| Completed | Concluído com sucesso |
| Failed | Falhou |

---

**Navegação:** ← [Changelog](./changelog.md) | → [Comparação](./package-comparison.md)
