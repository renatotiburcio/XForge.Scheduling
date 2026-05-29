namespace XForge.Scheduling;

/// <summary>
/// Abstracts job scheduling, storage, and execution.
/// </summary>
public interface IJobScheduler
{
    /// <summary>
    /// Schedules a job for execution.
    /// </summary>
    /// <param name="schedule">The job schedule definition.</param>
    /// <param name="ct">Cancellation token.</param>
    Task ScheduleAsync(JobSchedule schedule, CancellationToken ct = default);

    /// <summary>
    /// Removes a scheduled job.
    /// </summary>
    /// <param name="jobId">The job identifier to unschedule.</param>
    /// <param name="ct">Cancellation token.</param>
    Task UnscheduleAsync(string jobId, CancellationToken ct = default);

    /// <summary>
    /// Retrieves all scheduled jobs.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A read-only list of all scheduled jobs.</returns>
    Task<IReadOnlyList<JobSchedule>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Executes a scheduled job immediately.
    /// </summary>
    /// <param name="jobId">The job identifier to execute.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The result of the job execution.</returns>
    Task<JobResult> ExecuteAsync(string jobId, CancellationToken ct = default);
}
