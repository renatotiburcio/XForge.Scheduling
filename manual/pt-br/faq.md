> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 17 — FAQ

### 1. O InMemoryJobScheduler executa jobs automaticamente?

**Não.** Ele apenas armazena os schedules. Use um BackgroundService ou implementação custom para executar periodicamente.

### 2. Posso usar sem expressões cron?

**Sim.** A expressão cron é armazenada como string. Você pode parsear com a biblioteca de sua escolha.

### 3. Como persistir jobs?

Implemente IJobScheduler com Entity Framework Core ou outro provider.

### 4. Jobs são executados em paralelo?

Depende da sua implementação. O InMemoryJobScheduler executa sequencialmente.

---

**Navegação:** ← [Troubleshooting](./troubleshooting.md) | → [Roadmap](./roadmap.md)
