namespace XForge.Scheduling;

/// <summary>
/// Represents a scheduled job definition.
/// </summary>
/// <param name="Id">The unique identifier for this schedule.</param>
/// <param name="Name">The human-readable name of the job.</param>
/// <param name="CronExpression">The cron expression defining when the job should run.</param>
/// <param name="HandlerType">The type of the handler that executes this job.</param>
/// <param name="CreatedUtc">The UTC time when this schedule was created.</param>
public sealed record JobSchedule(
    string Id,
    string Name,
    string CronExpression,
    Type HandlerType,
    DateTimeOffset CreatedUtc);
