namespace Vicgital.Application.Shared.Results;

/// <summary>
/// Represents the outcome of an operation that can fail in expected ways, without throwing.
/// Use this for business/validation outcomes a caller should branch on; reserve exceptions
/// (see <see cref="Vicgital.Application.Shared.Exceptions.AppException"/>) for unexpected failures.
/// </summary>
public class Result
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public IReadOnlyList<Error> Errors { get; }

    public Error? FirstError => Errors.Count > 0 ? Errors[0] : null;

    protected Result(bool isSuccess, IReadOnlyList<Error> errors)
    {
        if (isSuccess && errors.Count > 0)
        {
            throw new InvalidOperationException("A successful result cannot carry errors.");
        }

        if (!isSuccess && errors.Count == 0)
        {
            throw new InvalidOperationException("A failed result must carry at least one error.");
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success() => new(true, []);

    public static Result Failure(Error error) => new(false, [error]);

    public static Result Failure(IEnumerable<Error> errors) => new(false, errors.ToArray());
}
