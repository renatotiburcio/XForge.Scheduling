using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace XForge.Scheduling;

/// <summary>
/// In-memory implementation of <see cref="IJobScheduler"/> for development and testing.
/// Production applications should implement <see cref="IJobScheduler"/> with a real scheduling backend.
/// </summary>
/// <param name="logger">The logger instance.</param>
public sealed class InMemoryJobScheduler(ILogger<InMemoryJobScheduler> logger) : IJobScheduler
{
    private readonly ConcurrentDictionary<string, JobSchedule> _schedules = new(StringComparer.Ordinal);
    private readonly ConcurrentDictionary<string, Func<CancellationToken, Task>> _handlers = new(StringComparer.Ordinal);

    /// <summary>
    /// Registers an execution handler for a specific job.
    /// </summary>
    /// <param name="jobId">The job identifier.</param>
    /// <param name="handler">The handler function to execute.</param>
    public void RegisterHandler(string jobId, Func<CancellationToken, Task> handler)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(jobId);
        ArgumentNullException.ThrowIfNull(handler);

        _handlers[jobId] = handler;
    }

    /// <inheritdoc />
    public Task ScheduleAsync(JobSchedule schedule, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(schedule);

        _schedules[schedule.Id] = schedule;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UnscheduleAsync(string jobId, CancellationToken ct = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(jobId);

        _schedules.TryRemove(jobId, out _);
        _handlers.TryRemove(jobId, out _);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<JobSchedule>> GetAllAsync(CancellationToken ct = default)
    {
        JobSchedule[] result = [.. _schedules.Values.OrderBy(s => s.Name, StringComparer.Ordinal)];
        return Task.FromResult<IReadOnlyList<JobSchedule>>(result);
    }

    /// <inheritdoc />
    public async Task<JobResult> ExecuteAsync(string jobId, CancellationToken ct = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(jobId);

        if (!_schedules.TryGetValue(jobId, out _))
        {
            return new JobResult(false, $"Job '{jobId}' not found.", DateTimeOffset.UtcNow);
        }

        if (!_handlers.TryGetValue(jobId, out var handler))
        {
            return new JobResult(false, $"No handler registered for job '{jobId}'.", DateTimeOffset.UtcNow);
        }

        logger.LogInformation("Executing job {JobId}", jobId);

        try
        {
            await handler(ct);
            logger.LogInformation("Job {JobId} completed successfully", jobId);
            return new JobResult(true, null, DateTimeOffset.UtcNow);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Job {JobId} failed", jobId);
            return new JobResult(false, ex.Message, DateTimeOffset.UtcNow);
        }
    }
}
