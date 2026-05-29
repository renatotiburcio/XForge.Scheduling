# XForge.Scheduling — Manual Oficial

<p align="center">
  <img src="./icon.png" alt="XForge.Scheduling" width="128" height="128" />
</p>

<p align="center">
  <strong>Agendamento de jobs em background para .NET com expressões cron</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/nuget/v/XForge.Scheduling.svg" alt="NuGet" />
  <img src="https://img.shields.io/badge/version-0.4.0-blue" alt="Version" />
  <img src="https://img.shields.io/badge/status-Published-green" alt="Status" />
  <img src="https://img.shields.io/badge/license-MIT-blue" alt="License" />
  <img src="https://img.shields.io/badge/.NET-8.0%20%7C%209.0%20%7C%2010.0-purple" alt=".NET" />
</p>

<p align="center">
  <a href="https://www.nuget.org/packages/XForge.Scheduling/">NuGet</a> ·
  <a href="https://github.com/renatotiburcio/XForge.Scheduling">GitHub</a>
</p>

---

> ✅ **Release Estável:** v0.4.0 — APIs seguem Semantic Versioning.

---

## Sumário

| # | Capítulo | Arquivo |
|---|----------|---------|
| 01–04 | Capa, Introdução, Status, Features | [README.md](README.md) |
| 05 | Instalação | [installation.md](installation.md) |
| 06 | Quick Start | [quick-start.md](quick-start.md) |
| 07 | Configuração | [configuration.md](configuration.md) |
| 08 | Arquitetura | [architecture.md](architecture.md) |
| 09 | Uso Básico | [basic-usage.md](basic-usage.md) |
| 10 | Uso Intermediário | [intermediate-usage.md](intermediate-usage.md) |
| 11 | Uso Avançado | [advanced-usage.md](advanced-usage.md) |
| 12 | Boas Práticas Enterprise | [enterprise-best-practices.md](enterprise-best-practices.md) |
| 13 | Exemplos de Integração | [integration-examples.md](integration-examples.md) |
| 14 | Testing | [testing.md](testing.md) |
| 15 | Performance | [performance.md](performance.md) |
| 16 | Troubleshooting | [troubleshooting.md](troubleshooting.md) |
| 17 | FAQ | [faq.md](faq.md) |
| 18 | Roadmap | [roadmap.md](roadmap.md) |
| 19 | Changelog | [changelog.md](changelog.md) |
| 20 | Referência da API | [api-reference.md](api-reference.md) |
| 21 | Comparação com Concorrentes | [package-comparison.md](package-comparison.md) |
| 22 | Guia de Migração | [migration-guide.md](migration-guide.md) |
| 23 | Contribuindo | [contributing.md](contributing.md) |
| 24 | Licença | [license.md](license.md) |
| 25 | Notas Finais | [final-notes.md](final-notes.md) |
| 26 | Extensibilidade | [extensibility.md](extensibility.md) |
| 27 | Expressões Cron | [cron-expressions.md](cron-expressions.md) |
| 28 | Compatibilidade Multi-TFM | [multi-tfm-compatibility.md](multi-tfm-compatibility.md) |
| 29 | Segurança | [security.md](security.md) |
| 30 | Suporte & Comunidade | [support-community.md](support-community.md) |

---

## 01 — Capa

| Campo | Valor |
|-------|-------|
| **Nome** | XForge.Scheduling |
| **Versão** | 0.4.0 |
| **Status** | Published |
| **Última atualização** | 2026-05-29 |
| **Licença** | MIT |
| **Repositório** | [github.com/renatotiburcio/XForge.Scheduling](https://github.com/renatotiburcio/XForge.Scheduling) |

---

## 02 — Introdução

### O que é

XForge.Scheduling é uma biblioteca .NET para agendamento de jobs em background. Ela fornece abstrações para `IJobScheduler` e `IJobHandler<T>` com suporte a expressões cron, execução imediata e gerenciamento de jobs.

```csharp
await scheduler.ScheduleAsync(new JobSchedule(
    "cleanup", "Limpeza Diária", "0 2 * * *", typeof(CleanupHandler), DateTimeOffset.UtcNow));
```

### Por que existe

- **Hangfire** — Requer infraestrutura externa (Redis/SQL Server), complexo para cenários simples.
- **Quartz.NET** — Configuração extensa, curva de aprendizado alta.
- **Implementações manuais** — Timer-based jobs sem persistência ou cron support.

XForge.Scheduling oferece uma API simples com implementação in-memory para desenvolvimento e abstrações para produção.

### Diferenciais Técnicos

| Característica | XForge.Scheduling | Hangfire | Quartz.NET |
|---|:---:|:---:|:---:|
| Zero configuração | ✅ | ❌ | ❌ |
| In-memory implementation | ✅ | ❌ | ❌ |
| Cron expressions | ✅ | ✅ | ✅ |
| IJobHandler tipado | ✅ | ❌ | ✅ |
| JobResult com status | ✅ | ❌ | ❌ |
| MIT license | ✅ | ❌ | ✅ |

---

## 03 — Status do Pacote

### Fase Atual: Published

### Recursos Estáveis

| Recurso | Descrição |
|---------|-----------|
| **IJobScheduler** | Interface de agendamento com Schedule, Unschedule, GetAll, Execute |
| **IJobHandler\<T\>** | Handler tipado para execução de jobs |
| **JobSchedule** | Record com Id, Name, CronExpression, HandlerType, CreatedUtc |
| **JobResult** | Record com Success, ErrorMessage, ExecutedUtc |
| **JobStatus** | Enum: Scheduled, Running, Completed, Failed |
| **InMemoryJobScheduler** | Implementação in-memory para dev/test |

### Matriz de Compatibilidade

| Target Framework | Status | Observação |
|------------------|--------|------------|
| `net8.0` | ✅ Suportado | LTS até novembro de 2026 |
| `net9.0` | ✅ Suportado | STS até novembro de 2027 |
| `net10.0` | ✅ Suportado | LTS até novembro de 2028 |

---

## 04 — Features

### 4.1 — Job Scheduling com Cron

```csharp
var schedule = new JobSchedule(
    Id: "daily-report",
    Name: "Relatório Diário",
    CronExpression: "0 8 * * MON-FRI",
    HandlerType: typeof(ReportHandler),
    CreatedUtc: DateTimeOffset.UtcNow);

await scheduler.ScheduleAsync(schedule);
```

### 4.2 — Job Execution Imediata

```csharp
var result = await scheduler.ExecuteAsync("daily-report");
if (result.Success)
    Console.WriteLine("Job executado com sucesso");
```

### 4.3 — Typed Job Handlers

```csharp
public class ReportHandler : IJobHandler<ReportContext>
{
    public async Task HandleAsync(ReportContext context, CancellationToken ct)
    {
        await GenerateReportAsync(context.ReportDate, ct);
    }
}
```

### 4.4 — Job Management

```csharp
// Listar todos os jobs
var jobs = await scheduler.GetAllAsync();

// Remover job
await scheduler.UnscheduleAsync("daily-report");
```

---

<div align="center">

**Próximo:** [Instalação →](installation.md)

</div>
