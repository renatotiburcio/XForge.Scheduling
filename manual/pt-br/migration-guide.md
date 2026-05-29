# 22 — Guia de Migração

## De Timer-based para XForge.Scheduling

`csharp
// Antes
var timer = new Timer(async _ => await DoWorkAsync(), null, TimeSpan.Zero, TimeSpan.FromHours(1));

// Depois
await scheduler.ScheduleAsync(new JobSchedule(
    "work", "Work Job", "0 * * * *", typeof(WorkHandler), DateTimeOffset.UtcNow));
`

---

**Navegação:** ← [Comparação](./package-comparison.md) | → [Contribuindo](./contributing.md)
