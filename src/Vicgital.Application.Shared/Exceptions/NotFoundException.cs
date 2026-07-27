namespace Vicgital.Application.Shared.Exceptions;

/// <summary>Thrown when a requested resource does not exist. Maps to HTTP 404 in typical hosts.</summary>
public sealed class NotFoundException : AppException
{
    public NotFoundException(string message)
        : base("not_found", message)
    {
    }

    public NotFoundException(string entityName, object key)
        : base("not_found", $"{entityName} with id '{key}' was not found.")
    {
    }
}
