namespace Vicgital.Application.Shared.Abstractions;

/// <summary>Abstracts access to the caller identity for the current operation. Implemented per host (HTTP context, message metadata, etc.).</summary>
public interface ICurrentUserService
{
    bool IsAuthenticated { get; }

    string? UserId { get; }

    string? Email { get; }

    IReadOnlyList<string> Roles { get; }

    bool IsInRole(string role);
}
