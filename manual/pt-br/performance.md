> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 15 — Performance

O XForge.Scheduling é leve por design. A implementação InMemory usa ConcurrentDictionary para operações thread-safe.

## Considerações

- InMemoryJobScheduler é adequado para dev/test, não para produção com persistência.
- Para produção, implemente IJobScheduler com banco de dados.
- Jobs devem ser rápidos; para operações longas, use background processing.

---

**Navegação:** ← [Testing](./testing.md) | → [Troubleshooting](./troubleshooting.md)
