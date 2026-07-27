namespace Vicgital.Application.Shared.Exceptions;

/// <summary>Thrown when a request conflicts with the current state of a resource (e.g. duplicate key, stale version). Maps to HTTP 409.</summary>
public sealed class ConflictException : AppException
{
    public ConflictException(string message)
        : base("conflict", message)
    {
    }
}
