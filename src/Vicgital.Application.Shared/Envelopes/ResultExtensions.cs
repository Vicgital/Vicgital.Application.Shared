using Vicgital.Application.Shared.Results;

namespace Vicgital.Application.Shared.Envelopes;

/// <summary>Converts <see cref="Result"/>/<see cref="Result{T}"/> outcomes into transport-ready <see cref="ApiResponse"/> envelopes.</summary>
public static class ResultExtensions
{
    public static ApiResponse ToApiResponse(this Result result, string? traceId = null) =>
        result.IsSuccess
            ? ApiResponse.Ok(traceId)
            : ApiResponse.Fail(result.Errors.Select(ToApiError), traceId);

    public static ApiResponse<T> ToApiResponse<T>(this Result<T> result, string? traceId = null) =>
        result.IsSuccess
            ? ApiResponse<T>.Ok(result.Value, traceId)
            : ApiResponse<T>.Fail(result.Errors.Select(ToApiError), traceId);

    private static ApiError ToApiError(Error error) => new(error.Code, error.Message);
}
