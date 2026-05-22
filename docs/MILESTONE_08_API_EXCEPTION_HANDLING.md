# Milestone 08 - API Exception Handling

## Branch

`feature/api-exception-handling`

## Goal

Standardize runtime errors and validation failures across the API without duplicating try/catch logic inside controllers.

## Added

- Global exception handling middleware.
- Standard validation response for invalid request models.
- Consistent error payload using the existing generic `ApiResponse<T>` wrapper.
- Exception-to-HTTP status mapping.

## Error Mapping

| Exception | HTTP Status |
| --- | --- |
| ValidationException | 400 Bad Request |
| ArgumentException | 400 Bad Request |
| UnauthorizedAccessException | 401 Unauthorized |
| ForbiddenException | 403 Forbidden |
| NotFoundException | 404 Not Found |
| ConflictException | 409 Conflict |
| Unhandled Exception | 500 Internal Server Error |

## Response Shape

```json
{
  "success": false,
  "message": "Validation failed.",
  "data": null,
  "errors": [
    "The Name field is required."
  ]
}
```

## Design Notes

- Controllers stay focused on HTTP routing and successful responses.
- Application services throw meaningful exceptions.
- The API layer translates exceptions into clean HTTP responses.
- Internal server errors do not expose implementation details.
