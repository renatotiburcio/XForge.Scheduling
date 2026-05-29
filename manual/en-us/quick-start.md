> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 06 — Quick Start

Exemplo rápido para começar a usar o XForge.Scheduling.

## Exemplo Mínimo (5 minutos)

### 1. Criar o projeto

`ash
dotnet new worker -n MeuScheduler
cd MeuScheduler
dotnet add package XForge.Scheduling
`

### 2. Criar o job handler

`csharp
public class CleanupHandler : IJobHandler<object>
{
    private readonly ILogger<CleanupHandler> _logger;

    public CleanupHandler(ILogger<CleanupHandler> logger)
        => _logger = logger;

    public Task HandleAsync(object context, CancellationToken ct)
    {
        _logger.LogInformation("Executando limpeza...");
        return Task.CompletedTask;
    }
}
`

### 3. Configurar Program.cs

`csharp
using XForge.Scheduling.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddXForgeScheduling();

var host = builder.Build();

var scheduler = host.Services.GetRequiredService<IJobScheduler>();
await scheduler.ScheduleAsync(new JobSchedule(
    "cleanup", "Limpeza Diária", "0 2 * * *",
    typeof(CleanupHandler), DateTimeOffset.UtcNow));

await host.RunAsync();
`

### 4. Executar

`ash
dotnet run
`

## Exemplo com contexto tipado

`csharp
public class ReportContext
{
    public DateTime ReportDate { get; set; }
    public string Format { get; set; } = "PDF";
}

public class ReportHandler : IJobHandler<ReportContext>
{
    public async Task HandleAsync(ReportContext context, CancellationToken ct)
    {
        await GenerateReportAsync(context.ReportDate, context.Format, ct);
    }
}
`

---

**Navegação:** ← [Instalação](installation.md) | → [Configuração](configuration.md)
