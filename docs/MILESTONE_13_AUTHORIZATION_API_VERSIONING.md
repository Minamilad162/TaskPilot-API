# Milestone 13 - Authorization & API Versioning Polish

## Branch

```text
feature/authorization-and-api-versioning
```

## Goal

This milestone makes the role-based authorization bonus explicit while keeping the solution simple and maintainable.

## Changes

- Added centralized role constants through `ApplicationRoles`.
- Replaced plain `[Authorize]` on protected modules with explicit `[Authorize(Roles = ApplicationRoles.User)]`.
- Added `GET /api/v1/auth/me` to validate the current JWT token and show the authenticated user's claims.
- Extended `ICurrentUserService` to expose full name and roles from JWT claims.
- Updated the Postman collection and README to document the bonus features.

## Why this approach

The project already uses ASP.NET Core Identity and JWT. Adding explicit role authorization is a low-risk improvement that demonstrates production-minded security without introducing unnecessary complexity.

CQRS, MediatR, and Redis are intentionally left as future enhancements because this assessment system is small. Adding them only to satisfy bonus keywords would increase complexity without clear business value.

## New Endpoint

```http
GET /api/v1/auth/me
Authorization: Bearer {token}
```

Sample response:

```json
{
  "success": true,
  "message": "Request completed successfully.",
  "data": {
    "userId": "00000000-0000-0000-0000-000000000000",
    "email": "user@example.com",
    "fullName": "Test User",
    "roles": ["User"]
  },
  "errors": null
}
```

## Verification

```bash
dotnet build
dotnet test
```

Then run the API and test:

1. Register a user.
2. Login and copy the token.
3. Authorize in Swagger.
4. Call `GET /api/v1/auth/me`.
5. Call Projects and Tasks endpoints with the same token.
