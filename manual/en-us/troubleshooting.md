> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 16 — Troubleshooting

## "Job not found"

**Causa:** O jobId não foi registrado no scheduler.

**Solução:** Verifique se o job foi agendado antes de executar.

## "No handler registered for job"

**Causa:** O handler não foi registrado via RegisterHandler.

**Solução:** Registre o handler antes de executar.

## Jobs não executam automaticamente

**Causa:** O InMemoryJobScheduler não tem timer automático.

**Solução:** Use um BackgroundService para iterar e executar jobs periodicamente.

---

**Navegação:** ← [Performance](./performance.md) | → [FAQ](./faq.md)
