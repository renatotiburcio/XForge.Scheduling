using FluentAssertions;

namespace XForge.Scheduling.Tests;

public class JobScheduleTests
{
    [Fact]
    public void Properties_AreSet()
    {
        var now = DateTimeOffset.UtcNow;
        var schedule = new JobSchedule("job-1", "My Job", "0 * * * *", typeof(object), now);

        schedule.Id.Should().Be("job-1");
        schedule.Name.Should().Be("My Job");
        schedule.CronExpression.Should().Be("0 * * * *");
#pragma warning disable CA2263
        schedule.HandlerType.Should().Be(typeof(object));
#pragma warning restore CA2263
        schedule.CreatedUtc.Should().Be(now);
    }

    [Fact]
    public void Record_Equality_Works()
    {
        var now = DateTimeOffset.UtcNow;
        var s1 = new JobSchedule("job-1", "My Job", "0 * * * *", typeof(object), now);
        var s2 = s1 with { };

        s1.Should().Be(s2);
    }

    [Fact]
    public void Record_Inequality_Works()
    {
        var now = DateTimeOffset.UtcNow;
        var s1 = new JobSchedule("job-1", "Job A", "0 * * * *", typeof(object), now);
        var s2 = new JobSchedule("job-2", "Job B", "*/5 * * * *", typeof(string), now);

        s1.Should().NotBe(s2);
    }
}

public class JobResultTests
{
    [Fact]
    public void Success_Result()
    {
        var result = new JobResult(true, null, DateTimeOffset.UtcNow);

        result.Success.Should().BeTrue();
        result.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public void Failure_Result()
    {
        var result = new JobResult(false, "Something went wrong", DateTimeOffset.UtcNow);

        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Be("Something went wrong");
    }

    [Fact]
    public void Record_Equality_Works()
    {
        var now = DateTimeOffset.UtcNow;
        var r1 = new JobResult(true, null, now);
        var r2 = r1 with { };

        r1.Should().Be(r2);
    }
}

public class JobStatusTests
{
    [Fact]
    public void AllStatuses_AreDefined()
    {
        var statuses = Enum.GetValues<JobStatus>();

        statuses.Should().Contain(JobStatus.Scheduled);
        statuses.Should().Contain(JobStatus.Running);
        statuses.Should().Contain(JobStatus.Completed);
        statuses.Should().Contain(JobStatus.Failed);
    }

    [Fact]
    public void Default_IsScheduled()
    {
        var status = default(JobStatus);

        status.Should().Be(JobStatus.Scheduled);
    }
}
