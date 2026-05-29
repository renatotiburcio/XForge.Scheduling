namespace XForge.Scheduling;

/// <summary>
/// Handles execution of a specific type of job.
/// </summary>
/// <typeparam name="T">The job context type.</typeparam>
public interface IJobHandler<in T>
{
    /// <summary>
    /// Executes the job with the given context.
    /// </summary>
    /// <param name="context">The job context data.</param>
    /// <param name="ct">Cancellation token.</param>
    Task HandleAsync(T context, CancellationToken ct = default);
}
