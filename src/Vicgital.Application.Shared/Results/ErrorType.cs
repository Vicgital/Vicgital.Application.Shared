namespace Vicgital.Application.Shared.Results;

/// <summary>Classifies an <see cref="Error"/> so hosts can map it to a transport-level status without inspecting messages.</summary>
public enum ErrorType
{
    Unexpected = 0,
    Validation,
    NotFound,
    Conflict,
    Forbidden,
    Unauthorized,
}
