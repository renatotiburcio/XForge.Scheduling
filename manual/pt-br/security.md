# 29 — Segurança

## Considerações

### Injeção de dependências em handlers

Sempre resolva handlers via DI container para evitar instanciação manual sem dependências.

### Execução de jobs

Jobs executam código arbitrário. Valide e sanitiza dados de contexto antes da execução.

### Logs sensíveis

Não logue dados sensíveis dentro de handlers de jobs.

---

**Navegação:** ← [Compatibilidade Multi-TFM](./multi-tfm-compatibility.md) | → [Suporte & Comunidade](./support-community.md)
