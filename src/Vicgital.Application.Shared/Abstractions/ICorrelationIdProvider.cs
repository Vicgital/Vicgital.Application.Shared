namespace Vicgital.Application.Shared.Abstractions;

/// <summary>Abstracts access to the correlation id for the current request/operation, for logging and tracing. Implemented per host.</summary>
public interface ICorrelationIdProvider
{
    string CorrelationId { get; }
}
