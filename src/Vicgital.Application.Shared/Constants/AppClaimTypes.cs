namespace Vicgital.Application.Shared.Constants;

/// <summary>Well-known claim types shared across Vicgital services' auth tokens.</summary>
public static class AppClaimTypes
{
    public const string UserId = "sub";
    public const string Email = "email";
    public const string TenantId = "tenant_id";
    public const string Role = "role";
}
