> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 14 — Testing

## Testando com InMemoryJobScheduler

`csharp
[Fact]
public async Task ScheduleAsync_ShouldAddJob()
{
    // Arrange
    var logger = NullLogger<InMemoryJobScheduler>.Instance;
    var scheduler = new InMemoryJobScheduler(logger);
    var schedule = new JobSchedule("test", "Test Job", "* * * * *",
        typeof(TestHandler), DateTimeOffset.UtcNow);

    // Act
    await scheduler.ScheduleAsync(schedule);
    var jobs = await scheduler.GetAllAsync();

    // Assert
    Assert.Single(jobs);
    Assert.Equal("test", jobs[0].Id);
}
`

## Testando execução

`csharp
[Fact]
public async Task ExecuteAsync_WithHandler_ShouldReturnSuccess()
{
    // Arrange
    var logger = NullLogger<InMemoryJobScheduler>.Instance;
    var scheduler = new InMemoryJobScheduler(logger);
    var executed = false;

    scheduler.RegisterHandler("test", ct =>
    {
        executed = true;
        return Task.CompletedTask;
    });

    await scheduler.ScheduleAsync(new JobSchedule(
        "test", "Test", "* * * * *", typeof(object), DateTimeOffset.UtcNow));

    // Act
    var result = await scheduler.ExecuteAsync("test");

    // Assert
    Assert.True(result.Success);
    Assert.True(executed);
}
`

## Testando falha

`csharp
[Fact]
public async Task ExecuteAsync_WhenHandlerThrows_ShouldReturnFailure()
{
    var scheduler = new InMemoryJobScheduler(NullLogger<InMemoryJobScheduler>.Instance);
    scheduler.RegisterHandler("fail", ct => throw new InvalidOperationException("erro"));

    await scheduler.ScheduleAsync(new JobSchedule(
        "fail", "Fail", "* * * * *", typeof(object), DateTimeOffset.UtcNow));

    var result = await scheduler.ExecuteAsync("fail");

    Assert.False(result.Success);
    Assert.Contains("erro", result.ErrorMessage);
}
`

---

**Navegação:** ← [Exemplos de Integração](./integration-examples.md) | → [Performance](./performance.md)
