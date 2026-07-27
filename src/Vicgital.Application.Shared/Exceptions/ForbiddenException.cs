namespace Vicgital.Application.Shared.Exceptions;

/// <summary>Thrown when an authenticated caller lacks permission to perform an action. Maps to HTTP 403.</summary>
public sealed class ForbiddenException : AppException
{
    public ForbiddenException(string message = "You do not have permission to perform this action.")
        : base("forbidden", message)
    {
    }
}
