using Microsoft.Extensions.Logging.Abstractions;

namespace XForge.Scheduling.Tests;

public class InMemoryJobSchedulerTests
{
    private readonly InMemoryJobScheduler _scheduler = new(NullLogger<InMemoryJobScheduler>.Instance);

    [Fact]
    public async Task ScheduleAsync_StoresSchedule()
    {
        var schedule = new JobSchedule("job-1", "Test Job", "0 * * * *", typeof(object), DateTimeOffset.UtcNow);

        await _scheduler.ScheduleAsync(schedule);

        var all = await _scheduler.GetAllAsync();
        all.Should().ContainSingle().Which.Should().Be(schedule);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllSchedules_OrdersByName()
    {
        var schedule1 = new JobSchedule("job-2", "Zebra Job", "0 * * * *", typeof(object), DateTimeOffset.UtcNow);
        var schedule2 = new JobSchedule("job-1", "Alpha Job", "0 * * * *", typeof(object), DateTimeOffset.UtcNow);

        await _scheduler.ScheduleAsync(schedule1);
        await _scheduler.ScheduleAsync(schedule2);

        var all = await _scheduler.GetAllAsync();

        all.Should().HaveCount(2);
        all[0].Name.Should().Be("Alpha Job");
        all[1].Name.Should().Be("Zebra Job");
    }

    [Fact]
    public async Task UnscheduleAsync_RemovesSchedule()
    {
        var schedule = new JobSchedule("job-1", "Test Job", "0 * * * *", typeof(object), DateTimeOffset.UtcNow);
        await _scheduler.ScheduleAsync(schedule);

        await _scheduler.UnscheduleAsync("job-1");

        var all = await _scheduler.GetAllAsync();
        all.Should().BeEmpty();
    }

    [Fact]
    public async Task UnscheduleAsync_NonexistentJob_DoesNotThrow()
    {
        var act = async () => await _scheduler.UnscheduleAsync("nonexistent");

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ExecuteAsync_WithRegisteredHandler_ReturnsSuccess()
    {
        var schedule = new JobSchedule("job-1", "Test Job", "0 * * * *", typeof(object), DateTimeOffset.UtcNow);
        await _scheduler.ScheduleAsync(schedule);

        var executed = false;
        _scheduler.RegisterHandler("job-1", _ =>
        {
            executed = true;
            return Task.CompletedTask;
        });

        var result = await _scheduler.ExecuteAsync("job-1");

        result.Success.Should().BeTrue();
        result.ErrorMessage.Should().BeNull();
        executed.Should().BeTrue();
    }

    [Fact]
    public async Task ExecuteAsync_HandlerThrows_ReturnsFailure()
    {
        var schedule = new JobSchedule("job-1", "Test Job", "0 * * * *", typeof(object), DateTimeOffset.UtcNow);
        await _scheduler.ScheduleAsync(schedule);

        _scheduler.RegisterHandler("job-1", _ => throw new InvalidOperationException("test error"));

        var result = await _scheduler.ExecuteAsync("job-1");

        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Be("test error");
    }

    [Fact]
    public async Task ExecuteAsync_NoHandlerRegistered_ReturnsFailure()
    {
        var schedule = new JobSchedule("job-1", "Test Job", "0 * * * *", typeof(object), DateTimeOffset.UtcNow);
        await _scheduler.ScheduleAsync(schedule);

        var result = await _scheduler.ExecuteAsync("job-1");

        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Contain("No handler registered");
    }

    [Fact]
    public async Task ExecuteAsync_JobNotFound_ReturnsFailure()
    {
        var result = await _scheduler.ExecuteAsync("nonexistent");

        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Contain("not found");
    }

    [Fact]
    public async Task ExecuteAsync_EmptyJobId_Throws()
    {
        var act = async () => await _scheduler.ExecuteAsync("");

        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task ScheduleAsync_NullSchedule_Throws()
    {
        var act = async () => await _scheduler.ScheduleAsync(null!);

        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ScheduleAsync_OverwritesExistingSchedule()
    {
        var schedule1 = new JobSchedule("job-1", "Job v1", "0 * * * *", typeof(object), DateTimeOffset.UtcNow);
        var schedule2 = new JobSchedule("job-1", "Job v2", "*/5 * * * *", typeof(string), DateTimeOffset.UtcNow);

        await _scheduler.ScheduleAsync(schedule1);
        await _scheduler.ScheduleAsync(schedule2);

        var all = await _scheduler.GetAllAsync();

        all.Should().ContainSingle();
        all[0].Name.Should().Be("Job v2");
    }

    [Fact]
    public void RegisterHandler_EmptyJobId_Throws()
    {
        var act = () => _scheduler.RegisterHandler("", _ => Task.CompletedTask);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void RegisterHandler_NullHandler_Throws()
    {
        var act = () => _scheduler.RegisterHandler("job-1", null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task ExecuteAsync_SetsExecutedUtc()
    {
        var schedule = new JobSchedule("job-1", "Test Job", "0 * * * *", typeof(object), DateTimeOffset.UtcNow);
        await _scheduler.ScheduleAsync(schedule);

        _scheduler.RegisterHandler("job-1", _ => Task.CompletedTask);

        var before = DateTimeOffset.UtcNow;
        var result = await _scheduler.ExecuteAsync("job-1");

        result.ExecutedUtc.Should().BeOnOrAfter(before);
    }
}
