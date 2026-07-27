# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this repo is

`Vicgital.Application.Shared` is a .NET class library (targets `net10.0`) consumed by the application layer of every Vicgital .NET service. It ships shapes, not behavior: exceptions, a `Result` type, response envelopes, pagination types, shared constants, and cross-cutting interfaces. It has zero external package dependencies and no framework opinions — nothing here references ASP.NET Core, MediatR, EF Core, etc. Each consuming application implements the interfaces and maps the exception/result types onto its own framework.

## Commands

```
dotnet build Vicgital.Application.Shared.slnx      # build
dotnet build src/Vicgital.Application.Shared/Vicgital.Application.Shared.csproj   # build the library directly
```

There is no test project yet (`test/` exists but is currently empty). Once tests are added, they'll typically run via `dotnet test`, and a single test via `dotnet test --filter FullyQualifiedName~<Name>`.

`nuget.config` points at nuget.org and a private Vicgital GitHub package feed; the feed's read credential is expected via the `GIT_PACKAGES_READ_ONLY_PAT` environment variable, not committed anywhere.

## Architecture

The library is organized by concept, one namespace per folder under `src/Vicgital.Application.Shared/`:

- **`Exceptions/`** — `AppException` is the abstract root (carries a machine-readable `Code`). Concrete subtypes (`NotFoundException`, `ConflictException`, `ForbiddenException`, `UnauthorizedAppException`, `BusinessRuleViolationException`, `ValidationAppException`) are for *unexpected* failures meant to propagate up to a global exception handler in the consuming app.
- **`Results/`** — `Result` / `Result<T>` plus `Error`/`ErrorType` are for *expected* business/validation outcomes a caller branches on inline instead of via try/catch. `Result<T>` has implicit conversions from both `T` and `Error` for terse return statements.
- **`Envelopes/`** — `ApiResponse` / `ApiResponse<T>` are the standard `{ Success, Data, Errors, TraceId }` wire shape. `ResultExtensions.ToApiResponse()` is the bridge: it converts a `Result`/`Result<T>` directly into the matching envelope, so the exceptions path and the Result path both end up producing the same response shape at the API boundary.
- **`Pagination/`** — `PagedRequest` (normalizes page/size against `Constants.PaginationDefaults`, exposes `Skip`) and `PagedResult<T>` (`Items`, `TotalCount`, `TotalPages`, `HasNextPage`, `HasPreviousPage`).
- **`Constants/`** — shared literals (`PaginationDefaults`, `AppClaimTypes`, `AppHeaderNames`) so services don't redefine or drift on them independently.
- **`Abstractions/`** — interfaces only (`IDateTimeProvider`, `ICurrentUserService`, `ICorrelationIdProvider`); each consuming application supplies its own implementation and registers it in DI. Do not add implementations of these interfaces to this repo.

### Key design decisions to preserve when extending this library

- **Exceptions vs. Result is a deliberate two-track design, not an oversight.** Use `AppException` subtypes for failures that should propagate several layers up to a global handler; use `Result`/`Result<T>` for outcomes the immediate caller decides how to handle. Don't collapse these into one pattern.
- **No CQRS/MediatR marker interfaces** (`ICommand`, `IQuery`, etc.) exist here by design — this library intentionally has no opinion on request-pipeline shape. Don't add them without an explicit decision to do so.
- **Stay framework-agnostic.** Don't add a dependency on ASP.NET Core, MediatR, EF Core, or similar — that would force every consuming application onto the same framework choices.
- File naming for generic types uses the `Type{T}.cs` convention (e.g. `Result{T}.cs`, `PagedResult{T}.cs`, `ApiResponse{T}.cs`) to keep the generic overload visually distinct from its non-generic counterpart in the same folder.
