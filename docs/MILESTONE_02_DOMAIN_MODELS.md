# Milestone 2 - Domain Models

## Branch

`feature/domain-models`

## Goal

Add the core domain model without depending on ASP.NET Core, EF Core, or any infrastructure detail.

## Included

- Base entity model with `Id` and `CreatedAt`.
- Owned entity model with `OwnerId` for user-scoped data.
- Project aggregate root.
- Project task entity.
- Task status enum.
- Task priority enum.

## Design Notes

- Domain entities expose private setters to protect state changes.
- Factory methods keep object creation explicit.
- Update methods keep business changes discoverable.
- Basic domain invariants protect required fields before persistence.

## Next Milestone

`feature/application-contracts`
