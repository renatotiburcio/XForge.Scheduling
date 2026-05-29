namespace XForge.Scheduling;

/// <summary>
/// Represents the result of a job execution.
/// </summary>
/// <param name="Success">Whether the job completed successfully.</param>
/// <param name="ErrorMessage">The error message if the job failed, or null if successful.</param>
/// <param name="ExecutedUtc">The UTC time when the job was executed.</param>
public sealed record JobResult(bool Success, string? ErrorMessage, DateTimeOffset ExecutedUtc);
