# XForge.Scheduling — Official Manual

<p align="center">
  <img src="./icon.png" alt="XForge.Scheduling" width="128" height="128" />
</p>

<p align="center">
  <strong>Background job scheduling for .NET with cron expressions</strong>
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

> ✅ **Stable Release:** v0.4.0 — APIs follow Semantic Versioning.

---

## Table of Contents

| # | Chapter | File |
|---|---------|------|
| 01-04 | Cover, Introduction, Status, Features | [README.md](README.md) |
| 05 | Installation | [installation.md](installation.md) |
| 06 | Quick Start | [quick-start.md](quick-start.md) |
| 07 | Configuration | [configuration.md](configuration.md) |
| 08 | Architecture | [architecture.md](architecture.md) |
| 09 | Basic Usage | [basic-usage.md](basic-usage.md) |
| 10 | Intermediate Usage | [intermediate-usage.md](intermediate-usage.md) |
| 11 | Advanced Usage | [advanced-usage.md](advanced-usage.md) |
| 12 | Enterprise Best Practices | [enterprise-best-practices.md](enterprise-best-practices.md) |
| 13 | Integration Examples | [integration-examples.md](integration-examples.md) |
| 14 | Testing | [testing.md](testing.md) |
| 15 | Performance | [performance.md](performance.md) |
| 16 | Troubleshooting | [troubleshooting.md](troubleshooting.md) |
| 17 | FAQ | [faq.md](faq.md) |
| 18 | Roadmap | [roadmap.md](roadmap.md) |
| 19 | Changelog | [changelog.md](changelog.md) |
| 20 | API Reference | [api-reference.md](api-reference.md) |
| 21 | Competitor Comparison | [package-comparison.md](package-comparison.md) |
| 22 | Migration Guide | [migration-guide.md](migration-guide.md) |
| 23 | Contributing | [contributing.md](contributing.md) |
| 24 | License | [license.md](license.md) |
| 25 | Final Notes | [final-notes.md](final-notes.md) |
| 26 | Extensibility | [extensibility.md](extensibility.md) |
| 27 | Cron Expressions | [cron-expressions.md](cron-expressions.md) |
| 28 | Multi-TFM Compatibility | [multi-tfm-compatibility.md](multi-tfm-compatibility.md) |
| 29 | Security | [security.md](security.md) |
| 30 | Support & Community | [support-community.md](support-community.md) |

---

## 01 - Cover

| Field | Value |
|-------|-------|
| **Name** | XForge.Scheduling |
| **Version** | 0.4.0 |
| **Status** | Published |
| **Last Updated** | 2026-05-29 |
| **License** | MIT |
| **Repository** | [github.com/renatotiburcio/XForge.Scheduling](https://github.com/renatotiburcio/XForge.Scheduling) |

---

## 02 - Introduction

### What It Is

XForge.Scheduling is a .NET library for background job scheduling. It provides abstractions for IJobScheduler and IJobHandler<T> with support for cron expressions, immediate execution, and job management.

`csharp
await scheduler.ScheduleAsync(new JobSchedule(
    "cleanup", "Daily Cleanup", "0 2 * * *", typeof(CleanupHandler), DateTimeOffset.UtcNow));
`

### Why It Exists

- **Hangfire** - Requires external infrastructure (Redis/SQL Server), complex for simple scenarios.
- **Quartz.NET** - Extensive configuration, high learning curve.
- **Manual implementations** - Timer-based jobs without persistence or cron support.

XForge.Scheduling offers a simple API with in-memory implementation for development and abstractions for production.

### Technical Differentiators

| Feature | XForge.Scheduling | Hangfire | Quartz.NET |
|---|:---:|:---:|:---:|
| Zero configuration | ✅ | ❌ | ❌ |
| In-memory implementation | ✅ | ❌ | ❌ |
| Cron expressions | ✅ | ✅ | ✅ |
| Typed IJobHandler | ✅ | ❌ | ✅ |
| JobResult with status | ✅ | ❌ | ❌ |
| MIT license | ✅ | ❌ | ✅ |

---

## 04 - Features

### 4.1 - Job Scheduling with Cron

`csharp
var schedule = new JobSchedule(
    Id: "daily-report",
    Name: "Daily Report",
    CronExpression: "0 8 * * MON-FRI",
    HandlerType: typeof(ReportHandler),
    CreatedUtc: DateTimeOffset.UtcNow);

await scheduler.ScheduleAsync(schedule);
`

### 4.2 - Immediate Job Execution

`csharp
var result = await scheduler.ExecuteAsync("daily-report");
if (result.Success)
    Console.WriteLine("Job executed successfully");
`

### 4.3 - Typed Job Handlers

`csharp
public class ReportHandler : IJobHandler<ReportContext>
{
    public async Task HandleAsync(ReportContext context, CancellationToken ct)
    {
        await GenerateReportAsync(context.ReportDate, ct);
    }
}
`

### 4.4 - Job Management

`csharp
var jobs = await scheduler.GetAllAsync();
await scheduler.UnscheduleAsync("daily-report");
`

---

<div align="center">

**Next:** [Installation →](installation.md)

</div>
