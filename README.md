# Vicgital.Application.Shared

Core application contracts, standard exceptions, and cross-cutting abstractions used across all Vicgital .NET applications' application layer.

This library has no external dependencies and no framework opinions (no ASP.NET Core, no MediatR). It defines *shapes* — exceptions, results, envelopes, pagination types, constants, and interfaces — that every Vicgital service implements or maps to on its own terms.

## Install

Package source is configured in `nuget.config` (nuget.org + the Vicgital GitHub package feed, which requires a `GIT_PACKAGES_READ_ONLY_PAT` environment variable for read access).

```
dotnet add package Vicgital.Application.Shared
```

## What's in here

### Exceptions (`Vicgital.Application.Shared.Exceptions`)

A small hierarchy rooted at `AppException` (`Code` + message), for **unexpected** failures a global exception handler/middleware catches and maps to a transport status:

| Type | Typical HTTP mapping |
|---|---|
| `NotFoundException` | 404 |
| `ConflictException` | 409 |
| `ForbiddenException` | 403 |
| `UnauthorizedAppException` | 401 |
| `BusinessRuleViolationException` | 422 / 400 |
| `ValidationAppException` | 400 (carries a `field -> string[]` error dictionary) |

### Results (`Vicgital.Application.Shared.Results`)

`Result` / `Result<T>` for **expected** business/validation outcomes a caller should branch on inline instead of catching an exception:

```csharp
Result<Order> result = order.PlaceOrder(request);

if (result.IsFailure)
{
    return result.Errors; // IReadOnlyList<Error>, each with Code/Message/ErrorType
}
```

`Error` factory methods (`Error.Validation`, `Error.NotFound`, `Error.Conflict`, `Error.Forbidden`, `Error.Unauthorized`, `Error.Unexpected`) tag each failure with an `ErrorType` so hosts can map it without inspecting message text.

**Rule of thumb:** use `Result` for outcomes the immediate caller decides how to handle; throw an `AppException` when the failure should propagate past several layers to a global handler.

### Envelopes (`Vicgital.Application.Shared.Envelopes`)

`ApiResponse` / `ApiResponse<T>` — the standard `{ Success, Data, Errors, TraceId }` wire shape. `ResultExtensions.ToApiResponse()` converts a `Result`/`Result<T>` straight into the matching envelope, so controllers/handlers produce the same response shape regardless of which failure pattern an operation uses internally.

### Pagination (`Vicgital.Application.Shared.Pagination`)

- `PagedRequest` — normalizes page number/size (defaults and caps come from `Constants.PaginationDefaults`) and exposes `Skip` for `Skip().Take()` queries.
- `PagedResult<T>` — `Items`, `TotalCount`, `TotalPages`, `HasNextPage`, `HasPreviousPage`.

### Constants (`Vicgital.Application.Shared.Constants`)

`PaginationDefaults`, `AppClaimTypes` (well-known JWT claim names), `AppHeaderNames` (correlation/request id headers) — shared literals so services don't redefine or drift on them independently.

### Abstractions (`Vicgital.Application.Shared.Abstractions`)

Interfaces only — each consuming application registers its own implementation in DI:

- `IDateTimeProvider` — testable `UtcNow` / `TodayUtc`.
- `ICurrentUserService` — current caller identity (auth state, id, email, roles).
- `ICorrelationIdProvider` — current operation's correlation id, for logging/tracing.

## Design principles

- **No framework dependencies.** Nothing here references ASP.NET Core, MediatR, EF Core, etc. Each application maps these types onto its own framework (e.g. an exception-handling middleware that catches `AppException` and writes an `ApiResponse`).
- **No CQRS opinions.** There are deliberately no `ICommand`/`IQuery` marker interfaces — this library doesn't assume a particular request-pipeline shape.
- **Interfaces, not implementations.** The `Abstractions` namespace defines contracts; implementations live in each application, per host.
