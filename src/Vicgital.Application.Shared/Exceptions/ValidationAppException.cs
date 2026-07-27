using System.Collections.ObjectModel;

namespace Vicgital.Application.Shared.Exceptions;

/// <summary>
/// Thrown when one or more field-level validation errors prevent an operation from proceeding. Maps to HTTP 400.
/// Prefer this only for validation failures a caller can't reasonably branch on with <see cref="Vicgital.Application.Shared.Results.Result"/>
/// (e.g. deep in a call stack where surfacing a Result all the way up isn't practical).
/// </summary>
public sealed class ValidationAppException : AppException
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public ValidationAppException(IDictionary<string, string[]> errors)
        : base("validation_failed", "One or more validation errors occurred.")
    {
        Errors = new ReadOnlyDictionary<string, string[]>(errors);
    }

    public ValidationAppException(string propertyName, string errorMessage)
        : this(new Dictionary<string, string[]> { [propertyName] = [errorMessage] })
    {
    }
}
