namespace Vicgital.Application.Shared.Envelopes;

/// <summary>Standard response envelope for operations that return no data.</summary>
public sealed class ApiResponse
{
    public bool Success { get; init; }

    public IReadOnlyList<ApiError>? Errors { get; init; }

    public string? TraceId { get; init; }

    public static ApiResponse Ok(string? traceId = null) => new() { Success = true, TraceId = traceId };

    public static ApiResponse Fail(ApiError error, string? traceId = null) => Fail([error], traceId);

    public static ApiResponse Fail(IEnumerable<ApiError> errors, string? traceId = null) =>
        new() { Success = false, Errors = errors.ToArray(), TraceId = traceId };
}
