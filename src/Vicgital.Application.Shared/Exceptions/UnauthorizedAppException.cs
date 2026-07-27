namespace Vicgital.Application.Shared.Exceptions;

/// <summary>Thrown when an action requires authentication that is missing or invalid. Maps to HTTP 401.</summary>
public sealed class UnauthorizedAppException : AppException
{
    public UnauthorizedAppException(string message = "Authentication is required.")
        : base("unauthorized", message)
    {
    }
}
