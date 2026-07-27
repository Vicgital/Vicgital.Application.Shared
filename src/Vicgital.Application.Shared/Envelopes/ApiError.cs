namespace Vicgital.Application.Shared.Envelopes;

/// <summary>A single error surfaced to a client inside an <see cref="ApiResponse"/>.</summary>
public sealed record ApiError(string Code, string Message);
