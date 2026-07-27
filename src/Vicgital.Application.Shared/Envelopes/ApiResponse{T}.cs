namespace Vicgital.Application.Shared.Envelopes;

/// <summary>Standard response envelope for operations that return data.</summary>
public sealed class ApiResponse<T>
{
    public bool Success { get; init; }

    public T? Data { get; init; }

    public IReadOnlyList<ApiError>? Errors { get; init; }

    public string? TraceId { get; init; }

    public static ApiResponse<T> Ok(T data, string? traceId = null) =>
        new() { Success = true, Data = data, TraceId = traceId };

    public static ApiResponse<T> Fail(ApiError error, string? traceId = null) => Fail([error], traceId);

    public static ApiResponse<T> Fail(IEnumerable<ApiError> errors, string? traceId = null) =>
        new() { Success = false, Errors = errors.ToArray(), TraceId = traceId };
}
