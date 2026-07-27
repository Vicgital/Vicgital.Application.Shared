namespace Vicgital.Application.Shared.Results;

/// <summary>A <see cref="Result"/> that carries a value on success.</summary>
public sealed class Result<T> : Result
{
    private readonly T? _value;

    /// <summary>The success value. Throws if the result is a failure — check <see cref="Result.IsSuccess"/> first.</summary>
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access the value of a failed result.");

    private Result(T value)
        : base(true, [])
    {
        _value = value;
    }

    private Result(IReadOnlyList<Error> errors)
        : base(false, errors)
    {
        _value = default;
    }

    public static Result<T> Success(T value) => new(value);

    public static new Result<T> Failure(Error error) => new([error]);

    public static new Result<T> Failure(IEnumerable<Error> errors) => new(errors.ToArray());

    public static implicit operator Result<T>(T value) => Success(value);

    public static implicit operator Result<T>(Error error) => Failure(error);
}
