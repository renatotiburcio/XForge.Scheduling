# XForge.Scheduling — Manual Enterprise

> ✅ **Estável:** v0.4.0

---

## 08 — Arquitetura

### Visão geral

`
┌────────────────────────────────────────────────┐
│              Consumidor (Aplicação)             │
└───────────────────────┬────────────────────────┘
                        │
                        ▼
┌────────────────────────────────────────────────┐
│              XForge.Scheduling                  │
│  ┌──────────────────────────────────────────┐  │
│  │ IJobScheduler      │  JobSchedule        │  │
│  │ IJobHandler<T>     │  JobResult          │  │
│  │ InMemoryJobScheduler  │  JobStatus       │  │
│  │ ServiceCollectionExtensions              │  │
│  └──────────────────────────────────────────┘  │
└────────────────────────────────────────────────┘
`

### Componentes

| Componente | Descrição |
|-----------|-----------|
| IJobScheduler | Interface principal para agendamento e execução |
| IJobHandler<T> | Handler tipado para execução de jobs |
| JobSchedule | Definição do job (Id, Name, Cron, HandlerType) |
| JobResult | Resultado da execução (Success, ErrorMessage) |
| InMemoryJobScheduler | Implementação para dev/test |

### Ciclo de vida

| Componente | Lifetime |
|-----------|----------|
| IJobScheduler | Singleton |

---

**Navegação:** ← [Configuração](./configuration.md) | → [Uso Básico](./basic-usage.md)
