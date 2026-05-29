namespace XForge.Scheduling;

/// <summary>
/// Represents the current status of a scheduled job.
/// </summary>
public enum JobStatus
{
    /// <summary>
    /// The job is scheduled and waiting to be executed.
    /// </summary>
    Scheduled,

    /// <summary>
    /// The job is currently running.
    /// </summary>
    Running,

    /// <summary>
    /// The job completed successfully.
    /// </summary>
    Completed,

    /// <summary>
    /// The job failed with an error.
    /// </summary>
    Failed
}
