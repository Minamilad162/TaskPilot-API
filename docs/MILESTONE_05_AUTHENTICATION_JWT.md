# Milestone 05 - Authentication and JWT

## Branch

`feature/authentication-jwt`

## Scope

This milestone introduces authentication using ASP.NET Core Identity and JWT bearer tokens.

## Added Components

- `ApplicationUser` as the Identity user model.
- Register and login implementation through `AuthService`.
- JWT token generation through `JwtTokenService`.
- JWT configuration using strongly typed `JwtOptions`.
- `AuthController` with register and login endpoints.
- Swagger JWT bearer configuration.
- `CurrentUserService` to read the authenticated user's claims.

## API Endpoints

| Method | Endpoint | Description |
| --- | --- | --- |
| POST | `/api/v1/auth/register` | Creates a new user and returns a JWT token. |
| POST | `/api/v1/auth/login` | Authenticates an existing user and returns a JWT token. |

## Design Decisions

- ASP.NET Core Identity is used for user management and password hashing instead of custom password handling.
- JWT is used to keep the API stateless and easy to consume from web or mobile clients.
- A default `User` role is created during registration to prepare the system for role-based authorization later.
- JWT settings are centralized under the `Jwt` section in `appsettings.json`.

## Notes

Database migrations are intentionally delayed until the authentication layer is ready, because Identity changes the database schema.
