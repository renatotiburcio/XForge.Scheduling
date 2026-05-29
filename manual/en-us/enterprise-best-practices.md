> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 12 — Boas Práticas Enterprise

## Separar handlers por responsabilidade

`csharp
// ✅ Bom — handler com responsabilidade única
public class DailyReportHandler : IJobHandler<ReportContext> { ... }

// ❌ Ruim — handler fazendo múltiplas coisas
public class DoEverythingHandler : IJobHandler<object> { ... }
`

## Usar DI para resolver handlers

`csharp
// ✅ Bom — resolver do container
using var scope = scopeFactory.CreateScope();
var handler = scope.ServiceProvider.GetRequiredService<MyHandler>();

// ❌ Ruim — criar manualmente
var handler = new MyHandler(); // Sem dependências injetadas
`

## Logging em handlers

`csharp
public class MyHandler : IJobHandler<MyContext>
{
    private readonly ILogger<MyHandler> _logger;

    public async Task HandleAsync(MyContext context, CancellationToken ct)
    {
        _logger.LogInformation("Iniciando job {JobId}", context.JobId);
        try
        {
            // Lógica do job
            _logger.LogInformation("Job {JobId} concluído", context.JobId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Job {JobId} falhou", context.JobId);
            throw;
        }
    }
}
`

## Tratamento de erros

`csharp
var result = await scheduler.ExecuteAsync("my-job");
if (!result.Success)
{
    _logger.LogError("Job falhou: {Error}", result.ErrorMessage);
    // Notificar, retry, etc.
}
`

---

**Navegação:** ← [Uso Avançado](./advanced-usage.md) | → [Exemplos de Integração](./integration-examples.md)
