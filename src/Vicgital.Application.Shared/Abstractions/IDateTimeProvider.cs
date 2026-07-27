namespace Vicgital.Application.Shared.Abstractions;

/// <summary>Abstracts the current time so application code stays deterministic and testable. Implemented per host.</summary>
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }

    DateOnly TodayUtc { get; }
}
