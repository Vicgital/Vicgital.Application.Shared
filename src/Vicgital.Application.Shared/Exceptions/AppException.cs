namespace Vicgital.Application.Shared.Exceptions;

/// <summary>
/// Base type for all exceptions raised deliberately by application-layer code across Vicgital services.
/// Reserve these for unexpected/exceptional failures; use <see cref="Vicgital.Application.Shared.Results.Result"/>
/// for expected business outcomes (e.g. validation failures a caller should branch on without a try/catch).
/// </summary>
public abstract class AppException : Exception
{
    /// <summary>Stable, machine-readable error code (e.g. "not_found") that hosts can map to an HTTP status or client-facing code.</summary>
    public string Code { get; }

    protected AppException(string code, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
    }
}
