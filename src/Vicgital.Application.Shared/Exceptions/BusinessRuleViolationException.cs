namespace Vicgital.Application.Shared.Exceptions;

/// <summary>Thrown when an operation violates a domain/business rule that isn't a simple field-level validation error. Maps to HTTP 422/400.</summary>
public sealed class BusinessRuleViolationException : AppException
{
    public BusinessRuleViolationException(string message)
        : base("business_rule_violation", message)
    {
    }
}
